using UnityEngine;

public class Player : MonoBehaviour
{
    //弾のオブジェクト
    public GameObject Bullet;
    //弾の発射までのインターバル用変数
    private float ShotTimer = 0.0f;
    //現在発射可能か
    [SerializeField] private bool EnableShot = false;
    //チャージショットタイム
    private float ChargeTimer = 0.0f;

    //7shotカウント
    private int ShotCount = 0;

    // プレイヤーがバレットを所持しているかどうか
    public bool HaveBullet = false;

    public Bullet bullet;

    void Start()
    {
        
    }
 
    void Update()
    {
        PlayerMove();
        PlayerShot();
        PlayerCollison();
    }

    void PlayerMove()
    {
        //x,y,z 宣言　　(x,y,z) = (0,0,0)         
        Vector3 move = Vector3.zero;

        //wasd
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }

        transform.Translate(move * Time.deltaTime);
    }

    private void PlayerShot()
    {
        //ShotTimerの加算
        ShotTimer += Time.deltaTime;

        //発射可能にするまでの処理
        if (EnableShot == false && ShotTimer <= 1.00)
        {
            EnableShot = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            ChargeTimer += Time.deltaTime;
        }

        if ((Input.GetKeyUp(KeyCode.Space)) && (EnableShot == true) && (HaveBullet == true))
        {
            //入力されている移動の角度の取得
            bullet.PlayerVectorX = Input.GetAxisRaw("Horizontal");
            bullet.PlayerVectorY = Input.GetAxisRaw("Vertical");



            //通常弾の処理
            if (ChargeTimer < 3.0 && ShotCount != 6)
            {
                ShotTimer = 0.0f;
                EnableShot = false;
                HaveBullet = false;
                ShotCount++;
                bullet.BulletBoundLimit = 5;
                Instantiate(Bullet, transform.position, Quaternion.identity);
                
            }
            //チャージショットの処理
            else if (ChargeTimer > 3.0 && ShotCount != 6)
            {
                PlayerShot_c();
            }
            //7shot
            else if (ShotCount == 6)
            {
                PlayerShot_7();
            }
            ChargeTimer = 0.0f;

        }

        
        
    }

    private void PlayerCollison()
    {
        PlayerAnimation();
    }

    private void PlayerShot_c()
    {
        ShotTimer = 0.0f;
        EnableShot = false;
        HaveBullet = false;
        ShotCount++;
        bullet.BulletBoundLimit = 10;
    }

    private void PlayerShot_7()
    {
        ShotTimer = 0.0f;
        EnableShot = false;
        HaveBullet = false;
        ShotCount = 0;
        bullet.BulletBoundLimit = 200;
    }

    private void PlayerAnimation()
    {

    }

}
