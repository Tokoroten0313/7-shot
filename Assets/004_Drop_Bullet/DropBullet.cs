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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.HaveBullet == false)
        {
            Debug.Log("test");
            Destroy(gameObject);
            playerScript.HaveBullet = true;
        }
    }
}
