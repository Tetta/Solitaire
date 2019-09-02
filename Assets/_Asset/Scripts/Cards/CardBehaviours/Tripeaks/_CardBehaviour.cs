using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ==================================== TRI PICK GAME =============================== //

#region TRIPICK

namespace TRIPEAKS
{
	public class _CardBehaviour
	{
		// TODO: The card start.
		protected CardBehaviour card;

		public _CardBehaviour (CardBehaviour param)
		{
			// TODO: Set the param card.
			card = param;
		}

		public bool DoCheckPlayingZone(bool IsMoving = true)
		{
			// TODO: Check the condition null.
			if (object.ReferenceEquals (GamePlay.Instance.cardSaveCache, null)) {

				// TODO: Break the  function.
				return false;
			}

			// TODO: Get the cache of card.
			var cardSave = GamePlay.Instance.cardSaveCache;

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

			if ( (cardSave.GetValue () + 1 == card.GetValue () || cardSave.GetValue () - 1 == card.GetValue ())&& IsReadyAnotherCondition || IsReadyKingCondition && IsReadyAnotherCondition ) {

				Enums.IdTransformCard previousId = PlayingZone.Instance.GetIdTransform (card);

				// TODO: Record undo.
				UndoSystem.Instance.Record (Enums.Zone.Play, PlayingZone.Instance.GetIdTransform (card) , card, card.IsUnlocked ());

				// TODO: Get the dafult transform from zone.
				Transform parentTransform = ResultZone.Instance.GetTransformCards (Enums.IdTransformCard.TransformCards_A);

				// TODO: Get the position of card from zone.
				var position = Helper.GetPositionInTheResultZone (Enums.IdTransformCard.TransformCards_A, Enums.Direction.None, true); 

				// TODO: Set the target position x for the card.
				card.TargetPosition.x = position.x;

				// TODO: Set the target position y for the card.
				card.TargetPosition.y = position.y;

				// TODO: Set the target position z for the card.
				card.TargetPosition.z = position.z;
                //Debug.Log("--card.POSITION: " + card.transform.position);
                //Debug.Log("--card.TargetPosition: " + card.TargetPosition);
                // TODO: Moving the card to new position.
                card.Moving (card.TargetPosition, parentTransform, () => {

					if ( PlayingZone.Instance.IsEmptyCards (previousId))
					{
						Vector3 positionFind = PlayingZone.Instance.GetDefaultPosition(previousId);

						// TODO: Get the pool.
						var objectEmptyThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.PExploise);

						if ( !object.ReferenceEquals ( objectEmptyThing , null ))
						{						

							// TODO: Enable the gameobject.
							objectEmptyThing.gameObject.SetActive (true);

							// TODO: Set the position.
							objectEmptyThing.transform.position = positionFind;
						}

						var textEmptyThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.TColorText);

						if ( !object.ReferenceEquals ( textEmptyThing , null ))
						{				
							// TODO: Set the position.
							textEmptyThing.transform.position = positionFind;

							// TODO: Get the script.
							var scriptEmptyFind = textEmptyThing.GetComponent < TScoreDisplay >();

							// TODO: Check the condition to show.
							if ( !object.ReferenceEquals ( scriptEmptyFind , null ))
							{
								// TODO: Display the score.
								scriptEmptyFind.Show (Contains._ScoreResultClear);

								// TODO: Enable the gameobject.
								textEmptyThing.gameObject.SetActive (true);
							}
						}

						// TODO: Update the score.
						Contains.Score += Contains.ScoreResultClear;

						// TODO: Play the completed sound.
						SoundSystems.Instance.PlaySound (Enums.SoundIndex.Completed);
					}

					// TODO: Play the correct sound.
					SoundSystems.Instance.PlaySound (Enums.SoundIndex.Correct);

					// TODO: Get the pool.
					var objectThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.PExploise);

					if ( !object.ReferenceEquals ( objectThing , null ))
					{						

						// TODO: Enable the gameobject.
						objectThing.gameObject.SetActive (true);

						// TODO: Set the position.
						objectThing.transform.position = card.TargetPosition;
					}

					var textThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.TColorText);

					if ( !object.ReferenceEquals ( textThing , null ))
					{				
						// TODO: Set the position.
						textThing.transform.position = card.TargetPosition;

						// TODO: Get the script.
						var scriptFind = textThing.GetComponent < TScoreDisplay >();

						// TODO: Check the condition to show.
						if ( !object.ReferenceEquals ( scriptFind , null ))
						{
							// TODO: Display the score.
							scriptFind.Show (Contains._ScoreResultCards);

							// TODO: Enable the gameobject.
							textThing.gameObject.SetActive (true);

						}
					}

					// TODO: Unlock the last card.
					PlayingZone.Instance.UnlockLastCard ();

					// TODO: Update the state of card.
					PlayingZone.Instance.UpdateTheStateCardsInZone ();

					// TODO: Update the score.
					Contains.Score += Contains.ScoreResultCards;

					// TODO: Update the display score.
					UIBehaviours.Instance.UpdateScore ();	

					if ( GamePlay.Instance.IsConditionWining ())
					{
						// TODO: Update the state of GameOver.
						GameManager.Instance.UpdateState(Enums.StateGame.GameOver);

						// TODO: Showing the wining dialog.
						DialogSystem.Instance.ShowDialogWining();

						var fireworks = PoolSystem.Instance.GetFromPool (Enums.PoolType.Fireworks);

						if (!object.ReferenceEquals (fireworks, null)) {

							fireworks.transform.SetParent (GamePlay.Instance.transform);

							fireworks.transform.localPosition = Contains.Vector3Zero;

							fireworks.gameObject.SetActive (true);
						}

						// TODO: Save the score.
						PlayerData.BestScore = Contains.Score;

						// TODO: Save the best move.
						PlayerData.BestMove = Contains.Moves;

						// TODO: Save the best time.
						PlayerData.BestTime = Contains.Time;

						// TODO: Play wining music.
						SoundSystems.Instance.PlayerMusic(Enums.MusicIndex.WinMusic);
					}
				});			

				// TODO: Remove the card from list.
				PlayingZone.Instance.RemoveTheCard (card);

				// TODO: Add the card to the list result.
				ResultZone.Instance.AddTheCard (Enums.IdTransformCard.TransformCards_A, card);	

				// TODO: Set the card save.
				GamePlay.Instance.cardSaveCache = card;

				return true;
			}

			return false;
		}
	}
}

#endregion
