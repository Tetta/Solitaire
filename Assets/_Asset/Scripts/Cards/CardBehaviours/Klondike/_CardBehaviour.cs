using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ========================================= KLONDIKE GAME ====================================== //

#region KLONDIKE

namespace KLONDIKE
{
	public class _CardBehaviour
	{
		// TODO: The card start.
		protected CardBehaviour card;

		public _CardBehaviour (CardBehaviour param)
		{
			card = param;
		}

		public void OnTouchBeginDrag()
		{
			// TODO: Turn off hint.
			HintDisplay.Instance.DisableHint();

			// TODO: Check if the card is using.
			if (card.stateTouched != Enums.StateTouch.None) {
				return;
			}

			// TODO: update the number of move.
			UIBehaviours.Instance.UpdateMove();

			// TODO: Set the state like begin.
			card.stateTouched = Enums.StateTouch.BeginDrag;

			// TODO: Set Up the information before following.
			card.BeginFollow ();
		}

		public void OnTouchDrag()
		{
			// TODO: Check if the card is using.
			if (card.stateTouched != Enums.StateTouch.BeginDrag && card.stateTouched != Enums.StateTouch.Drag) {
				return;
			}

			// TODO: Set the state is draging.
			card.stateTouched = Enums.StateTouch.Drag;

			// TODO: Follow the position.
			card.Follow ();
		}

		public void OnTouchEndDrag()
		{

			// TODO: Check if the card is using.
			if (card.stateTouched != Enums.StateTouch.Drag && card.stateTouched != Enums.StateTouch.BeginDrag) {

				// TODO: Break the function.
				return;
			}

			// TODO: Reset the state of touching.
			card.stateTouched = Enums.StateTouch.None;

			// TODO: Check the Playing zone.
			if (DoCheckPlayingZone ()) {

				// TODO: Break the functions.
				return;
			}

			// TODO: Back to the current position.
			card.Moving ( card.TargetPosition , card.parentTransform  , ()=>{

				// TODO: Distribute the follow cards.
				card.DistributeTheFollowCards();

				// TODO: Failed Collect;
				card.AnimationFailedCollect();
			});
		}

		protected bool DoCheckResultZone(bool IsMoving = true ){

			// TODO: Check start with point.
			int IsCardsFromTheCheckPoint = (int)Enums.Zone.None;

			// TODO: Get the id zone from old.
			var zoneOld = ResultZone.Instance.GetIdTransform (card);

			if (zoneOld == Enums.IdTransformCard.None) {

				// TODO: Get zone from hint zone.
				zoneOld = HintZone.Instance.GetIdTransform (card);

				if (zoneOld != Enums.IdTransformCard.None) {
				
					//TODO: Record the zone.
					IsCardsFromTheCheckPoint = (int)Enums.Zone.Hint;
				} else {
				
					zoneOld = PlayingZone.Instance.GetIdTransform (card);
				
					if (zoneOld != Enums.IdTransformCard.None) {

						IsCardsFromTheCheckPoint = (int)Enums.Zone.Play;
					}
				}

			} else {

				//TODO: Record the zone.
				IsCardsFromTheCheckPoint = (int)Enums.Zone.Result;
			}

			if (IsCardsFromTheCheckPoint == (int)Enums.Zone.Play) {

				if (HelperZone.Instance.GetCountCards() > 0) {

					// TODO: Break the function.
					return false;
				}
			}

			// TODO: Get the card find.
			CardBehaviour cardFind = null;

			// TODO: Get the zone of card.
			List < int > paramGet = new List<int>();

			// TODO: Check if this moving.
			if (IsMoving) {

				// TODO: Get the zone's id near this.
				paramGet.AddRange ( ResultZone.Instance.GetTheIdZonesMovingInside (card.transform.position) );
			} else {

				// TODO: Get all id of zones.
				paramGet.AddRange ( ResultZone.Instance.GetTheListIdZones ());
			}	

			// TODO: Loop to get the best zone.

			for (int i = 0; i < paramGet.Count; i++) {

				// TODO: Get the zone of card.
				Enums.IdTransformCard zoneGet = ( Enums.IdTransformCard )paramGet[i];

				// TODO: Create the point to check ready to join.
				var IsReadyToJoin = false;

				// TODO: Check if this zone is not none.
				if (zoneGet != Enums.IdTransformCard.None && (IsCardsFromTheCheckPoint == (int)Enums.Zone.Result && zoneOld != zoneGet || IsCardsFromTheCheckPoint != (int)Enums.Zone.Result)) {

					// TODO: Get the card.
					card.TargetBehaviour = ResultZone.Instance.GetTheLastCard (zoneGet);

					// TODO: Check if this card is not null.
					if (!object.ReferenceEquals (card.TargetBehaviour, null)) {

						// TODO: Check if this card can join with this zone.
						if (card.IsReadyToJoinZone (card.TargetBehaviour.GetProperties (), true)) {

							// TODO: Set ready.
							IsReadyToJoin = true;
						}
					} else {					

						if (card.GetValue () == (int)Enums.CardVariables.One) {

							// TODO: Set ready.
							IsReadyToJoin = true;
						} else {

							IsReadyToJoin = false;
						}
					}				
				
				}

				// TODO: The condition free not none.
				if (IsReadyToJoin) {			

					switch (IsCardsFromTheCheckPoint) {

					case (int)Enums.Zone.Hint:

						// TODO: Record Data.
						UndoSystem.Instance.Record(Enums.Zone.Hint, zoneOld , card , card.IsUnlocked());
						break;
					case (int)Enums.Zone.Result:

						// TODO: Record Data.
						UndoSystem.Instance.Record(Enums.Zone.Result, zoneOld , card , card.IsUnlocked());
						break;
					case (int)Enums.Zone.Play:

						// TODO: Record Data.
						UndoSystem.Instance.Record(Enums.Zone.Play, zoneOld , card , card.IsUnlocked());
						break;
					}			


					// TODO: Get the new position need to moving.
					card.TargetPosition = Helper.GetPositionInTheResultZone ( zoneGet, Enums.Direction.None, card.IsUnlocked ());
                    Debug.Log("card.POSITION: " + card.transform.position);
                    Debug.Log("card.TargetPosition: " + card.TargetPosition);
					// TODO: Moving to the new position.
					card.Moving (card.TargetPosition, () => {		

						// TODO: Set the new of parent.
						card.transform.SetParent (ResultZone.Instance.GetTransformCards (zoneGet));

						// TODO: Sort this card in the view.
						card.transform.SetAsLastSibling ();	

						if ( IsCardsFromTheCheckPoint != (int)Enums.Zone.Result && IsCardsFromTheCheckPoint != (int)Enums.Zone.None )
						{
							// TODO: Get the pool.
							var objectEmptyThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.PExploise);

							if ( !object.ReferenceEquals ( objectEmptyThing , null ))
							{						

								// TODO: Enable the gameobject.
								objectEmptyThing.gameObject.SetActive (true);

								// TODO: Set the position.
								objectEmptyThing.transform.position = card.TargetPosition;
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
									Helper.StringBulding.Clear();

									// TODO: Append the string.
									Helper.StringBulding.Append (Contains.ScoreResultCards + Contains.ScoreResultCards * HelperZone.Instance.GetCountCards());

									// TODO: Display the score.
									scriptFind.Show (Helper.StringBulding.ToString());

									// TODO: Enable the gameobject.
									textThing.gameObject.SetActive (true);
								}
							}

							// TODO: Update the score.
							Contains.Score += Contains.ScoreResultCards + Contains.ScoreResultCards * HelperZone.Instance.GetCountCards() ;

							// TODO: Update display score.
							UIBehaviours.Instance.UpdateScore();
						}

						// TODO: Unlocking the last card.
						PlayingZone.Instance.UnlockLastCard();

						// TODO: Update the state of all cards.
						PlayingZone.Instance.UpdateTheStateCardsInZone(zoneOld);
					});

					switch (IsCardsFromTheCheckPoint) {

					case (int)Enums.Zone.Hint:

						// TODO: Remove this card from zone old.
						HintZone.Instance.RemoveTheCard (zoneOld, card);

						// TODO: Sort the cards of hint zone.
						Helper.SortCards ( HintZone.Instance.GetTheListCards (Enums.IdTransformCard.TransformCards_B), Enums.Direction.Right , HintZone.Instance.GetDefaultPosition (Enums.IdTransformCard.TransformCards_B), 3);

						break;
					case (int)Enums.Zone.Result:

						// TODO: Remove this card.
						ResultZone.Instance.RemoveTheCard (zoneOld, card);
						break;
					case (int)Enums.Zone.Play:

						// TODO: Remove from zone old.
						PlayingZone.Instance.RemoveTheCard (zoneOld, card);
						break;
					}			

					// TODO: Add this card to the new zone.
					ResultZone.Instance.AddTheCard (zoneGet, card);

					return true;
				}
			}
			// TODO: Break the function.
			return false;
		}

		public bool DoCheckPlayingZone(bool IsMoving = true)
		{
			// TODO: Check the condition return.
			if (DoCheckResultZone (IsMoving)) {

				// TODO: Check the condition wining.
				if ( GamePlay.Instance.IsConditionWining ()){
					
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

				// TODO: Break the function.
				return true;
			}


			// TODO: Check start with point.
			int CardsFromTheCheckPoint = (int)Enums.Zone.None;

			// TODO: Get the old zone.
			var zoneOld = PlayingZone.Instance.GetIdTransform (card);

			// TODO: Get state zone from old zone.
			if (zoneOld == Enums.IdTransformCard.None) {

				zoneOld = HintZone.Instance.GetIdTransform (card);

				if (zoneOld == Enums.IdTransformCard.None) {
				
					// TODO: Get the id transform.
					zoneOld = ResultZone.Instance.GetIdTransform (card);
				

					if (zoneOld == Enums.IdTransformCard.None) {
					
					} else {
					
						CardsFromTheCheckPoint = (int)Enums.Zone.Result;
					}
				}else{
					CardsFromTheCheckPoint = (int)Enums.Zone.Hint ; 
				}
			} else {

				CardsFromTheCheckPoint = (int)Enums.Zone.Play;
			}

			// TODO: Get the zone of card.
			List < int > paramGet = new List<int>();

			// TODO: Check if this moving.
			if (IsMoving) {

				// TODO: Get the zone's id near this.
				paramGet.AddRange ( PlayingZone.Instance.GetTheIdZonesMovingInside (card.transform.position) );
			} else {

				// TODO: Get all id of zones.
				paramGet.AddRange (PlayingZone.Instance.GetTheListIdZones ());
			}	

			for (int i = 0; i < paramGet.Count; i++) {

				// TODO: Get the zone of card.
				Enums.IdTransformCard zoneGet = ( Enums.IdTransformCard )paramGet[i];

				// TODO: Create the point to check ready to join.
				var IsReadyToJoin = false;

				// TODO: Check if this zone is not none.
				if (zoneGet != Enums.IdTransformCard.None && ( CardsFromTheCheckPoint == (int)Enums.Zone.Play &&  zoneOld != zoneGet || CardsFromTheCheckPoint != (int)Enums.Zone.Play )) {

					// TODO: Get the card.
					card.TargetBehaviour = PlayingZone.Instance.GetTheLastCard (zoneGet);

					// TODO: Check if this card is not null.
					if (!object.ReferenceEquals (card.TargetBehaviour, null)) {

						// TODO: Check if this card can join with this zone.
						if (card.IsReadyToJoinZone (card.TargetBehaviour.GetProperties (), false , GameManager.Instance.GetModeGame() == Enums.ModeGame.Easy )) {
												
							// TODO: Set ready.
							IsReadyToJoin = true;						
						}
					} else {

						if (card.GetValue () == (int)Enums.CardVariables.King) {

							// TODO: Set ready.
							IsReadyToJoin = true;
						} else {
						
							IsReadyToJoin = false;
						}
					}

					// TODO: Check if ready to join.
					if (IsReadyToJoin) {

						switch (CardsFromTheCheckPoint) {
							
						case (int)Enums.Zone.Play:

							// TODO: Record Data.
							UndoSystem.Instance.Record(Enums.Zone.Play, zoneOld , card , card.IsUnlocked());
							break;
						case (int)Enums.Zone.Hint:

							// TODO: Record Data.
							UndoSystem.Instance.Record(Enums.Zone.Hint, zoneOld , card , card.IsUnlocked());
							break;
						case (int)Enums.Zone.Result:


							// TODO: Record Data.
							UndoSystem.Instance.Record(Enums.Zone.Result, zoneOld , card , card.IsUnlocked());
							break;
						}


						// TODO: Get the list cards.
						var cardsFollow = HelperZone.Instance.GetListCards();

						// TODO: Check start with point.
						bool IsReadyGetPoint = true;

						for (int j = cardsFollow.Count - 1; j > -1; j--)
						{               

							switch (CardsFromTheCheckPoint) {

							case (int)Enums.Zone.Play:

								// TODO: Record datas.
								UndoSystem.Instance.Record(Enums.Zone.Play, zoneOld, cardsFollow[j], cardsFollow[j].IsUnlocked(), true);
								break;
							case (int)Enums.Zone.Hint:

								// TODO: Record datas.
								UndoSystem.Instance.Record(Enums.Zone.Hint, zoneOld, cardsFollow[j], cardsFollow[j].IsUnlocked(), true);
								break;
							case (int)Enums.Zone.Result:

								// TODO: Record datas.
								UndoSystem.Instance.Record(Enums.Zone.Result, zoneOld, cardsFollow[j], cardsFollow[j].IsUnlocked(), true);
								break;
							}
						}

						switch (CardsFromTheCheckPoint) {

						case (int)Enums.Zone.Play:


							// TODO: Remove this card.
							PlayingZone.Instance.RemoveTheCard ( zoneOld , card );	
							break;
						case (int)Enums.Zone.Hint:

							// TODO: Remove this card.
							HintZone.Instance.RemoveTheCard (zoneOld, card);
							break;
						case (int)Enums.Zone.Result:

							// TODO: Remove this card.
							ResultZone.Instance.RemoveTheCard (zoneOld, card);
							break;
						}
								
						// TODO: Get the new position need to moving.
						card.TargetPosition = Helper.GetPositionInThePlayingZone ( zoneGet, Enums.Direction.Down, card.IsUnlocked ());
                        //Debug.Log("qq -- " + card.TargetPosition);

                        // TODO: Moving to the new position.
                        card.Moving (card.TargetPosition, () => {		

							// TODO: Set the new of parent.
							card.transform.SetParent (PlayingZone.Instance.GetTransformCards (zoneGet));

							// TODO: Sort this card in the view.
							card.transform.SetAsLastSibling ();								

							if ( IsReadyGetPoint )
							{
								if ( CardsFromTheCheckPoint == (int)Enums.Zone.Result )
								{

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
										Helper.StringBulding.Clear();

										// TODO: Append the string.
										Helper.StringBulding.Append ("-").Append(Contains.ScoreResultCards);

										// TODO: Display the score.
										scriptFind.Show (Helper.StringBulding.ToString());

										// TODO: Enable the gameobject.
										textThing.gameObject.SetActive (true);
									}
								}

								// TODO: Update the score.
								Contains.Score -= Contains.ScoreResultCards;
								}

								// TODO: Update display score.
								UIBehaviours.Instance.UpdateScore();
							}	

							// TODO: Reset all the card follow.
							card.DistributeTheFollowCards ();	

							switch (CardsFromTheCheckPoint) {
							case (int)Enums.Zone.Play:

								// TODO: Unlock the last card.
								PlayingZone.Instance.UnlockLastCard();

								// TODO: Update the state of cards.
								PlayingZone.Instance.UpdateTheStateCardsInZone(zoneOld);		
								break;
							}
						});

						// TODO: Add this card to the new zone.
						PlayingZone.Instance.AddTheCard (zoneGet, card);

						// TODO: Break the functions.
						return true;
					}
				}
			}

			return false;
		}
	}
}

#endregion

