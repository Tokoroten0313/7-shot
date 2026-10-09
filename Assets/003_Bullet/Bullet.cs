using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bullet;

    // 弾が一度反射したかどうか
    private bool HitWall = false;
    // 初期の弾の進行方向
    private Vector3 BulletVector;
    // 現在の弾の進行方向
    private Vector3 NowVelocity;

    // 弾のx軸の値
    public float PlayerVectorX = 3;

    // 弾のy軸の値
    public float PlayerVectorY = 0;

    //弾の反射回数
    private int BoundCount = 0;

    //5・10・200
    public int BulletBoundLimit = 0;

    private int InitBoundLimit = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        //弾の反射回数を初期化
        InitBoundLimit = BulletBoundLimit;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
        BulletCollision();

    }

    private void BulletMove()
    {
        if (HitWall == false)
        {
            // いずれ八方向分をswichで分岐させる
            BulletVector = new Vector3(PlayerVectorX, PlayerVectorY, 0);
            bullet.linearVelocity = BulletVector;
        }
        // 現在の弾のベクトルを保存
        NowVelocity = bullet.linearVelocity;
    }
    

    private void BulletCollision()
    {
        BulletGoal();
    }

    private void BulletGoal()
    {
        if (BoundCount == InitBoundLimit)
        {
            Destroy(gameObject);
        }
    }

    private void BulletDestroy()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した面のベクトルを反転させる処理
        ContactPoint2D contactPoint = collision.GetContact(0);
        bullet.linearVelocity = Vector3.Reflect(NowVelocity, contactPoint.normal);

        HitWall = true;
        BoundCount++;
    }
}