using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GameTime { get; private set; }

    //Player1のスコア
    public float Player1Score = 0;
    //Player2のスコア
    public float Player2Score = 0;

    //ポーズしているかしていないか？
    private bool IsPaused = false;

    void Start()
    {
        
    }

    void Update()
    {
        GetPlayerScore();
        GetEnemyScore();
        GetTime();
        Pause();
        TimeAdd();
    }

    private void GetPlayerScore()
    {

    }

    private void GetEnemyScore()
    {

    }

    private void TimeAdd()
    {
        if (GameTime <= 60)
        {
            GameTime += Time.deltaTime;
        }
        
    }

    private void GetTime()
    {

    }

    private void Pause()
    {

    }

}
