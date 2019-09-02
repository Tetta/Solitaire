using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace SPIDER
{
	public class _HintZone {

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
			// TODO: reset the undo.
			UndoSystem.Instance.Clear();

			// TODO: Get the list Cards from the zone.
			List < CardBehaviour > paramIn = HintZone.Instance.GetTheListCards (Enums.IdTransformCard.TransformCards_A);

			// TODO: Count the number of cards.
			int length = paramIn.Count;

			// TODO: Create the start transform.
			Enums.IdTransformCard idTransform = Enums.IdTransformCard.TransformCards_A;

			// TODO: Get the limit of cards will be drawn.
			int limit = Mathf.Clamp ( length - Contains.numberColumn, 0 , int.MaxValue );

			// TODO: Create the cache.
			CardBehaviour cardCache;

			// TODO: Create the cache of position.
			Vector3 position = Contains.Vector3Zero;

			// TODO: Loop to get the cards.
			for (int i = length - 1; i >= limit; i--) {

				// TODO: Set value of card.
				cardCache = paramIn [i];

				// TODO: Check if this card is null.
				if (cardCache == null) {

					// TODO: Throw the exception and stop the game.
					throw new UnityException (Contains.NullExceptions);
				}

				// TODO: Get the dafult transform from zone.
				Transform parentTransform = PlayingZone.Instance.GetTransformCards (idTransform);

				// TODO: Get the position of card from zone.
				position = Helper.GetPositionInThePlayingZone ( idTransform, Enums.Direction.Down , PlayingZone.Instance.IsLastCardUnlocked(idTransform)); 

				// TODO: Set the target position x for the card.
				cardCache.TargetPosition.x = position.x;

				// TODO: Set the target position y for the card.
				cardCache.TargetPosition.y = position.y;

				// TODO: Set the target position z for the card.
				cardCache.TargetPosition.z = position.z;

				// TODO: Moving the card to new position.
				cardCache.Moving ( cardCache.TargetPosition , parentTransform );

				// TODO: Adding the card to the list of cards.
				PlayingZone.Instance.AddTheCard (idTransform, cardCache);

				// TODO: Playing the sound.
				SoundSystems.Instance.PlaySound (Enums.SoundIndex.Draw);

				// TODO: Remove the cards from the list.
				HintZone.Instance.RemoveTheCard (cardCache);

				// TODO: Waiting until next draw.
				yield return Timing.WaitForSeconds (Contains.DurationDraw);

				// TODO: Get the next id of transform.
				idTransform = PlayingZone.Instance.GetNextIDTransform (idTransform);
			}

			// TODO: Unlocking the last cards in each arrays.
			PlayingZone.Instance.UnlockLastCard ();

			// TODO: update the state of all cards.
			PlayingZone.Instance.UpdateTheStateCardsInZone ();

			yield return 0f;

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
