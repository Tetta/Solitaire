using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogNewGame : DialogInterface {

    [Header("UI")]
    public static bool isShown = false;
	/// <summary>
	/// The user interface title.
	/// </summary>
	[SerializeField] private Text UITitle;

	public override void Show ()
	{

        Debug.Log("DialogNewGame Show");
        GamePlay.autoWinShown = false;
        base.Show ();	

		// TODO: Check if null.
		if (!object.ReferenceEquals (UITitle, null)) {

			switch (GameManager.Instance.GameType) {
				
			case Enums.GameScenes.Klondike:

				// TODO: update the title.
				UITitle.text = "KINGS KLONDIKE";
				break;
			case Enums.GameScenes.Spider:

				// TODO: update the title.
				UITitle.text = "KINGS SPIDER";
				break;
			case Enums.GameScenes.Tripeaks:

				// TODO: update the title.
				UITitle.text = "KINGS TRIPEAKS";
				break;
			}
		}
	}

	/// <summary>
	/// Raises the touch escape event.
	/// </summary>
	public override void OnTouchEscape (){}

	/// <summary>
	/// Easies the mode.
	/// </summary>
	public void EasyMode()
	{
        PlayerData.Load();
        // TODO: playing the sound.
        SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: close the dialog with actions.
		Close (() => {
			
			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: update the mode of game.
				GameManager.Instance.UpdateModeGame (Enums.ModeGame.Easy);

				// TODO: update the state of game.
				GameManager.Instance.UpdateState (Enums.StateGame.Start);
			}		
		});
	}

	/// <summary>
	/// Mediums the mode.
	/// </summary>
	public void MediumMode()
	{
        PlayerData.Load();
        // TODO: playing the sound.
        SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: close the dialog with actions.
		Close (() => {

			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: update the mode of game.
				GameManager.Instance.UpdateModeGame (Enums.ModeGame.Medium);

				// TODO: update the state of game.
				GameManager.Instance.UpdateState (Enums.StateGame.Start);
			}	
		});
	}

	/// <summary>
	/// Hards the mode.
	/// </summary>
	public void HardMode()
	{
        PlayerData.Load();
        // TODO: playing the sound.
        SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: close the dialog with actions.
		Close (() => {

			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: update the mode of game.
				GameManager.Instance.UpdateModeGame (Enums.ModeGame.Hard);

				// TODO: update the state of game.
				GameManager.Instance.UpdateState (Enums.StateGame.Start);
			}	
		});
	}

    public override void Close() {
        

        base.Close();

        if (GameManager.Instance.GetModeGame() == Enums.ModeGame.None) newGame();
        //Debug.Log(GamePlay.Instance.IsConditionWining());
        //if (GamePlay.Instance.IsConditionWining()) newGame();
    }
    //=====
    public void newGame() {
        if(GameManager.Instance.GetModeGame() == Enums.ModeGame.None) GameManager.Instance.UpdateModeGame(Enums.ModeGame.Hard);
        playMode(GameManager.Instance.GameType, GameManager.Instance.GetModeGame());
    }
    public void klondikeHard() {
        playMode(Enums.GameScenes.Klondike, Enums.ModeGame.Hard);
    }
    public void klondikeMedium() {
        
        if (isAvailable("KlondikeMedium")) playMode(Enums.GameScenes.Klondike, Enums.ModeGame.Medium);
    }
    public void klondikeEasy() {
        if (isAvailable("KlondikeEasy")) playMode(Enums.GameScenes.Klondike, Enums.ModeGame.Easy);
    }
    public void spiderHard() {
        if (isAvailable("SpiderHard")) playMode(Enums.GameScenes.Spider, Enums.ModeGame.Hard);
    }
    public void spiderMedium() {
        if (isAvailable("SpiderMedium")) playMode(Enums.GameScenes.Spider, Enums.ModeGame.Medium);
    }
    public void spiderEasy() {
        if (isAvailable("SpiderEasy")) playMode(Enums.GameScenes.Spider, Enums.ModeGame.Easy);
    }
    public void tripeakHard() {
        if (isAvailable("TripeakHard")) playMode(Enums.GameScenes.Tripeaks, Enums.ModeGame.Hard);
    }
    public void tripeakMedium() {
        if (isAvailable("TripeakMedium")) playMode(Enums.GameScenes.Tripeaks, Enums.ModeGame.Medium);
    }
    public void tripeakEasy() {
        if (isAvailable("TripeakEasy")) playMode(Enums.GameScenes.Tripeaks, Enums.ModeGame.Easy);
    }

    public void playMode (Enums.GameScenes scene, Enums.ModeGame mode) {

        Debug.Log("playMode: " + scene + " " + mode);

        Contains.Time = 0;
        // TODO: Check the condition null.
        if (!object.ReferenceEquals(TimerBehaviours.handleCountTiming, null)) {

            // TODO: Kill the old process.
            MEC.Timing.KillCoroutines(TimerBehaviours.handleCountTiming);
        }


        //        if (GameManager.Instance.GameType != scene) {
        GameManager.Instance.GameType = scene;

            SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

            // TODO: Get the scene loaded.
            Contains.GamePlayScene = Enums._GameScene[(int)GameManager.Instance.GameType];
           
            GameManager.Instance.UpdateModeGame(mode);
        Close();
        // TODO: Clear the handles.
        // TODO: Check if not null.
        if (!isShown) isShown = true;
        else
        if (!object.ReferenceEquals(GamePlay.Instance, null)) {

                switch (GameManager.Instance.GameType) {

                    case Enums.GameScenes.Tripeaks:

                        // TODO: Create the new class.
                        if (GamePlay.Instance._TGamePlay != null && GamePlay.Instance._TGamePlay.cardsGet != null) GamePlay.Instance.StopTimingHandle();
                        break;
                    case Enums.GameScenes.Spider:

                        // TODO: Create the new class.

                        if (GamePlay.Instance._SGamePlay != null && GamePlay.Instance._SGamePlay.cardsGet != null) GamePlay.Instance.StopTimingHandle();
                        break;
                    case Enums.GameScenes.Klondike:

                        // TODO: Init start.
                        if (GamePlay.Instance._KGamePlay  != null && GamePlay.Instance._KGamePlay.cardsGet != null) GamePlay.Instance.StopTimingHandle();
                        break;
                }

                GameManager.Instance. InitStart();
                Debug.Log("InitCards");
                GameManager.Instance.InitCards();
            }
            // TODO: Show the loadings.
            LoadingBehaviour.Instance.ShowLoading(Contains.GamePlayScene, false, true);


 //       }
//        else 
 //           updateMode(mode);
    }

    public static void updateMode (Enums.ModeGame mode) {
        Debug.Log("updateMode: " + mode);
        //------ mode
        PlayerData.Load();
        // TODO: playing the sound.
        SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);
        
        

        // TODO: Turn off hint.
        //HintDisplay.Instance.DisableHint();



        // TODO: close the dialog with actions.
        var go = GameObject.Find("DialogNewGame(Clone)");
        if (go != null) {
            //gameObject.SetActive(false);
            go.GetComponent<DialogNewGame>().Close
            (() => {

                // TODO: Check the exists of GameManager.
                if (!object.ReferenceEquals(GameManager.Instance, null)) {

                    // TODO: update the mode of game.
                    GameManager.Instance.UpdateModeGame(mode);

                    // TODO: update the state of game.
                    GameManager.Instance.UpdateState(Enums.StateGame.Start);
                }
            });
        } else {

            // TODO: Check the exists of GameManager.
            if (!object.ReferenceEquals(GameManager.Instance, null)) {

                // TODO: update the mode of game.
                GameManager.Instance.UpdateModeGame(mode);

                // TODO: update the state of game.
                GameManager.Instance.UpdateState(Enums.StateGame.Start);
            }
        }

        /*
        switch (GameManager.Instance.GameType) {

            case Enums.GameScenes.Tripeaks:

                // TODO: Create the new class.
                new TRIPEAKS._GamePlay().InitStart();
                break;
            case Enums.GameScenes.Spider:

                // TODO: Create the new class.

                new SPIDER._GamePlay().InitStart();
                break;
            case Enums.GameScenes.Klondike:

                // TODO: Init start.
                new KLONDIKE._GamePlay().InitStart();
                break;
        }
        */
    }

    bool isAvailable (string from) {
        bool flag = IAPManager.vip;
        #if UNITY_ANDROID
                //fix
                //flag = true;
#endif
        if (flag) return true;
        else {
            IAPManager.instance.ShowSubscriptionPanel(from);
            return false;
        }
    }
}
