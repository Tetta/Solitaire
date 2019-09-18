using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// TODO: Load the process players.
		PlayerData.Load ();

		// TODO: Load the config.
		ConfigApplication ();

		// TODO: Get the scene loaded.
		//Contains.GamePlayScene = Enums._GameScene [(int)Enums.GameScenes.Menus];

		// TODO: Show the loadings.
		//LoadingBehaviour.Instance.ShowLoading (Contains.GamePlayScene);
	}

	public void ConfigApplication()
	{
		// TODO: Set the target frame.
		Application.targetFrameRate = 60;

		// TODO: Disable the multitouch.
		Input.multiTouchEnabled = false;
	}
}
