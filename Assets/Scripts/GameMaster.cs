using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    private static bool _GameOver;
    private static int score;
    
    public static bool GameOver
    {
        get { return _GameOver; }
        set { _GameOver = value; }
    }

    public static int Score
    {
        get { return score; }
        set { score = value; }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
