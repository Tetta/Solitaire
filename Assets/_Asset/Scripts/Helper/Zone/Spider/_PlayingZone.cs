using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace SPIDER
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

		/// <summary>
		/// Determines whether this instance is completed A list cards the specified id.
		/// </summary>
		public bool DoCompletedAListCards(Enums.IdTransformCard id)
		{
			// TODO: Create the key.
			string idKey = id.ToString ();

			// TODO: Create the value will be returned.
			bool IsCompleted = false;

			// TODO: Create the list will get the cards completed.
			List < CardBehaviour > paramOut = new List<CardBehaviour> ();

			// TODO: Check the key exists.
			if (PlayingZone.Instance.cards.ContainsKey (idKey)) {

				// TODO: Create the list of cards.
				List < CardBehaviour > paramGet = null;

				// TODO: Get the list of cards from the zones.
				if (PlayingZone.Instance.cards.TryGetValue (idKey, out paramGet)) {

					// TODO: Get the number of cards.
					int count = paramGet.Count;

					// TODO: Check if the number of cards is bigger than 0.
					if (count > 0) {

						// TODO: Check if the last card is one.
						if (paramGet [count - 1].GetProperties().GetEnumCardValue() == Enums.CardVariables.One) {

							// TODO: Add the card.
							paramOut.Add (paramGet [count - 1]);

							// TODO: Set the value will be returned is true.
							IsCompleted = true;

							// Create the number of count select cards.
							int numberCount = 1;

							// TODO: Loop to check the condition.
							for (int i = count - 1; i > 0; i--) {							

								// TODO: Check if both of cards unlocked.
								if (paramGet [i - 1].IsUnlocked () && paramGet [i].IsUnlocked () && numberCount < Contains.numberCardEachType) {

									// TODO: Check if the value of two cards is difference 1 unit and has the same type of card.
									if (paramGet [i].GetValue () != paramGet [i - 1].GetValue () - 1 || paramGet [i].GetProperties ().GetCardType () != paramGet [i - 1].GetProperties ().GetCardType ()) {

										// TODO: Break the function if the condition is not right.
										IsCompleted = false;

										// TODO: Break the loop.
										break;
									}

									numberCount++;

									// TODO: Add the card.
									paramOut.Add(paramGet[i - 1]);

								} else {

									if (numberCount != Contains.numberCardEachType) {						

										// TODO: Break the function if the condition is not right.
										IsCompleted = false;
									}

									// TODO: Break the loop.
									break;
								}
							}
						}
					}
				}
			}

			// TODO: Doing complete with the condition.
			if (IsCompleted) {

				// TODO: Get the number of cards.
				int count = paramOut.Count;

				// TODO: Break the function if don't enough the cards.
				if (count == 0 || paramOut [count - 1].GetValue () != (int)Enums.CardVariables.King) {

					return false;
				}

				// TODO: Get the position moving.
				Vector3 position = Helper.GetPositionInTheResultZone ( Enums.IdTransformCard.TransformCards_A, Enums.Direction.Right, true);

				// TODO: Get the transform holder.
				Transform parent = ResultZone.Instance.GetTransformCards (Enums.IdTransformCard.TransformCards_A);

				// TODO: animation
				Timing.RunCoroutine(MovingEndCards(paramOut , position , parent , ()=> {

					// TODO: Unlock the last cards.
					UnlockLastCard();

					// TODO: Update the state of cards.
					UpdateTheStateCardsInZone(id);

					// TODO: Clear the undo
					UndoSystem.Instance.Clear();

				}), Enums.Tags.GamePlaying.ToString());
			}

			// TODO: Return the value.
			return IsCompleted;
		}

		IEnumerator < float > MovingEndCards(List < CardBehaviour > paramIn , Vector3 position, Transform parent , System.Action OnComplete = null)
		{
			// TODO: Get the number of cards.
			int count = paramIn.Count;

			// TODO: Waiting to end.
			GameManager.Instance.UpdateState(Enums.StateGame.Waiting);

			// TODO: Create the loop to get the cards wins.
			for (int i = count - 1; i > -1; i--)
			{
				// TODO: Remove the card.
				PlayingZone.Instance.RemoveTheCard(paramIn[i]);

				// TODO: Complete this cards.
				paramIn[i].UpdateReadyToUse(Enums.StateCard.Complete);

				// TODO: Update the target position.
				paramIn[i].TargetPosition = position;
                //Debug.Log("MovingEndCards1 -- " + paramIn[i].TargetPosition);

                // TODO: Moving the completed card to the position.
                paramIn[i].Moving(position, parent, ()=> {

					// TODO: Get the position.
					Vector3 positionRandom = position;

					// TODO: Add the position offset.
					positionRandom.x += Random.Range(-1f, 1f);

					// TODO: Add the position offset.
					positionRandom.y += Random.Range(-1f, 1f);

					// TODO: Get the pool.
					var objectEmptyThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.PExploise);

					if ( !object.ReferenceEquals ( objectEmptyThing , null ))
					{						

						// TODO: Enable the gameobject.
						objectEmptyThing.gameObject.SetActive (true);

						// TODO: Set the position.
						objectEmptyThing.transform.position = positionRandom;
					}

					var textThing = PoolSystem.Instance.GetFromPool (Enums.PoolType.TColorText);

					if ( !object.ReferenceEquals ( textThing , null ))
					{				
						// TODO: Set the position.
						textThing.transform.position = positionRandom;

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

					// TODO: Update the score.
					Contains.Score += Contains.ScoreResultCards;

					// TODO: Update display score.
					UIBehaviours.Instance.UpdateScore();				

				});

				// TODO: Add the cards into the transform cards A.
				ResultZone.Instance.AddTheCard(Enums.IdTransformCard.TransformCards_A, paramIn[i]);

				// TODO: Waiting to end animation.
				yield return Timing.WaitForSeconds (Contains.DurationDraw);
			}

			// TODO: Check null.
			if ( !object.ReferenceEquals ( OnComplete , null ))
			{
				// TODO: Doing the action.
				OnComplete();
			}

			// TODO: Check the state of game.
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

				PlayerData.BestScore = Contains.Score;

				PlayerData.BestMove = Contains.Moves;

				PlayerData.BestTime = Contains.Time;

				SoundSystems.Instance.PlayerMusic(Enums.MusicIndex.WinMusic);
			}
			else
			{

				// TODO: Update the state of Playing.
				GameManager.Instance.UpdateState(Enums.StateGame.Playing);
			}
		}


		public HintValueDisplay GetHint()
		{
			// TODO: Create the list will be returned.
			HintValueDisplay paramOut = new HintValueDisplay();

			// TODO: Create the lenght of cards.
			int lenght = PlayingZone.Instance.zoneCards.Length;

			// TODO: Create the flag, the condition to check moving cards.
			bool IsReadyToCheck = false;

			// TODO: Loop to check.
			for ( int i = 0; i < lenght; i++ )
			{
				// TODO: the condition to break the function.
				if (IsReadyToCheck)
				{
					// TODO: Break the function.
					break;
				}

				// TODO: Create the cache to check the cards.
				var paramCheck = new List<CardBehaviour>(PlayingZone.Instance.GetTheListCards(PlayingZone.Instance.zoneCards[i].Id));

				// TODO: Check if the list cards is empty or null.
				if ( object.ReferenceEquals ( paramCheck , null ) || paramCheck.Count == 0)
				{
					// TODO: next to another.
					continue;
				}        

				// TODO: Create the count of list.
				int count = paramCheck.Count;

				// TODO: Loop to get the cards.
				for ( int j = 0; j < count ; j ++ )
				{
					// TODO: the condition to break the function.
					if (IsReadyToCheck)
					{
						// TODO: Break the function.
						break;
					}

					// TODO: Create the cache of cards.
					var cardParam = paramCheck[j];

					// TODO: Check if the card is lock break.
					if (cardParam.IsUnlocked() == false || cardParam.GetStateCard() != Enums.StateCard.None)
					{
						continue;
					}            

					for ( int  h =  0;  h < lenght; h++ )
					{
						// TODO: Check if id is the same.
						if ( PlayingZone.Instance.zoneCards[h].Id == PlayingZone.Instance.zoneCards[i].Id)
						{
							continue;
						}

						// TODO: the condition to join.
						var IsReadyToJoin = false;

						// TODO: Get the card.
						var TargetBehaviour = PlayingZone.Instance.GetTheLastCard(PlayingZone.Instance.zoneCards[h].Id);

						// TODO: Check if this card is not null.
						if (!object.ReferenceEquals(TargetBehaviour, null))
						{

							// TODO: Check if this card can join with this zone.
							if (cardParam.IsReadyToJoinZone(TargetBehaviour.GetProperties()))
							{

								// TODO: Set ready.
								IsReadyToJoin = true;
							}
						}
						else
						{
							if ( j != 0 )
							{
								// TODO: Set ready.
								IsReadyToJoin = true;
							}
						}

						if ( IsReadyToJoin )
						{
							// TODO: Set the card will be displayed.
							paramOut.cardDisplay = cardParam;

							// TODO: Get the new position need to moving.
							paramOut.positionTarget =  Helper.GetPositionInThePlayingZone( PlayingZone.Instance.zoneCards[h].Id, Enums.Direction.Down, cardParam.IsUnlocked());

							// TODO: Break the flags.
							IsReadyToCheck = true;

							// TODO: Break the function.
							break;
						}
					}

					// TODO: Break the function.
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
