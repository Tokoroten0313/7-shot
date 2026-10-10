using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //弾のオブジェクト
    public GameObject DropBullet;
    //ゲームマネージャのオブジェクト
    public GameManager gameManager;
    private float spawntime = 0;

    void Start()
    {
       
    }

    void Update()
    {
        SpawnDropBullet();
    }

    private void SpawnDropBullet()
    {
        float randomXNumber = Random.Range(-7f, 7f);
        float randomYNumber = Random.Range(-3f, 3f);
        
        Debug.Log(spawntime);
        spawntime += Time.deltaTime;

        //X軸-7~7/Y軸-3~3
        if (gameManager.GameTime < 20.0f)
        {
            if (spawntime >= 2.0f)
            {
                Instantiate(DropBullet, transform.position = new Vector3(randomXNumber, randomYNumber, 0), Quaternion.identity);
                spawntime = 0;
            }
            


        }
        else if((gameManager.GameTime < 40.0f) && (gameManager.GameTime > 20.0f))
        {

        }
        else if ((gameManager.GameTime < 60.0f) && (gameManager.GameTime > 40.0f))
        {

        }
        else
        {

        }
    }
}
