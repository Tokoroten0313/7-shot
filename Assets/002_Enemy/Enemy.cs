using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static float CHARGE_TIME = 1;

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

    //チャージショットタイム
    private float ChargeTimer = 0.0f;

    //7shotカウント
    [SerializeField] private int ShotCount = 0;

    //エネミーがバレットを所持しているかどうか
    public bool HaveBullet = false;

    //弾のオブジェクト取得
    public Bullet bullet;

    

    private enum EnemyState
    {
        Move,       //通常移動
        GetBullet,  //弾を拾いに行く
        BulletShot, //弾の発射
        none
    }

    

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
        if (!HaveBullet && !ExistDropBullet)
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
        //ShotTimerの加算
        ShotTimer += Time.deltaTime;

        //発射可能にするまでの処理
        if (EnableShot == false && ShotTimer >= 1.00)
        {
            EnableShot = true;
        }

        //ここはenemy用に変更必要
        if (Input.GetKey(KeyCode.Space))
        {
            ChargeTimer += Time.deltaTime;
        }

        //ここもenemy用に変更が必要
        if ((Input.GetKeyUp(KeyCode.Space)) && (EnableShot == true) && (HaveBullet == true))
        {
            //入力されている移動の角度の取得
            bullet.PlayerVectorX = Input.GetAxisRaw("Horizontal");
            bullet.PlayerVectorY = Input.GetAxisRaw("Vertical");
            bullet.BulletSpeedMagnification = 3;

            //通常弾の処理
            if (ChargeTimer < CHARGE_TIME && ShotCount != 6)
            {

                ShotTimer = 0.0f;
                EnableShot = false;
                HaveBullet = false;
                ShotCount++;
                bullet.BulletBoundLimit = 5;

                Instantiate(Bullet, transform.position, Quaternion.identity);
            }
            //チャージショットの処理
            else if (ChargeTimer > 1.0 && ShotCount != 6)
            {
                EnemyShot_c();
            }
            //7shot
            else if (ShotCount == 6)
            {
                EnemyShot_7();
            }
            ChargeTimer = 0.0f;

        }

    }

    private void EnemyCollision()
    {
        EnemyAnimation();
    }

    private void EnemyShot_c()
    {
        ShotTimer = 0.0f;
        EnableShot = false;
        HaveBullet = false;
        ShotCount++;
        bullet.BulletBoundLimit = 10;
        bullet.BulletSpeedMagnification = 4.5f;
        Instantiate(Bullet, transform.position, Quaternion.identity);
    }

    private void EnemyShot_7()
    {
        ShotTimer = 0.0f;
        EnableShot = false;
        HaveBullet = false;
        ShotCount = 0;
        bullet.BulletBoundLimit = 200;
        bullet.BulletSpeedMagnification = 6.0f;
        Instantiate(Bullet, transform.position, Quaternion.identity);
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
