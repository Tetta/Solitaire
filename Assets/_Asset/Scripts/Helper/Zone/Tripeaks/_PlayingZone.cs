using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace TRIPEAKS
{
	public class _PlayingZone {

		public void UpdateTheStateCardsInZone()
		{
			// TODO: Loop to get the zone.
			for (int i = 0; i < PlayingZone.Instance.zoneCards.Length; i++) {

				// TODO: update state of all cards.
				UpdateTheStateCardsInZone (PlayingZone.Instance.zoneCards [i].Id);
			}
		}

		public void UpdateTheStateCardsInZone(Enums.IdTransformCard id)
		{
			// TODO: Create the list of card.
			List < CardBehaviour > paramCheck = new List<CardBehaviour> ();

			// TODO: Get the list of cards.
			paramCheck.AddRange (PlayingZone.Instance.GetTheListCards (id)); 

			// TODO: Get the number of cards int the list.
			int length = paramCheck.Count; 

			// TODO: Create the cache of card.
			CardBehaviour cardCache;

			if (length > 0) {

				cardCache = paramCheck [length - 1];

				// TODO: Check if this unlocking.
				if (cardCache.IsUnlocked ()) {

					// TODO: can move this card.
					cardCache.UpdateReadyToUse (Enums.StateCard.None);
				}
			}

			// TODO: Loop to check.
			for (int i = length - 1; i >= 0; i--) {

				// TODO: Set the cache.
				cardCache = paramCheck [i];

				if (object.ReferenceEquals (cardCache, null)) {

					// TODO: Break the functions.
					throw new UnityException (Contains.NullExceptions);
				}

				// TODO: Check the state of card.
				if (!cardCache.IsUnlocked ()) {

					// TODO: Break the function if the current card is locking.
					break;
				}

				// TODO: Check if the lenght of card is smaller than 2.
				if (length > 1) {

					// TODO: Check if state of i == 0.
					if (i > 0) {

						// TODO: Get the condition to moving card.
						if (cardCache.GetStateCard() == Enums.StateCard.None && paramCheck [i - 1].IsUnlocked() && paramCheck [i - 1].GetProperties ().GetCardValue () == cardCache.GetProperties ().GetCardValue () + 1 && paramCheck [i - 1].GetProperties ().GetCardType () == cardCache.GetProperties ().GetCardType ()) {

							// TODO: can move this card.
							paramCheck [i - 1].UpdateReadyToUse (Enums.StateCard.None);

						} else {

							// TODO: can not move this card.
							paramCheck [i - 1].UpdateReadyToUse (Enums.StateCard.Locking);
						}
					}
				} 
			}
		}
			
		public HintValueDisplay GetHint()
		{
			// TODO: Create the list will be returned.
			HintValueDisplay paramOut = new HintValueDisplay();

			// TODO: Check the condition null.
			if (object.ReferenceEquals (GamePlay.Instance.cardSaveCache, null)) {

				// TODO: Return the value.
				return paramOut;
			}

			// TODO: Create the lenght of cards.
			int lenght = PlayingZone.Instance.zoneCards.Length;

			// TODO: Loop to check.
			for ( int i = 0; i < lenght; i++ )
			{
				// TODO: Create the cache to check the cards.
				var card = PlayingZone.Instance.GetTheLastCard(PlayingZone.Instance.zoneCards[i].Id);

				// TODO: Get the card save.
				var cardSave = GamePlay.Instance.cardSaveCache;

				// TODO: Check if the list cards is empty or null.
				if ( object.ReferenceEquals ( card , null ))
				{
					// TODO: next to another.
					continue;
				}        

				bool IsReadyAnotherCondition = true;
				bool IsReadyKingCondition = false;

				if (card.GetEnumsCard () == Enums.CardVariables.King && cardSave.GetEnumsCard () == Enums.CardVariables.One || card.GetEnumsCard () == Enums.CardVariables.One && cardSave.GetEnumsCard () == Enums.CardVariables.King) {

					// TODO: Set the another condition.
					IsReadyKingCondition = true;
				}

				switch (GameManager.Instance.GetModeGame ()) {	

				case Enums.ModeGame.Hard:

					if (card.GetTypeCards () != cardSave.GetTypeCards ()) {

						// TODO: Set the another condition.
						IsReadyAnotherCondition = false;
					}

					break;
				case Enums.ModeGame.Medium:

					if (!card.IsSameColorCard (cardSave.GetTypeCards ())) {

						// TODO: Set the another condition.
						IsReadyAnotherCondition = false;
					}

					break;
				}

				if ((cardSave.GetValue () + 1 == card.GetValue () || cardSave.GetValue () - 1 == card.GetValue ()) && IsReadyAnotherCondition || IsReadyKingCondition && IsReadyAnotherCondition) {

					// TODO: Set the card display.
					paramOut.cardDisplay = card;

					// TODO: Set the target position.
					paramOut.positionTarget = cardSave.TargetPosition;

					// TODO: break the state check
					break;
				}
			}

			// TODO: Return the list.
			return paramOut;
		}

		/// <summary>
		/// Unlocks the last card from all the arrays.
		/// </summary>
		public void UnlockLastCard()
		{
			// TODO: Create the value will be unlocked.
			CardBehaviour paramCheck = null;

			// TODO: Loop to check all the arrays.
			for (int i = 0; i < PlayingZone.Instance.zoneCards.Length; i++)
			{

				// TODO: Get the default of value.
				paramCheck = PlayingZone.Instance.GetTheLastCard(PlayingZone.Instance.zoneCards[i].Id);

				// TODO: Check it don't equal null.
				if (paramCheck != null)
				{

					// TODO: Records Unlock
					UndoSystem.Instance.Record(Enums.Zone.Play, Enums.IdTransformCard.None, paramCheck ,paramCheck.IsUnlocked(), true);


					// TODO: Unlock this cards.
					paramCheck.Unlock(true);
				}
			}
		}
	}
}
