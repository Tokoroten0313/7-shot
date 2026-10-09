using UnityEngine;

public class Goal : MonoBehaviour
{
    // 得点を加算するときに参照
    private GameManager gmScore;

    void Start()
    {
        gmScore = GameObject.Find("GameManager")
                       .GetComponent<GameManager>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーの得点を加算する処理 x座標が0より右か左かで、どちら側の点数かを分ける
        if (transform.position.x > 0)
        {
            // ここで得点した弾の種類によって、点数を変えなければならない
            gmScore.Player1Score++;

            Debug.Log("Player1の加点");
        }
        else
        {   // ここで得点した弾の種類によって、点数を変えなければならない
            gmScore.Player2Score++;

            Debug.Log("Player2の加点");
        }

        // 衝突したBulletを削除する処理
        // if (collision.gameObject.CompareTag("Bullet"))のタグ指定でBulletだけ消す分岐にする
        Destroy(collision.gameObject);
    }
}
