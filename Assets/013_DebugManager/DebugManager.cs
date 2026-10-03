using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        TimeManage();
        ScoreManage();
    }

    private void TimeManage()
    {
        TimeStop();
        ResetScene();
        Time2x();
        
    }

    // 一時停止
    private void TimeStop()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                Debug.Log("ゲーム停止");
            }
            else
            {
                Time.timeScale = 1f;
                Debug.Log("ゲーム再開");
            }
        }
    }

    //現在シーンのリセット
    private void ResetScene()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            Debug.Log("デバックコマンドを受け付けました : ResetScene()");
        }
    }

    //倍速
    private void Time2x()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 2f;
                Debug.Log("ゲーム速度倍速");
            }
            else
            {
                Time.timeScale = 1f;
                Debug.Log("ゲーム速度等速");
            }
        }
    }

    private void ScoreManage()
    {
        Player1ScoreAdd();
        Player1ScoreSub();
        Player2ScoreAdd();
        Player2ScoreSub();
    }

    private void Player1ScoreAdd()
    {
        
    }

    private void Player1ScoreSub()
    {
        
    }

    private void Player2ScoreAdd()
    {
        
    }

    private void Player2ScoreSub()
    {
        
    }

}
