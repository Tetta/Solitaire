using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playing zone.
/// </summary>
public class PlayingZone : Zone < PlayingZone >  {

	// ============================== Cache ==================================== //
	#region Cache

	// TODO: Get the cache of playing zone.
	TRIPEAKS._PlayingZone _TPlayingZone;

	// TODO: Get the cache of playing zone.
	SPIDER._PlayingZone _SPlayingZone;

	// TODO: Get the cache of playing zone.
	KLONDIKE._PlayingZone _KPlayingZone;

	#endregion


    protected override void Start()
    {
		switch (GameManager.Instance.GameType) {
			
		case Enums.GameScenes.Tripeaks:

			// TODO: Get the cache.
			_TPlayingZone = new TRIPEAKS._PlayingZone ();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Get the cache.
			_SPlayingZone = new SPIDER._PlayingZone ();
			break;

		case Enums.GameScenes.Klondike:

			// TODO: Get the cache.
			_KPlayingZone = new KLONDIKE._PlayingZone ();
			break;
		}

        base.Start();
    }

	#region Solitaire Condition

	public void UpdateTheStateCardsInZone()
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Get the cache.
			_TPlayingZone.UpdateTheStateCardsInZone();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Get the cache.
			_SPlayingZone.UpdateTheStateCardsInZone();
			break;

		case Enums.GameScenes.Klondike:

			// TODO: Update the state of cache.
			//_KPlayingZone.UpdateTheStateCardsInZone ();
			break;
		}
	}

	public void UpdateTheStateCardsInZone(Enums.IdTransformCard id)
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Get the cache.
			_TPlayingZone.UpdateTheStateCardsInZone(id);
			break;
		case Enums.GameScenes.Spider:

			// TODO: Get the cache.
			_SPlayingZone.UpdateTheStateCardsInZone(id);
			break;

		case Enums.GameScenes.Klondike:

			// TODO: Get the cache.
			//_KPlayingZone.UpdateTheStateCardsInZone (id);
			break;
		}
	}

	/// <summary>
	/// Determines whether this instance is completed A list cards the specified id.
	/// </summary>
	public bool DoCompletedAListCards(Enums.IdTransformCard id)
	{
		switch (GameManager.Instance.GameType) {
		case Enums.GameScenes.Spider:

			// TODO: Get the cache.
			return _SPlayingZone.DoCompletedAListCards(id);
		case Enums.GameScenes.Klondike:

			// TODO: Completed the list cards.
			return _KPlayingZone.DoCompletedAListCards (id);
		}

		// TODO: return the value.
		return false;
	}
     
    public HintValueDisplay GetHint()
    {
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Get the cache.
			return _TPlayingZone.GetHint();
		case Enums.GameScenes.Spider:

			// TODO: Get the cache.
			return _SPlayingZone.GetHint();

		case Enums.GameScenes.Klondike:

			// TODO: Get the hint.
			return _KPlayingZone.GetHint ();
		}

		// TODO: Return the value.
		return new HintValueDisplay ();
    }

    /// <summary>
    /// Unlocks the last card from all the arrays.
    /// </summary>
    public void UnlockLastCard()
    {
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Get the cache.
			_TPlayingZone.UnlockLastCard ();

			break;
		case Enums.GameScenes.Spider:

			// TODO: Get the cache.
			_SPlayingZone.UnlockLastCard ();

			break;
		case Enums.GameScenes.Klondike:

			// TODO: Unlock the last cards.
			_KPlayingZone.UnlockLastCard ();
			break;
		}
    }
    #endregion
}
