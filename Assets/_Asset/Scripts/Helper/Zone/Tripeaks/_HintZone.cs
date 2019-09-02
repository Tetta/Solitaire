using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace TRIPEAKS
{
	public class _HintZone {

		CardBehaviour cardCache;

		/// <summary>
		/// Draws the cards.
		/// </summary>
		public void DrawCards(int numberCards = Contains.numberColumn )
		{
			// TODO: Check if don't ready to draw the cards. Return.
			if (GameManager.Instance.GetStateGame () != Enums.StateGame.Playing) {

				// TODO: Break the functions.
				return;
			}

			// TODO: Update state game is drawing.
			GameManager.Instance.UpdateState (Enums.StateGame.Drawing);

			// TODO: Runing The Drawing Cards.
			Timing.RunCoroutine (_DrawCards (numberCards), Enums.Tags.GamePlaying.ToString ());
		}

		/// <summary>
		/// Draws the cards.
		/// </summary>
		IEnumerator < float > _DrawCards(int numberCards)
		{
			// TODO: Get the list Cards from the zone.
			cardCache  = HintZone.Instance.GetTheLastCard (Enums.IdTransformCard.TransformCards_A);

			// TODO: Check time ready to next card.
			bool IsReady = false;

			// TODO: Check the condition.
			if (!object.ReferenceEquals (cardCache, null)) {

				// TODO: Record undo.
				UndoSystem.Instance.Record (Enums.Zone.Hint, Enums.IdTransformCard.TransformCards_A, cardCache, cardCache.IsUnlocked ());

				// TODO: Get the dafult transform from zone.
				Transform parentTransform = ResultZone.Instance.GetTransformCards (Enums.IdTransformCard.TransformCards_A);

				// TODO: Get the position of card from zone.
				var position = Helper.GetPositionInTheResultZone (Enums.IdTransformCard.TransformCards_A, Enums.Direction.None, true); 

				// TODO: Set the target position x for the card.
				cardCache.TargetPosition.x = position.x;

				// TODO: Set the target position y for the card.
				cardCache.TargetPosition.y = position.y;

				// TODO: Set the target position z for the card.
				cardCache.TargetPosition.z = position.z;
                //Debug.Log("cardCache -- " + cardCache.TargetPosition);

                // TODO: Moving the card to new position.
                cardCache.Moving (cardCache.TargetPosition, parentTransform, () => {

					// TODO: play the sounds.
					SoundSystems.Instance.PlaySound(Enums.SoundIndex.Draw);

					cardCache.Unlock (true, () => {

						// TODO: Ready to draw another cards.
						IsReady = true;
					});
				});

				// TODO: Remove the cards from the list.
				HintZone.Instance.RemoveTheCard (cardCache);

				// TODO: Add the new card.
				ResultZone.Instance.AddTheCard (Enums.IdTransformCard.TransformCards_A, cardCache);

				// TODO: Set the card unlocked.
				GamePlay.Instance.cardSaveCache = cardCache;
			} else {

				IsReady = true;
			}

			while (IsReady == false )
			{
				yield return 0f;
			}

			// TODO: Check the current state of game.
			if (GameManager.Instance.GetStateGame () == Enums.StateGame.Drawing) {

				// TODO: Update the current state.
				GameManager.Instance.UpdateState (Enums.StateGame.Playing);
			}
		}

		public HintValueDisplay GetHint()
		{
			// TODO: Create the param will be returned.
			HintValueDisplay paramReturn = new HintValueDisplay();

			// TODO: Get the last cards.
			CardBehaviour lastCard = HintZone.Instance.GetTheLastCard(Enums.IdTransformCard.TransformCards_A);

			// TODO: Check null.
			if ( !object.ReferenceEquals  ( lastCard , null ))
			{

				// TODO: set the card.
				paramReturn.cardDisplay = lastCard;

				// TODO: Get the position.
				paramReturn.positionTarget = lastCard.TargetPosition;
			}

			// TODO: Return the value.
			return paramReturn;
		}
	}
}

