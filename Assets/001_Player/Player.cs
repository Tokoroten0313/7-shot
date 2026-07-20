using UnityEngine;

public class Player : MonoBehaviour
{
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
        //x,y,z êÈåæÅ@Å@(x,y,z) = (0,0,0)         
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
