﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameLoopManager : MonoBehaviour {

    public AudioSource bgmAudioSource;
    public HUE_Rotate hueRotate;

    public ScoreManager scoreManager;
    public AudioSource bgmAudio;

    public List<BulletScript> bullets;
    public PlayerController playerController;
    public FollowTheBeat followTheBeat;

    bool playerAlive = true;

    public void GameOver()
    {
        DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, 0, 0.5f).SetUpdate(true);

        bgmAudioSource.DOFade(0, 1f).OnComplete(() =>
         {
             bgmAudioSource.Stop();
             playerAlive = false;
         }).SetUpdate(true);
        hueRotate.RotateValue = Mathf.PI;
    }

    public void RestartGame()
    {
        playerAlive = true;
        hueRotate.RotateValue = 0;
        scoreManager.Reset();
        playerController.Reset();
        followTheBeat.Reset();

        for (int i = 0; i < bullets.Count; i++)
        {
            GameObject.Destroy(bullets[i].gameObject);
        }

        bullets.Clear();
        bgmAudio.volume = 1;
        bgmAudio.Play();
        Time.timeScale = 1;
    }
    public void StartGame()
    {
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerAlive)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
        }
	}
}
