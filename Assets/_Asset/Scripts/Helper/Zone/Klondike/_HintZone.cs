using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace KLONDIKE
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

			// TODO: Update the move.
			UIBehaviours.Instance.UpdateMove (1, true);

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

				// TODO: Add the arrays.
				var arrayCards = HintZone.Instance.GetTheListCards (Enums.IdTransformCard.TransformCards_B);

				// TODO: Sort the cards.
				Helper.SortCards (arrayCards, Enums.Direction.Right, HintZone.Instance.GetDefaultPosition (Enums.IdTransformCard.TransformCards_B));

				// TODO: Get the dafult transform from zone.
				Transform parentTransform = HintZone.Instance.GetTransformCards (Enums.IdTransformCard.TransformCards_B);

				// TODO: Get the position of card from zone.
				var position = Helper.GetPositionInTheHintZone (Enums.IdTransformCard.TransformCards_B, Enums.Direction.Right, true); 

				// TODO: Set the target position x for the card.
				cardCache.TargetPosition.x = position.x;

				// TODO: Set the target position y for the card.
				cardCache.TargetPosition.y = position.y;

				// TODO: Set the target position z for the card.
				cardCache.TargetPosition.z = position.z; 

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
				HintZone.Instance.AddTheCard (Enums.IdTransformCard.TransformCards_B, cardCache);

				// TODO: Set the card unlocked.
				GamePlay.Instance.cardSaveCache = cardCache;
			} else {

				// TODO: Get the arrays.
				var arrayCards = new List < CardBehaviour > ( HintZone.Instance.GetTheListCards (Enums.IdTransformCard.TransformCards_B) );

				if (!object.ReferenceEquals (arrayCards, null)) {

					// TODO: Get the trasnform A.
					var transformA = HintZone.Instance.GetTransformCards (Enums.IdTransformCard.TransformCards_A);				

					for (int i = arrayCards.Count - 1; i > -1; i--) {

						UndoSystem.Instance.Record (Enums.Zone.Hint, Enums.IdTransformCard.TransformCards_B, arrayCards [i], arrayCards [i].IsUnlocked (), i != arrayCards.Count - 1 , false);

						arrayCards [i].TargetPosition = HintZone.Instance.GetDefaultPosition (Enums.IdTransformCard.TransformCards_A);

						// TODO: Lock the cards.
						arrayCards [i].Lock ();

						// TODO: Get the 
						arrayCards [i].Moving (arrayCards [i].TargetPosition, HintZone.Instance.GetTransformCards (Enums.IdTransformCard.TransformCards_A), null, true);

						// TODO: Remove the old cards.
						HintZone.Instance.RemoveTheCard (Enums.IdTransformCard.TransformCards_B, arrayCards [i]);

						// TODO: Set the default list card.
						HintZone.Instance.AddTheCard (Enums.IdTransformCard.TransformCards_A, arrayCards [i]);
					}

					// TODO: Return waiting time.
					yield return Timing.WaitForSeconds (1f);

					IsReady = true;

				} else {
					
					IsReady = true;
				}			
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

			// ============================================ LAST CARD FROM B ==================================== //

			// TODO: Get the last cards.
			CardBehaviour lastCard = HintZone.Instance.GetTheLastCard ( Enums.IdTransformCard.TransformCards_B );

			var isReadyToJoin = false;

			// TODO: Get the target position.
			var positionTarget = Contains.Vector3Zero;

			// ============================================ DOING LAST CARD WITH RESULT ZONE ==================== //
			if (!object.ReferenceEquals (lastCard, null)) {

				// TODO: Get the list of id.
				var zoneCards = ResultZone.Instance.GetTheListIdZones ();

				// TODO: Get the cache of card from Result zone.
				CardBehaviour cardParamFromResultZone = null;

				for (int i = 0; i < zoneCards.Count; i++) {

					// TODO: Get the last card in playing zone.
					cardParamFromResultZone = ResultZone.Instance.GetTheLastCard ((Enums.IdTransformCard)zoneCards [i]);

					// TODO: Get the condition null.
					if (!object.ReferenceEquals (cardParamFromResultZone, null)) {

						// TODO: Check the condition to join.
						if (lastCard.IsReadyToJoinZone (cardParamFromResultZone.GetProperties (), true)) {

							isReadyToJoin = true;

							// TODO: Get the default position.
							positionTarget = cardParamFromResultZone.TargetPosition;

							break;
						}
					} else {

						if (lastCard.GetValue () == (int)Enums.CardVariables.One) {

							isReadyToJoin = true;

							// TODO: Get the default position.
							positionTarget = Helper.GetPositionInTheResultZone ((Enums.IdTransformCard)zoneCards [i] , Enums.Direction.None ,true );

							break;
						}
					}
				}
			}

			// =========================================== DOING LAST CARD WITH PLAYING ZONE ==================== //

			if (!object.ReferenceEquals (lastCard, null) && isReadyToJoin == false) {

				// TODO: Get the list of cards.
				var zoneCards = PlayingZone.Instance.GetTheListIdZones ();

				// TODO: Get the cache of card from playing zone.
				CardBehaviour cardParamFromPlayingZone = null;

				for (int i = 0; i < zoneCards.Count; i++) {

					// TODO: Get the last card in playing zone.
					cardParamFromPlayingZone = PlayingZone.Instance.GetTheLastCard ((Enums.IdTransformCard)zoneCards [i]);

					// TODO: Get the condition null.
					if (!object.ReferenceEquals (cardParamFromPlayingZone, null)) {
										

						// TODO: Check the condition to join.
						if (lastCard.IsReadyToJoinZone (cardParamFromPlayingZone.GetProperties () , false , GameManager.Instance.GetModeGame() == Enums.ModeGame.Easy )) {

							isReadyToJoin = true;

							// TODO: Get the default position.
							positionTarget = cardParamFromPlayingZone.TargetPosition;

							break;
						} 
					} else {
						if (lastCard.GetValue () == (int)Enums.CardVariables.King) {

							isReadyToJoin = true;

							// TODO: Get the default position.
							positionTarget = Helper.GetPositionInThePlayingZone ((Enums.IdTransformCard)zoneCards [i] , Enums.Direction.None ,true );

							break;
						}
					}
				}
			}

			// ============================================ LAST CARD FROM A ==================================== //

			if (isReadyToJoin == false) {
				lastCard = HintZone.Instance.GetTheLastCard (Enums.IdTransformCard.TransformCards_A);
                
				// TODO: Get the default position.
				if (lastCard != null) positionTarget = lastCard.TargetPosition;
			}

			// TODO: Check null.
			if ( !object.ReferenceEquals  ( lastCard , null ))
			{
				// TODO: set the card.
				paramReturn.cardDisplay = lastCard;

				// TODO: Set the position.
				paramReturn.positionTarget = positionTarget;
			}

			// TODO: Return the value.
			return paramReturn;
		}

        public CardBehaviour GetHint2()
        {
            //first

            // TODO: Create the param will be returned.
            HintValueDisplay paramReturn = new HintValueDisplay();

            // ============================================ LAST CARD FROM B ==================================== //

            // TODO: Get the last cards.
            CardBehaviour lastCard = HintZone.Instance.GetTheLastCard(Enums.IdTransformCard.TransformCards_B);
            
            var isReadyToJoin = false;

            // TODO: Get the target position.
            var positionTarget = Contains.Vector3Zero;

            // ============================================ DOING LAST CARD WITH RESULT ZONE ==================== //
            if (!object.ReferenceEquals(lastCard, null))
            {

                // TODO: Get the list of id.
                var zoneCards = ResultZone.Instance.GetTheListIdZones();

                // TODO: Get the cache of card from Result zone.
                CardBehaviour cardParamFromResultZone = null;

                for (int i = 0; i < zoneCards.Count; i++)
                {

                    // TODO: Get the last card in playing zone.
                    cardParamFromResultZone = ResultZone.Instance.GetTheLastCard((Enums.IdTransformCard)zoneCards[i]);

                    // TODO: Get the condition null.
                    if (!object.ReferenceEquals(cardParamFromResultZone, null))
                    {

                        // TODO: Check the condition to join.
                        if (lastCard.IsReadyToJoinZone(cardParamFromResultZone.GetProperties(), true))
                        {

                            isReadyToJoin = true;

                            // TODO: Get the default position.
                            positionTarget = cardParamFromResultZone.TargetPosition;
                            return lastCard;
                            break;
                        }
                    }
                    else
                    {

                        if (lastCard.GetValue() == (int)Enums.CardVariables.One)
                        {

                            isReadyToJoin = true;

                            // TODO: Get the default position.
                            positionTarget = Helper.GetPositionInTheResultZone((Enums.IdTransformCard)zoneCards[i], Enums.Direction.None, true);
                            return lastCard;
                            break;
                        }
                    }
                }
            }

            // =========================================== DOING LAST CARD WITH PLAYING ZONE ==================== //

            if (!object.ReferenceEquals(lastCard, null) && isReadyToJoin == false)
            {

                // TODO: Get the list of cards.
                var zoneCards = PlayingZone.Instance.GetTheListIdZones();

                // TODO: Get the cache of card from playing zone.
                CardBehaviour cardParamFromPlayingZone = null;

                for (int i = 0; i < zoneCards.Count; i++)
                {

                    // TODO: Get the last card in playing zone.
                    cardParamFromPlayingZone = PlayingZone.Instance.GetTheLastCard((Enums.IdTransformCard)zoneCards[i]);

                    // TODO: Get the condition null.
                    if (!object.ReferenceEquals(cardParamFromPlayingZone, null))
                    {


                        // TODO: Check the condition to join.
                        if (lastCard.IsReadyToJoinZone(cardParamFromPlayingZone.GetProperties(), false, GameManager.Instance.GetModeGame() == Enums.ModeGame.Easy))
                        {

                            isReadyToJoin = true;

                            // TODO: Get the default position.
                            positionTarget = cardParamFromPlayingZone.TargetPosition;
                            return lastCard;
                            break;
                        }
                    }
                    else
                    {
                        if (lastCard.GetValue() == (int)Enums.CardVariables.King)
                        {

                            isReadyToJoin = true;

                            // TODO: Get the default position.
                            positionTarget = Helper.GetPositionInThePlayingZone((Enums.IdTransformCard)zoneCards[i], Enums.Direction.None, true);
                            return lastCard;
                            break;
                        }
                    }
                }
            }

            // ============================================ LAST CARD FROM A ==================================== //

            //----------------------------------------------------------------------------------
            // TODO: Create the param will be returned.
             paramReturn = new HintValueDisplay();

            // ============================================ LAST CARD FROM B ==================================== //
             lastCard = HintZone.Instance.GetTheLastCard(Enums.IdTransformCard.TransformCards_B);

            // TODO: Get the last cards.

            var arrayCards = HintZone.Instance.GetTheListCards(Enums.IdTransformCard.TransformCards_A);
            foreach (CardBehaviour card in arrayCards)
            {
                lastCard = card;

                 isReadyToJoin = false;

                // TODO: Get the target position.
                 positionTarget = Contains.Vector3Zero;

                // ============================================ DOING LAST CARD WITH RESULT ZONE ==================== //
                if (!object.ReferenceEquals(lastCard, null))
                {

                    // TODO: Get the list of id.
                    var zoneCards = ResultZone.Instance.GetTheListIdZones();

                    // TODO: Get the cache of card from Result zone.
                    CardBehaviour cardParamFromResultZone = null;

                    for (int i = 0; i < zoneCards.Count; i++)
                    {

                        // TODO: Get the last card in playing zone.
                        cardParamFromResultZone = ResultZone.Instance.GetTheLastCard((Enums.IdTransformCard)zoneCards[i]);

                        // TODO: Get the condition null.
                        if (!object.ReferenceEquals(cardParamFromResultZone, null))
                        {

                            // TODO: Check the condition to join.
                            if (lastCard.IsReadyToJoinZone(cardParamFromResultZone.GetProperties(), true))
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = cardParamFromResultZone.TargetPosition;
                                return lastCard;
                                break;
                            }
                        }
                        else
                        {

                            if (lastCard.GetValue() == (int)Enums.CardVariables.One)
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = Helper.GetPositionInTheResultZone((Enums.IdTransformCard)zoneCards[i], Enums.Direction.None, true);
                                return lastCard;
                                break;
                            }
                        }
                    }
                }

                // =========================================== DOING LAST CARD WITH PLAYING ZONE ==================== //

                if (!object.ReferenceEquals(lastCard, null) && isReadyToJoin == false)
                {

                    // TODO: Get the list of cards.
                    var zoneCards = PlayingZone.Instance.GetTheListIdZones();

                    // TODO: Get the cache of card from playing zone.
                    CardBehaviour cardParamFromPlayingZone = null;

                    for (int i = 0; i < zoneCards.Count; i++)
                    {

                        // TODO: Get the last card in playing zone.
                        cardParamFromPlayingZone = PlayingZone.Instance.GetTheLastCard((Enums.IdTransformCard)zoneCards[i]);

                        // TODO: Get the condition null.
                        if (!object.ReferenceEquals(cardParamFromPlayingZone, null))
                        {


                            // TODO: Check the condition to join.
                            if (lastCard.IsReadyToJoinZone(cardParamFromPlayingZone.GetProperties(), false, GameManager.Instance.GetModeGame() == Enums.ModeGame.Easy))
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = cardParamFromPlayingZone.TargetPosition;
                                return lastCard;
                                break;
                            }
                        }
                        else
                        {
                            if (lastCard.GetValue() == (int)Enums.CardVariables.King)
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = Helper.GetPositionInThePlayingZone((Enums.IdTransformCard)zoneCards[i], Enums.Direction.None, true);
                                return lastCard;
                                break;
                            }
                        }
                    }
                }

                // ============================================ LAST CARD FROM A ==================================== //




            }

            arrayCards = HintZone.Instance.GetTheListCards(Enums.IdTransformCard.TransformCards_B);
            foreach (CardBehaviour card in arrayCards)
            {
                lastCard = card;

                 isReadyToJoin = false;

                // TODO: Get the target position.
                 positionTarget = Contains.Vector3Zero;

                // ============================================ DOING LAST CARD WITH RESULT ZONE ==================== //
                if (!object.ReferenceEquals(lastCard, null))
                {

                    // TODO: Get the list of id.
                    var zoneCards = ResultZone.Instance.GetTheListIdZones();

                    // TODO: Get the cache of card from Result zone.
                    CardBehaviour cardParamFromResultZone = null;

                    for (int i = 0; i < zoneCards.Count; i++)
                    {

                        // TODO: Get the last card in playing zone.
                        cardParamFromResultZone = ResultZone.Instance.GetTheLastCard((Enums.IdTransformCard)zoneCards[i]);

                        // TODO: Get the condition null.
                        if (!object.ReferenceEquals(cardParamFromResultZone, null))
                        {

                            // TODO: Check the condition to join.
                            if (lastCard.IsReadyToJoinZone(cardParamFromResultZone.GetProperties(), true))
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = cardParamFromResultZone.TargetPosition;
                                return lastCard;
                                break;
                            }
                        }
                        else
                        {

                            if (lastCard.GetValue() == (int)Enums.CardVariables.One)
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = Helper.GetPositionInTheResultZone((Enums.IdTransformCard)zoneCards[i], Enums.Direction.None, true);
                                return lastCard;
                                break;
                            }
                        }
                    }
                }

                // =========================================== DOING LAST CARD WITH PLAYING ZONE ==================== //

                if (!object.ReferenceEquals(lastCard, null) && isReadyToJoin == false)
                {

                    // TODO: Get the list of cards.
                    var zoneCards = PlayingZone.Instance.GetTheListIdZones();

                    // TODO: Get the cache of card from playing zone.
                    CardBehaviour cardParamFromPlayingZone = null;

                    for (int i = 0; i < zoneCards.Count; i++)
                    {

                        // TODO: Get the last card in playing zone.
                        cardParamFromPlayingZone = PlayingZone.Instance.GetTheLastCard((Enums.IdTransformCard)zoneCards[i]);

                        // TODO: Get the condition null.
                        if (!object.ReferenceEquals(cardParamFromPlayingZone, null))
                        {


                            // TODO: Check the condition to join.
                            if (lastCard.IsReadyToJoinZone(cardParamFromPlayingZone.GetProperties(), false, GameManager.Instance.GetModeGame() == Enums.ModeGame.Easy))
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = cardParamFromPlayingZone.TargetPosition;
                                return lastCard;
                                break;
                            }
                        }
                        else
                        {
                            if (lastCard.GetValue() == (int)Enums.CardVariables.King)
                            {

                                isReadyToJoin = true;

                                // TODO: Get the default position.
                                positionTarget = Helper.GetPositionInThePlayingZone((Enums.IdTransformCard)zoneCards[i], Enums.Direction.None, true);
                                return lastCard;
                                break;
                            }
                        }
                    }
                }

                // ============================================ LAST CARD FROM A ==================================== //



            }



            return null;
        }
    }
}
