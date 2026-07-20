using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyShot();
        EnemyCollision();
    }

    private void EnemyMove()
    {

    }

    private void EnemyShot()
    {
        EnemyShot_c();
        EnemyShot_7();
    }

    private void EnemyCollision()
    {
        EnemyAnimation();
    }

    private void EnemyShot_c()
    {

    }

    private void EnemyShot_7()
    {

    }

    private void EnemyAnimation()
    {

    }
}
