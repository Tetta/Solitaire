﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenus : MonoBehaviour {
    public static UIMenus instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }
    private void Start() {
        OpenKlondikes();
    }

    void OnEnable()
	{
		SoundSystems.Instance.PlayerMusic(((Enums.MusicIndex)(Random.Range((int)Enums.MusicIndex.Background_I, (int)Enums.MusicIndex.Background_III + 1))) , true);
	}

	public void OpenKlondikes()
	{
		GameManager.Instance.GameType = Enums.GameScenes.Klondike;

		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: Get the scene loaded.
		Contains.GamePlayScene = Enums._GameScene [(int)GameManager.Instance.GameType];

		// TODO: Show the loadings.
		LoadingBehaviour.Instance.ShowLoading (Contains.GamePlayScene);
	}

    public void OpenSpider() {
        //point
        bool flag = IAPManager.vip;
#if UNITY_ANDROID
        flag = true;
#endif
        if (!flag) IAPManager.instance.ShowSubscriptionPanel("OpenSpider");
        else {
        GameManager.Instance.GameType = Enums.GameScenes.Spider;

        SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

        // TODO: Get the scene loaded.
        Contains.GamePlayScene = Enums._GameScene[(int)GameManager.Instance.GameType];

        // TODO: Show the loadings.
        LoadingBehaviour.Instance.ShowLoading(Contains.GamePlayScene);
        }
	}

	public void OpenTripeaks()
	{
        //point
        bool flag = IAPManager.vip;
#if UNITY_ANDROID
        flag = true;
#endif
        if (!flag) IAPManager.instance.ShowSubscriptionPanel("OpenTripeaks");
        else {
            GameManager.Instance.GameType = Enums.GameScenes.Tripeaks;

            SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

            // TODO: Get the scene loaded.
            Contains.GamePlayScene = Enums._GameScene[(int)GameManager.Instance.GameType];

            // TODO: Show the loadings.
            LoadingBehaviour.Instance.ShowLoading(Contains.GamePlayScene);
        }
	}

	void OnDisable()
	{
		if (!object.ReferenceEquals (SoundSystems.Instance, null)) {
			SoundSystems.Instance.StopMusic ();
		}
	}
}
