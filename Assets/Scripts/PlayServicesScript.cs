using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class PlayServicesScript : MonoBehaviour {
    [SerializeField]
    private string leaderBoardID;

    void Start()
    {
        PlayGamesPlatform.Activate();
    }

    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("You've successfully logged in");
            }
            else
            {
                Debug.Log("Login failed for some reason");
            }
        });
    }

    public void LeaderBoardShow()
    {
        Social.ShowLeaderboardUI();
    }
}
