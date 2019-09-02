using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenus : MonoBehaviour {

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

	public void OpenSpider()
	{
		GameManager.Instance.GameType = Enums.GameScenes.Spider;

		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: Get the scene loaded.
		Contains.GamePlayScene = Enums._GameScene [(int)GameManager.Instance.GameType];

		// TODO: Show the loadings.
		LoadingBehaviour.Instance.ShowLoading (Contains.GamePlayScene);
	}

	public void OpenTripeaks()
	{
		GameManager.Instance.GameType = Enums.GameScenes.Tripeaks;

		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: Get the scene loaded.
		Contains.GamePlayScene = Enums._GameScene [(int)GameManager.Instance.GameType];

		// TODO: Show the loadings.
		LoadingBehaviour.Instance.ShowLoading (Contains.GamePlayScene);
	}

	void OnDisable()
	{
		if (!object.ReferenceEquals (SoundSystems.Instance, null)) {
			SoundSystems.Instance.StopMusic ();
		}
	}
}
