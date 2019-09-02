using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class HintZone : Zone < HintZone >
{
	#region Cache

	// TODO: Create the cache of zone.
	TRIPEAKS._HintZone _THintZone ;

	// TODO: Create the cache of zone.
	SPIDER._HintZone _SHintZone;

	// TODO: Create the cache of zone.
	KLONDIKE._HintZone _KHintZone;

	#endregion

	protected override void Awake ()
	{
		base.Awake ();

		switch (GameManager.Instance.GameType) {
		case Enums.GameScenes.Tripeaks:

			// TODO: Cache the zone.
			_THintZone = new TRIPEAKS._HintZone ();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Cache the zone.
			_SHintZone = new SPIDER._HintZone ();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Cache the zone.
			_KHintZone = new KLONDIKE._HintZone ();
			break;
		}
	}

	/// <summary>
	/// Draws the cards.
	/// </summary>
	public void DrawCards(int numberCards = Contains.numberColumn )
	{
		switch (GameManager.Instance.GameType) {
		case Enums.GameScenes.Tripeaks:

			// TODO: Draw the cards for TriPick game.
			_THintZone.DrawCards (numberCards);
			break;
		case Enums.GameScenes.Spider:

			// TODO: Draw the cards for Spider Game. 
			_SHintZone.DrawCards (numberCards);
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Draw the cards for Klondike.
			_KHintZone.DrawCards (numberCards);
			break;
		}
	}

	public HintValueDisplay GetHint()
	{
		switch (GameManager.Instance.GameType) {
		case Enums.GameScenes.Tripeaks:

				// TODO: Cache the zone.
			return _THintZone.GetHint();
		case Enums.GameScenes.Spider:

			// TODO: Cache the zone.
			return _SHintZone.GetHint();
		case Enums.GameScenes.Klondike:

			// TODO: Return the hint from klondike.
			return _KHintZone.GetHint ();
		}

		// TODO: Return null hint.
		return new HintValueDisplay ();
	}
}

