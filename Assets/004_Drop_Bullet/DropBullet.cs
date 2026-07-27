using System.Runtime.CompilerServices;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DropBulletCollision();
    }

    private void DropBulletCollision()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        Destroy(gameObject);
    }
}
