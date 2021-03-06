﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private GameObject playButton;
    private GameObject pauseButton;
    private GameObject exitButton;
    private GameObject reloadButton;

    private bool once = false;
    private float startTime;

    private static int deathCount = 0;

    void Start()
    {
        startTime = Time.time;
        pauseButton = GameObject.Find("PauseButton");
        playButton = GameObject.Find("PlayButton");
        exitButton = GameObject.Find("ExitButton");
        reloadButton = GameObject.Find("ReloadButton");

        playButton.gameObject.SetActive(false);
        reloadButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        Advert.OpenBanner();
    }

    void Update () {
        if (GameMaster.GameOver)
        {
            GameOver();
        }
        else
            GameMaster.Score = (int)((Time.time - startTime) * 2);
	}

    public void PauseGame() {
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayGame() {
        StartCoroutine(Unpause());
    }

    IEnumerator Unpause()
    {
        float time = 3;
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
            yield return null;

        Time.timeScale = 1;
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        GameMaster.GameOver = false;
        SceneManager.LoadScene(0);
    }

    public void ReloadGame()
    {
        GameMaster.GameOver = false;
        GameMaster.Score = 0;
        SceneManager.LoadScene(1);
    }

    void GameOver() {
        if (once) return;
        once = true;

        deathCount++;

        //save record
        int lastScore = PlayerPrefs.GetInt("score", 0);
        if (GameMaster.Score > lastScore) PlayerPrefs.SetInt("score", GameMaster.Score);

        //remove scene
        reloadButton.SetActive(true);
        reloadButton.transform.GetChild(0).gameObject.SetActive(true);

        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);

        GameObject.Find("player").GetComponent<PolygonCollider2D>().enabled = false;
        GameObject.Find("player").GetComponent<PlayerScript>().Jump(false);
        Destroy(GameObject.Find("borders").gameObject);
        //Destroy(GameObject.Find("light").gameObject);
        Destroy(GameObject.Find("middle_bg").gameObject);
        Destroy(GameObject.Find("top_bg").gameObject);
        Destroy(GameObject.Find("bottom_bg").gameObject);
        GameObject.Find("enemies").GetComponent<LevelGenerator>().enabled = false;

        if (deathCount >= Advert.fullScreenFrequency)
        {
            Advert.OpenInterstitial();
            deathCount = 0;
        }
    }
}
