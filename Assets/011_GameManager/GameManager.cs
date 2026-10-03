using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GameTime = 0;

    //Player1のスコア
    public float Player1Score = 0;
    //Player2のスコア
    public float Player2Score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        GetPlayerScore();
        GetEnemyScore();
        GetTime();
        Pause();
    }

    private void GetPlayerScore()
    {

    }

    private void GetEnemyScore()
    {

    }

    private void TimeAdd()
    {
        GameTime += Time.deltaTime;
    }

    private void GetTime()
    {

    }

    private void Pause()
    {

    }

}
