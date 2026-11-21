using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //弾のオブジェクト
    [SerializeField] private GameObject DropBulletPrefab;

    //ゲームマネージャのオブジェクト
    public GameManager gameManager;

    //spawnの生成時間
    private float spawntime = 0;

    //弾のオブジェクトをリスト型で取得
    public List<DropBullet> ActiveDropBullets
       = new List<DropBullet>();

    void Start()
    {

    }

    void Update()
    {
        SpawnDropBullet();
    }

    //実際の生成とリストの箱入れ関数
    private void SpawnOneDropBullet(Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(
            DropBulletPrefab,
            spawnPosition,
            Quaternion.identity
        );

        DropBullet bullet = obj.GetComponent<DropBullet>();

        if (bullet != null)
        {
            ActiveDropBullets.Add(bullet);
        }
    }

    //取得したdropbullet削除用関数
    public void RemoveDropBullet(DropBullet bullet)
    {
        ActiveDropBullets.Remove(bullet);
    }


    private void SpawnDropBullet()
    {
        float randomXNumber = Random.Range(-7f, 7f);
        float randomYNumber = Random.Range(-3f, 3f);


        spawntime += Time.deltaTime;

        //60秒のタイミングで生成終了
        //To Do: 時間ごとのspawntimeの修正箇所
        if (gameManager.GameTime >= 60.0f)
        {
            return;
        }

        // 仮の生成間隔：2秒
        if (spawntime >= 2.0f)
        {

            Vector3 spawnPosition = new Vector3(
                randomXNumber,
                randomYNumber,
                0f
            );

            SpawnOneDropBullet(spawnPosition);

            spawntime = 0f;
        }
    }
}

