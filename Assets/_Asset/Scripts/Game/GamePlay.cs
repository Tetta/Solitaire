using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GamePlay : Singleton < GamePlay > {

	[Header ("UI")]
	[SerializeField] public Transform TDrawCards;
    // =============================== Variables =============================== //

    #region Cache

    // TODO: Get the cache gameplay from Spider.
    public SPIDER._GamePlay _SGamePlay;

    // TODO: Get the cache gameplay from Tripick.
    public TRIPEAKS._GamePlay _TGamePlay;

	// TODO: Get the cache gameplay from Klondike.
	public KLONDIKE._GamePlay _KGamePlay;

	[HideInInspector] 
	public CardBehaviour cardSaveCache;

	[HideInInspector]
	public TimerBehaviours TimeController;
	#endregion



	#region Functional System.

	/// <summary>
	/// Start this instance.
	/// </summary>

	protected override void Awake ()
	{
        AdController.ShowInterstitial();

        // TODO: Reset the main camera.
		Helper.mainCamera = null;

		base.Awake ();
	
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Create the new class.
			_TGamePlay = new TRIPEAKS._GamePlay ();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Create the new class.
			_SGamePlay = new SPIDER._GamePlay ();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Create the new class.
			_KGamePlay = new KLONDIKE._GamePlay ();
			break;
		} 

		// TODO: Add the time controller.
		TimeController = gameObject.AddComponent < TimerBehaviours > ();
	}

	void Start()
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Create the new class.
			_TGamePlay.InitStart();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Create the new class.

			_SGamePlay.InitStart();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Init start.
			_KGamePlay.InitStart ();
			break;
		} 
	}

	#endregion

	#region Init System



	public void StopTimingHandle()
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO:Stop the time.
			_TGamePlay.StopTimingHandle();
			break;
		case Enums.GameScenes.Spider:

			// TODO:Stop the time.
			_SGamePlay.StopTimingHandle();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Stop the time.
			_KGamePlay.StopTimingHandle ();
			break;
		} 
	}



	#endregion

    #region Condition Solitaire


    /// <summary>
    /// Check the condition to win this game.
    /// </summary>
    public bool IsConditionWining()
    {
		switch (GameManager.Instance.GameType) {
			
		case Enums.GameScenes.Tripeaks:

			// TODO: Check condition wining.
			return _TGamePlay.IsConditionWining ();
		case Enums.GameScenes.Spider:


			// TODO: Check condition wining.
			return _SGamePlay.IsConditionWining ();
		case Enums.GameScenes.Klondike:

			// TODO: Check condition wining.
			return _KGamePlay.IsConditionWining ();
		default:
			
			// TODO: Check condition wining.
			return _SGamePlay.IsConditionWining ();
		}
    }

	public bool IsConditionLosing()
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Check condition losing.
			return _TGamePlay.IsConditionLosing ();
		case Enums.GameScenes.Klondike:

			// TODO: Check condition losing.
			return _KGamePlay.IsConditionLosing ();
		}

		return false;
	}

    public bool IsHintAvailable(bool IsShow = true)
    {
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Checking Hint.
			return _TGamePlay.IsHintAvailable (IsShow);
		case Enums.GameScenes.Spider:


			// TODO: Checking Hint.
			return _SGamePlay.IsHintAvailable (IsShow);

		case Enums.GameScenes.Klondike:

			// TODO: Check hint.
			return _KGamePlay.IsHintAvailable (IsShow);
		default:

			// TODO: Checking Hint.
			return _SGamePlay.IsHintAvailable (IsShow);
		}
    }
    #endregion

	void OnDisable()
	{
		if (!object.ReferenceEquals (Timing.Instance, null)) {
			// TODO: Call the invoke.
			Timing.KillCoroutines (Enums._GameScene [(int)GameManager.Instance.GameType]);
		}
	}
}


/// <summary>
/// The properties of card hint will be shown on the hud.
/// </summary>
[System.Serializable]
public struct HintValueDisplay
{
    // TODO: Card will be display.
    public CardBehaviour cardDisplay; 

    // TODO: moving to.
    public Vector3 positionTarget;
}


