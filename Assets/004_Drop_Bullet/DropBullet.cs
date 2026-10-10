using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    private Player playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponent<Player>();

        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DropBulletCollision()
    {

    }

    // プレイヤーとドロップバレットの衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーが弾を持っていなければ、ドロップバレットを消す
            if (playerScript.HaveBullet == false)
            {
                Destroy(gameObject);
                playerScript.HaveBullet = true;
            }
        }
        
    }
}
