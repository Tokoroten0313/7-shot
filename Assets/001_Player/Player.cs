using UnityEngine;

public class Player : MonoBehaviour
{
    //弾のオブジェクト
    public GameObject Bulllet;
    //弾の発射までのインターバル用変数
    private float ShotTimer = 0.0f;
    //現在発射可能か
    [SerializeField] private bool EnableShot = false;
    //チャージショットタイム
    private float ChargeTimer = 0.0f;


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
        ShotTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && EnableShot == false)
        {
            ChargeTimer += Time.deltaTime;



        }

        if (Input.GetKeyUp(KeyCode.Space) && (ShotTimer > 1.0f) )
        {
            if(ChargeTimer > 0.01f)
            {
                Instantiate(Bulllet, transform.position, Quaternion.identity);
            }
            
            ShotTimer = 0.0f;
        }

        PlayerShot_c();
        PlayerShot_7();
    }

    private void PlayerCollison()
    {
        PlayerAnimation();
    }

    private void PlayerShot_c()
    {

    }

    private void PlayerShot_7()
    {

    }

    private void PlayerAnimation()
    {

    }

}
