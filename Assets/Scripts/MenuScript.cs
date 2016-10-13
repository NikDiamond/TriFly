using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	void Start () {
        Advert.CloseBanner();
        Advert.CloseInterstitial();
	}
    public void OpenScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
