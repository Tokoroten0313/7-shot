using UnityEngine;

public class DropBullet : MonoBehaviour
{
    private Player playerScript;
    private Enemy enemyScript;
    private SpawnManager spawnManager;

    void Start()
    {  
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();

        //リスト取得のため
        spawnManager = FindFirstObjectByType<SpawnManager>();
    }

    void Update()
    {

    }

    private void DropBulletCollision()
    {

    }

    // プレイヤーとドロップバレットの衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーかエネミーのタグチェック
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーが弾を持っていなければ、ドロップバレットを消す
            if (!playerScript.HaveBullet)
            {
                playerScript.HaveBullet = true;
                spawnManager.RemoveDropBullet(this);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // プレイヤーが弾を持っていなければ、ドロップバレットを消す
            if (!enemyScript.HaveBullet)
            {
                enemyScript.HaveBullet = true;
                spawnManager.RemoveDropBullet(this);
                Destroy(gameObject);
            }
        }
        
    }
}
