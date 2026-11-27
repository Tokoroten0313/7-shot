using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    

    //行動制限
    private static float LIMITPOSX_MIN = 0f;
    private static float LIMITPOSX_MAX = 8.4f;
    private static float LIMITPOSY = 4.4f;

    //エネミーの速度
    [SerializeField] private float EnemySpeed = 2;

    //dropbulletが生成されているか
    public bool ExistDropBullet = false;

    //ランダム移動の目的地
    private Vector3 targetPosition;

    //目的地を変更するまでの時間
    private float targetChangeTimer = 0f;

    //目的地を変更する間隔
    [SerializeField] private float targetChangeInterval = 2f;

    [SerializeField] private SpawnManager spawnManager;

    private EnemyState enemyState;

    //取得を行う対象の弾
    private DropBullet targetBullet;

    //以下弾の変数
    //弾のオブジェクト
    public GameObject Bullet;

    //弾の発射までのインターバル用変数
    private float ShotTimer = 0.0f;

    //現在発射可能か
    [SerializeField] private bool EnableShot = false;

    //7shotカウント
    [SerializeField] private int ShotCount = 0;

    //エネミーがバレットを所持しているかどうか
    public bool HaveBullet = false;

    private enum EnemyState
    {
        Move,       //通常移動
        GetBullet,  //弾を拾いに行く
        BulletShot, //弾の発射
        none
    }

    // 射撃パターン
    private enum ShotPattern
    {
        Player,       // プレイヤーを狙う
        Goal,         // Goalを直接狙う
        WallBounce    // 壁に反射させてGoalを狙う
    }

    // 今回選択された射撃パターン
    private ShotPattern shotPattern;

    [Header("攻撃パターンの選択確率")]
    [SerializeField] private float PlayerShotRate = 15f;
    [SerializeField] private float GoalShotRate = 15f;
    [SerializeField] private float WallBounceShotRate = 70f;

    [Header("射撃対象")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyGoalTransform;

    // 通常弾・チャージ弾の選択確率
    [Header("射撃種類の選択確率")]
    [SerializeField] private float NormalShotRate = 70f;
    [SerializeField] private float ChargeShotRate = 30f;

    [Header("壁反射用")]
    [SerializeField] private Collider2D topWallCollider;
    [SerializeField] private Collider2D bottomWallCollider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateChange();

        switch (enemyState)
        {
            case EnemyState.Move:
                EnemyMove();
                break;

            case EnemyState.GetBullet:
                EnemyGetBullet();
                break;

            case EnemyState.BulletShot:
                EnemyShot();
                break;
            
            case EnemyState.none:

                break;
        }
        EnemyCollision();
    }

    private void EnemyMove()
    {
        //弾を持っておらず、落ちている弾もない場合
        if (!ExistDropBullet)
        {
            //目的地を変更するタイミング
            targetChangeTimer -= Time.deltaTime;

            if (targetChangeTimer <= 0f ||
                Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                //画面内のランダムな位置を目的地にする
                targetPosition = new Vector3(
                    Random.Range(LIMITPOSX_MIN, LIMITPOSX_MAX),
                    Random.Range(-LIMITPOSY, LIMITPOSY),
                    transform.position.z
                );

                targetChangeTimer = targetChangeInterval;
            }

            //目的地に向かって移動(MoveTowardsの引数と戻り値以下)
            //1,現在の位置 2,移動先の目標位置 3,1回の処理で移動できる最大距離 戻り値移動後の位置を表すVector3
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                EnemySpeed * Time.deltaTime
            );
        }
    }

    //弾の取得挙動
    private void EnemyGetBullet()
    {
        //取得対象の弾が存在しない場合
        if (targetBullet == null)
        {
            enemyState = EnemyState.Move;
            return;
        }

        //取得対象の弾に向かって移動
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetBullet.transform.position,
            EnemySpeed * Time.deltaTime
        );
    }

    private void EnemyShot()
    {
        //射撃準備中もランダムに移動
        EnemyMove();
        
        //ShotTimerの加算
        ShotTimer += Time.deltaTime;

        //発射可能にするまでの処理
        if (EnableShot == false && ShotTimer >= 1.00)
        {
            EnableShot = true;
        }

        //ここもenemy用に変更が必要
        if (EnableShot == true && HaveBullet == true)
        {
            //ショットの種類選択
            SelectShotPattern();

            Vector2 shotDirection = CalculateShotDirection();

            // 射撃方向が正常に計算できなかった場合は発射しない
            if (shotDirection == Vector2.zero)
            {
                return;
            }

            // 7shotの判定
            if (ShotCount == 6)
            {
                EnemyShot_7(shotDirection);
            }
            // 通常弾・チャージ弾をランダムに選択
            else
            {
                float randomShot = Random.Range(0f,NormalShotRate + ChargeShotRate);
                //通常弾の処理
                if (randomShot < NormalShotRate)
                {
                    ShotTimer = 0.0f;
                    EnableShot = false;
                    HaveBullet = false;
                    ShotCount++;
                    Bullet newBullet = CreateBullet(shotDirection);
                    newBullet.BulletBoundLimit = 5;
                    newBullet.BulletSpeedMagnification = 3;

                }
                else
                {
                    // チャージ弾
                    EnemyShot_c(shotDirection);
                }
            }


            
            

        }

    }

    private void SelectShotPattern()
    {
        //ショットパターンの乱数生成
        float randomPattern = Random.Range(0, PlayerShotRate + GoalShotRate + WallBounceShotRate);

        if (randomPattern < PlayerShotRate)
        {
            shotPattern = ShotPattern.Player;
        }
        else if (randomPattern < PlayerShotRate + GoalShotRate)
        {
            shotPattern = ShotPattern.Goal;
        }
        else
        {
            shotPattern = ShotPattern.WallBounce;
        }
    }

    // 射撃方向を計算する
    private Vector2 CalculateShotDirection()
    {
        Vector2 targetPosition = Vector2.zero;

        // 選択された射撃パターンによって対象を変更
        switch (shotPattern)
        {
            // Playerを狙う
            case ShotPattern.Player:
                if (playerTransform == null) return Vector2.zero;

                targetPosition = playerTransform.position;
                break;

            // Goalを直接狙う
            case ShotPattern.Goal:
                if (enemyGoalTransform == null) return Vector2.zero;

                targetPosition = enemyGoalTransform.position;
                break;

            // 壁反射は次の段階で実装
            case ShotPattern.WallBounce:
                // 上側・下側のどちらの壁を使うかランダムに選択
                bool useTopWall = Random.Range(0, 2) == 0;

                float wallY;

                if (useTopWall)
                {
                    if (topWallCollider == null) return Vector2.zero;

                    // 上側の壁の内側の面
                    wallY = topWallCollider.bounds.min.y;
                }
                else
                {
                    if (bottomWallCollider == null) return Vector2.zero;

                    // 下側の壁の内側の面
                    wallY = bottomWallCollider.bounds.max.y;
                }

                // Goalを壁に対して鏡像の位置に移動させる
                Vector2 mirroredGoal = new Vector2(
                    enemyGoalTransform.position.x,
                    2f * wallY - enemyGoalTransform.position.y
                );

                // Enemyから鏡像Goalに向かう方向を計算
                Vector2 wallBounceDirection =
                    mirroredGoal - (Vector2)transform.position;

                return wallBounceDirection.normalized;
        }

        // Enemyから対象までの方向ベクトルを計算
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // 方向ベクトルを正規化して返す
        return direction.normalized;
    }

    //弾の生成
    private Bullet CreateBullet(Vector2 direction)
    {
        GameObject obj = Instantiate(
            Bullet,
            transform.position,
            Quaternion.identity
        );

        Bullet newBullet = obj.GetComponent<Bullet>();

        //発射方向の設定
        newBullet.PlayerVectorX = direction.x;
        newBullet.PlayerVectorY = direction.y;

        return newBullet;
    }

    private void EnemyCollision()
    {
        EnemyAnimation();
    }

    //チャージショット
    private void EnemyShot_c(Vector2 direction)
    {
        ShotTimer = 0.0f;
        EnableShot = false;
        HaveBullet = false;
        ShotCount++;
        Bullet newBullet = CreateBullet(direction);
        newBullet.BulletBoundLimit = 10;
        newBullet.BulletSpeedMagnification = 4.5f;
        
    }

    //7ショット
    private void EnemyShot_7(Vector2 direction)
    {
        ShotTimer = 0.0f;
        EnableShot = false;
        HaveBullet = false;
        ShotCount = 0;
        Bullet newBullet = CreateBullet(direction);
        newBullet.BulletBoundLimit = 200;
        newBullet.BulletSpeedMagnification = 6.0f;
        
    }

    private DropBullet FindNearestBullet(List<DropBullet> bullets)
    {
        DropBullet nearestBullet = null;
        float nearestDistance = Mathf.Infinity;

        foreach (DropBullet bullet in bullets)
        {
            if (bullet == null)
            {
                continue;
            }
                
            // 画面右半分の弾だけを対象にする
            if (bullet.transform.position.x < 0f)
            {
                continue;
            }
                
            float distance = Vector2.Distance(
                transform.position,
                bullet.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestBullet = bullet;
            }
        }

        return nearestBullet;
    }

    //敵の状態を管理する関数
    private void EnemyStateChange()
    {
        //弾をもっているなら
        if (HaveBullet)
        {
            //取得を行う対象の弾をnull
            targetBullet = null;
            enemyState = EnemyState.BulletShot;
        }
        else
        {
            //取得対象の弾がまだ決まっていない場合
            if (targetBullet == null && spawnManager != null)
            {
                targetBullet = FindNearestBullet(
                    spawnManager.ActiveDropBullets
                );
            }

            //落ちている弾が0より多くかつ弾を持っていない状態なら
            if (targetBullet != null)
            {
                enemyState = EnemyState.GetBullet;
            }
            else
            {
                enemyState = EnemyState.Move;
            }
        }
        
        
    }

    private void EnemyAnimation()
    {
        
    }
}
