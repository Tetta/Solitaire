using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton < GameManager > {

	// ======================== Variables ======================== //

	#region Variables

	/// <summary>
	/// The state game.
	/// </summary>
	protected Enums.StateGame stateGame = Enums.StateGame.None;

	/// <summary>
	/// The mode game.
	/// </summary>
	protected Enums.ModeGame ModeGame = Enums.ModeGame.None;

	/// <summary>
	/// The game.
	/// </summary>
	public Enums.GameScenes GameType = Enums.GameScenes.Tripeaks;

	#endregion

	// ======================== Functional ====================== //



	#region Functional 

	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected override void Awake ()
	{
		base.Awake ();

        Input.multiTouchEnabled = false;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected void Start()
	{
		if (PoolSystem.Instance == null) {
			
			throw new UnityException ("The game can not start!");
		}

		InitStart ();

		InitCards ();
	}

	#if UNITY_ANDROID

	protected void Update()
	{
			
		// TODO: Get the state of key code.
		if (Input.GetKeyDown (KeyCode.Escape)) {
		
			// TODO: Check if dialog is not null.
			if (!object.ReferenceEquals (DialogSystem.Instance.CurrentDialog, null)) {

				// TODO: Open Event Escape.
				DialogSystem.Instance.CurrentDialog.OnTouchEscape ();
			}
		}
	}
	#endif

    public void UpdateState(Enums.StateGame state)
    {
		this.stateGame = state;
    }

	public void UpdateModeGame(Enums.ModeGame mode)
	{
		this.ModeGame = mode;
	}

	#endregion

	// ====================== Init ============================ //

	#region Init

	/// <summary>
	/// Inits the cards.
	/// </summary>
	public void InitCards()
	{
		PoolSystem.Instance.ClearCards ();

		CardDataProperties[] cards =  DataSystem.Instance.GetCardsData ();

		int count = cards.Length;

		CardDataProperties card;

		for (int j = 0; j < 8; j++) {
			
			for (int i = 0; i < count; i++) {

				card = cards [i];

				if (!ReferenceEquals (card, null)) {
				
					InitCard (DataSystem.Instance.GetCardPrefab (), card);
				}
			}
		}
	}

	/// <summary>
	/// Inits the start.
	/// </summary>
	public void InitStart()
	{
		stateGame = Enums.StateGame.Start;
	}


	/// <summary>
	/// Inits the card.
	/// </summary>
	/// <param name="card">Card.</param>
	/// <param name="data">Data.</param>
	/// <param name="cardOnBoard">Card on board.</param>
	protected bool InitCard(CardBehaviour card, CardDataProperties data)
	{
		GameObject param = Instantiate (card.gameObject) as GameObject;

		if (param.GetComponent < CardBehaviour > () != null) {

			CardBehaviour paramBehaviour = param.GetComponent < CardBehaviour > ();

			paramBehaviour.Init (data);

			PoolSystem.Instance.ReturnToPool (paramBehaviour);

			return true;

		} else {
		
			Destroy (param);
		}

		return false;
	}
	#endregion


	// ======================= Helper ======================== //

	#region Helper

	/// <summary>
	/// Gets the state game.
	/// </summary>
	/// <returns>The state game.</returns>
	public Enums.StateGame GetStateGame()
	{
		return stateGame;
	}

	public Enums.ModeGame GetModeGame()
	{
		return ModeGame;
	}
		
	/// <summary>
	/// Determines whether this instance is game ready.
	/// </summary>
	/// <returns><c>true</c> if this instance is game ready; otherwise, <c>false</c>.</returns>
	public bool IsGameReady ()
	{
		return stateGame == Enums.StateGame.Playing;
	}

	/// <summary>
	/// Determines whether this instance is game end.
	/// </summary>
	/// <returns><c>true</c> if this instance is game end; otherwise, <c>false</c>.</returns>
	public bool IsGameEnd()
	{
		return stateGame == Enums.StateGame.GameOver;
	}
	#endregion
}
