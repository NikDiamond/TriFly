using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    Sprite soundOn;
    [SerializeField]
    Sprite soundOff;

    void Start () {
        Advert.CloseBanner();
        Advert.CloseInterstitial();

        soundReset();
    }
    
    public void Stats()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI7oXu-e8GEAIQAA");
    }

    public void OpenScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void soundToggle()
    {
        if (GameMaster.soundEnabled())
            GameMaster.setSoundEnabled(false);
        else
            GameMaster.setSoundEnabled(true);

        soundReset();
    }
    private void soundReset()
    {
        if (GameMaster.soundEnabled())
            GameObject.Find("SoundButton").gameObject.GetComponent<UnityEngine.UI.Image>().sprite = soundOn;
        else
            GameObject.Find("SoundButton").gameObject.GetComponent<UnityEngine.UI.Image>().sprite = soundOff;
    }

    public void OpenVideo()
    {
        Advert.OpenVideo();
    }
}
