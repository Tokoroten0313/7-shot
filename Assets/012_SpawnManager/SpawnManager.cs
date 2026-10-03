using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //弾のオブジェクト
    public GameObject DropBullet;
    //ゲームマネージャのオブジェクト
    public GameManager GameManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SpawnDropBullet()
    {
        if(GameManager.GameTime < 20.0f)
        {
            for(int i = 100; i < 100; )
            Instantiate(DropBullet, transform.position, Quaternion.identity);
        }
        else
        {

        }
    }
}
