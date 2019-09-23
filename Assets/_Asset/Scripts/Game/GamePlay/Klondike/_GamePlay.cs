using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace KLONDIKE
{
	public class _GamePlay {

		// =============================== Variables =============================== //

		/// <summary>
		/// The cards get.
		/// </summary>
		public List < CardBehaviour > cardsGet;

		#region Functional System.

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void InitStart()
		{
			// Runing the reset functions before start the games.
			Reset ();

			// TODO: Draw cards, check status game, runing the game.
			Timing.RunCoroutine (PreparingGameHandle (), Enums.Tags.GamePlaying.ToString());

			// TODO: Show the interstitial ad.
			if ( Contains.IsReadyShowAds )
			{
				AdSystem.Instance.ShowInterstitialAd();
			}

			// TODO: Show the banner ads.
			if ( AdSystem.Instance.IsBannerShowed == false)
			{
				AdSystem.Instance.ShowBanner();
			}
		}

		#endregion

		#region Init System

		/// <summary>
		/// Reset this instance.
		/// </summary>
		protected void Reset()
		{
			// TODO: Reset the number of move to zero.
			Contains.Moves = 0;

			// TODO: Reset the score to zero.
			Contains.Score = 0;

			if (UIBehaviours.Instance != null) {

				UIBehaviours.Instance.UpdateMove (0);

				UIBehaviours.Instance.UpdateScore ();
			}

			// TODO: Set default of list cards.
			cardsGet = new List<CardBehaviour> ();

			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: Update the state of games.
				GameManager.Instance.UpdateState (Enums.StateGame.None);
			}
		}

		public void StopTimingHandle()
		{
			// TODO: Stop all the coroutine with this gameobject.
			Timing.KillCoroutines(Enums.Tags.GamePlaying.ToString());

			// TODO: Check if the PoolSystem exists.
			if (!object.ReferenceEquals ( PoolSystem.Instance , null)) {

				int count = cardsGet.Count;

				CardBehaviour cardParam;

				// TODO: Loop with number of cards.
				for (int i = 0; i < count; i++) {

					// TODO: Create the cache.
					cardParam = cardsGet[i];

					// TODO: Update the state of locking.
					cardParam.UpdateReadyToUse(Enums.StateCard.None);

					// TODO: Killing the action.
					cardParam.StopMoving ();

					// TODO: Lock the cards.
					cardParam.Lock();

					// TODO: Return all cards to pool.
					PoolSystem.Instance.ReturnToPool (cardParam);
				}

				// TODO: Clear all cards from list.
				cardsGet.Clear ();
			}

            if (!DialogNewGame.isShown) DialogNewGame.isShown = true;
            else
            if (!object.ReferenceEquals (GameManager.Instance, null)) {

				// TODO: Set the total played.
				PlayerData.TotalPlayed++;

				// TODO: Save the datas.
				PlayerData.Save ();
			}
		}

		/// <summary>
		/// Init this instance.
		/// </summary>
		protected void Init()
		{

            Debug.Log("Klondike init");
            // TODO: Create the default list will get the cards.
			List < CardBehaviour > paramGet = new List<CardBehaviour>();

			// TODO: Check the current of mode.
			switch (GameManager.Instance.GetModeGame()) {
			case Enums.ModeGame.Easy:

				// TODO: Random color of cards for 1 suit.
				Enums.CardType type = (Enums.CardType)Random.Range ((int)Enums.CardType.Club, (int)Enums.CardType.Spade + 1);

				// TODO: Loop 8 times and get the cards.
				for (int i = 0; i < 4; i++) {

					// TODO: Clear the old of cards in the list.
					paramGet.Clear ();

					// TODO: Get the cards from the pool.
					paramGet.AddRange (PoolSystem.Instance.GetCardsColor (type));

					// TODO: Check the number of cards from list. 
					if (paramGet.Count > 0) {

						// TODO: Add the list of cards from the pool.
						cardsGet.AddRange (paramGet);
					} else {

						// TODO: Show the error if we don't have enough the cards to start the game.
						throw new UnityException ("We don't have enough the cards to start the game.");
					}
				}

				break;
			case Enums.ModeGame.Medium:

				// TODO: Create enum of Card.
				Enums.CardType type_Black;

				// TODO: Random value.
				if (Random.Range (0, 2) == 1) {

					// TODO: Set type of card is club.
					type_Black = Enums.CardType.Club;
				} else {

					// TODO: Set type of card is Spade.
					type_Black = Enums.CardType.Spade;
				}

				// TODO: Create enum of Card.
				Enums.CardType type_Red;

				// TODO: Random value.
				if (Random.Range (0, 2) == 1) {

					// TODO: Set type of card is Heart.
					type_Red = Enums.CardType.Heart;
				} else {

					// TODO: Set type of card is Diamond.
					type_Red = Enums.CardType.Diamond;
				}

				//TODO: Loop 4 times to get the cards.
				for (int i = 0; i < 2; i++) {

					// TODO: Clear the old of cards in the list.
					paramGet.Clear ();

					// TODO: Get the cards from the pool.
					paramGet.AddRange (PoolSystem.Instance.GetCardsColor (type_Black));

					// TODO: Check the number of cards from list. 
					if (paramGet.Count > 0) {

						// TODO: Add the list of cards from the pool.
						cardsGet.AddRange (paramGet);
					} else {

						// TODO: Show the error if we don't have enough the cards to start the game.
						throw new UnityException ("We don't have enough the cards to start the game.");
					}
				}

				// TODO: Loop 4 times to get the cards.
				for (int i = 0; i < 2; i++) {

					// TODO: Clear the old of cards in the list.
					paramGet.Clear ();

					// TODO: Get the cards from the pool.
					paramGet.AddRange (PoolSystem.Instance.GetCardsColor (type_Red));

					// TODO: Check the number of cards from list. 
					if (paramGet.Count > 0) {

						// TODO: Add the list of cards from the pool.
						cardsGet.AddRange (paramGet);
					} else {

						// TODO: Show the error if we don't have enough the cards to start the game.
						throw new UnityException ("We don't have enough the cards to start the game.");
					}
				}

				break;
			case Enums.ModeGame.Hard:

				// TODO: Clear the old of cards in the list.
				paramGet.Clear ();

				// TODO: Get the cards from the pool.
				paramGet.AddRange (PoolSystem.Instance.GetCardsColor (Enums.CardType.Club));

				// TODO: Check the number of cards from list. 
				if (paramGet.Count > 0) {

					// TODO: Add the list of cards from the pool.
					cardsGet.AddRange (paramGet);
				} else {

					// TODO: Show the error if we don't have enough the cards to start the game.
					throw new UnityException ("We don't have enough the cards to start the game.");
				}			

				// TODO: Clear the old of cards in the list.
				paramGet.Clear ();

				// TODO: Get the cards from the pool.
				paramGet.AddRange (PoolSystem.Instance.GetCardsColor (Enums.CardType.Heart));

				// TODO: Check the number of cards from list. 
				if (paramGet.Count > 0) {

					// TODO: Add the list of cards from the pool.
					cardsGet.AddRange (paramGet);
				} else {

					// TODO: Show the error if we don't have enough the cards to start the game.
					throw new UnityException ("We don't have enough the cards to start the game.");
				}			


				// TODO: Clear the old of cards in the list.
				paramGet.Clear ();

				// TODO: Get the cards from the pool.
				paramGet.AddRange (PoolSystem.Instance.GetCardsColor (Enums.CardType.Spade));

				// TODO: Check the number of cards from list. 
				if (paramGet.Count > 0) {

					// TODO: Add the list of cards from the pool.
					cardsGet.AddRange (paramGet);
				} else {

					// TODO: Show the error if we don't have enough the cards to start the game.
					throw new UnityException ("We don't have enough the cards to start the game.");
				}			


				// TODO: Clear the old of cards in the list.
				paramGet.Clear ();

				// TODO: Get the cards from the pool.
				paramGet.AddRange (PoolSystem.Instance.GetCardsColor (Enums.CardType.Diamond));

				// TODO: Check the number of cards from list. 
				if (paramGet.Count > 0) {

					// TODO: Add the list of cards from the pool.
					cardsGet.AddRange (paramGet);
				} else {

					// TODO: Show the error if we don't have enough the cards to start the game.
					throw new UnityException ("We don't have enough the cards to start the game.");
				}

				break;
			}

			// TODO: Get the total of cards in the list.
			int count = cardsGet.Count;

			// TODO: Create the cache of card.
			CardBehaviour paramCache;

			// TODO: Loop and set the item's properties.
			for (int i = 0; i < cardsGet.Count; i++) {

				// TODO: Set the default of card with the list cards.
				paramCache = cardsGet[i];

				// TODO: Set the defautl of transform parent.
				paramCache.transform.SetParent ( GamePlay.Instance.TDrawCards);

				// TODO: Set the default of scale.
				paramCache.transform.localScale = Vector3.one;

				// TODO: Set the default of position.
				paramCache.transform.localPosition = Vector3.zero;

				paramCache.ResetCards ();

				if (paramCache.gameObject.activeSelf == false) {

					paramCache.gameObject.SetActive (true);
				}
			}
		}



		#endregion

		#region Animation System

		/// <summary>
		/// Preparings the game handle.
		/// </summary>
		/// <returns>The game handle.</returns>
		IEnumerator <float> PreparingGameHandle()
		{
			// TODO: Waiting until Start Game Handle Completed.
			yield return Timing.WaitUntilDone (Timing.RunCoroutine (StartingGameHandle (), Enums.Tags.GamePlaying.ToString ()));

			// TODO: Waiting until Draw Game Handle Completed.
			yield return Timing.WaitUntilDone (Timing.RunCoroutine (DrawingGameHandle (), Enums.Tags.GamePlaying.ToString ()));

			// TODO: Waiting until Check all things completed before starting the game. 
			yield return Timing.WaitUntilDone (Timing.RunCoroutine (EndPreparingGameHandle (), Enums.Tags.GamePlaying.ToString ()));

			yield return 0f;
		}

		/// <summary>
		/// Startings the game handle.
		/// </summary>
		/// <returns>The game handle.</returns>
		IEnumerator <float> StartingGameHandle()
		{
			// TODO: waiting time.
			yield return Timing.WaitForSeconds (0.5f);

            // TODO: showing the dialog.

            Debug.Log(GameManager.Instance.GetModeGame());
            if (GameManager.Instance.GetModeGame() == Enums.ModeGame.None) DialogSystem.Instance.ShowDialogNewGame();
            else DialogNewGame.updateMode(GameManager.Instance.GetModeGame());



			// TODO: waiting.
			while (GameManager.Instance.GetStateGame () != Enums.StateGame.Start)
				yield return 0f;

			// TODO: Init the functions
			Init ();

			// TODO: Count the number cards will be used.
			Contains.TotalCardsAreUsing = cardsGet.Count;

			// TODO: Create the cards will be return to pools.
			var paramIn = new List<CardBehaviour> (cardsGet);

			// TODO: Clear all the cards to prepare add the new cards.
			cardsGet.Clear ();

			// TODO: Get the new cards.
			cardsGet.AddRange (Helper.SortRandom (paramIn));

			yield return 0f;
		}

		/// <summary>
		/// Drawings the game handle.
		/// </summary>
		/// <returns>The game handle.</returns>
		IEnumerator < float > DrawingGameHandle()
		{
			// TODO: Create the default card.
			CardBehaviour cardCache = null;

			// TODO: Create the default position.
			Vector3 position = Contains.Vector3Zero;

			// TODO: Create the start transform.
			Enums.IdTransformCard idTransform = Enums.IdTransformCard.TransformCards_A;

			// TODO: Get the count increase.
			int countIncrease = 0;

			// TODO: Get the lenght of cards.
			int lenght = cardsGet.Count - 1;

			// TODO: Loop 28 times to draw the cards.
			for (int i = 0; i < 28; i++) {
				// TODO: Check don't enough cards to use.
				if (lenght <= i) {

					// TODO: Throw the exception and stop the game.
					throw new UnityException (Contains.NullExceptions);
				}

				// TODO: Set value of card.
				cardCache = cardsGet [i];

				// TODO: Check if this card is null.
				if (cardCache == null) {
                    
					// TODO: Throw the exception and stop the game.
					throw new UnityException (Contains.NullExceptions);
				}

				// TODO: Get the dafult transform from zone.
				Transform parentTransform = PlayingZone.Instance.GetTransformCards (idTransform);

				// TODO: Get the position of card from zone.
				position = Helper.GetPositionInThePlayingZone (idTransform, Enums.Direction.Down); 

				// TODO: Set the target position x for the card.
				cardCache.TargetPosition.x = position.x;

				// TODO: Set the target position y for the card.
				cardCache.TargetPosition.y = position.y;

				// TODO: Set the target position z for the card.
				cardCache.TargetPosition.z = position.z;

				// TODO: Moving the card to new position.
				cardCache.Moving (cardCache.TargetPosition, parentTransform);
                //Debug.Log("cardCache1 -- " + cardCache.TargetPosition);

                // TODO: Adding the card to the list of cards.
                PlayingZone.Instance.AddTheCard (idTransform, cardCache);

				// TODO: Playing the sound.
				SoundSystems.Instance.PlaySound (Enums.SoundIndex.Draw);

				// TODO: Waiting until next draw.
				yield return Timing.WaitForSeconds (Contains.DurationDraw);

				// TODO: Get the next id of transform.
				idTransform = PlayingZone.Instance.GetNextIDTransform (idTransform);

				if (idTransform == Enums.IdTransformCard.TransformCards_A) {

					if (countIncrease == PlayingZone.Instance.GetLenghtZone ()) {

						// TODO: Break the loop.
						break;
					}

					countIncrease++;

					for ( int j = 0 ; j < countIncrease ; j++ )
					{
						idTransform = PlayingZone.Instance.GetNextIDTransform (idTransform);
					}
				}
			}


			// TODO: Get the next id of transform.
			idTransform = Enums.IdTransformCard.TransformCards_A;

			// TODO: Get the dafult transform from zone.
			Transform defaulTransform = HintZone.Instance.GetTransformCards (idTransform);

			// TODO: Get the position of card from zone.
			position = Helper.GetPositionInTheHintZone (idTransform, Enums.Direction.Left); 

			for (int i = 28; i <= lenght; i++) {

				// TODO: Check don't enough cards to use.
				if (lenght < i) {

					// TODO: Throw the exception and stop the game.
					throw new UnityException (Contains.NullExceptions);
				}
				// TODO: Set value of card.
				cardCache = cardsGet [i];

				// TODO: Check if this card is null.
				if (cardCache == null) {

					// TODO: Throw the exception and stop the game.
					throw new UnityException (Contains.NullExceptions);
				}

				// TODO: Set the target position x for the card.
				cardCache.TargetPosition.x = position.x;

				// TODO: Set the target position y for the card.
				cardCache.TargetPosition.y = position.y;

				// TODO: Set the target position z for the card.
				cardCache.TargetPosition.z = position.z;

				// TODO: Moving the card to new position.
				cardCache.Moving (cardCache.TargetPosition, defaulTransform, null , true);
                //Debug.Log("cardCache2 -- " + cardCache.TargetPosition);

                // TODO: Adding the card to the list of cards.
                HintZone.Instance.AddTheCard (idTransform, cardCache);

				// TODO: Play the sound.
				SoundSystems.Instance.PlaySound (Enums.SoundIndex.Draw);

				// TODO: Waiting until next draw.
				yield return Timing.WaitForSeconds (Contains.DurationDraw);
			}

			// TODO: Waiting until next draw.
			yield return Timing.WaitForSeconds (Contains.DurationDraw * 2);


			// TODO: Get the next id of transform.
			idTransform = HintZone.Instance.GetNextIDTransform (idTransform);

			// TODO: Unlocking the last cards in each arrays.
			PlayingZone.Instance.UnlockLastCard ();

			yield return 0f;
		}

		/// <summary>
		/// Ends the preparing game handle.
		/// </summary>
		/// <returns>The preparing game handle.</returns>
		IEnumerator < float > EndPreparingGameHandle()
		{

			// TODO: Update the state of game is ready for playing.
			GameManager.Instance.UpdateState (Enums.StateGame.Playing);

			SoundSystems.Instance.PlayerMusic(((Enums.MusicIndex)(Random.Range((int)Enums.MusicIndex.Background_I, (int)Enums.MusicIndex.Background_III + 1))) , true);

			// TODO: Resume the time.
			GamePlay.Instance.TimeController.Resume ();

			yield return 0f;
		}

		#endregion

		#region Condition Klondike Solitaire

		/// <summary>
		/// Check the condition to win this game.
		/// </summary>
		public bool IsConditionWining()
		{
			if ( PlayingZone.Instance.IsEmptyAllCards() && HintZone.Instance.IsEmptyAllCards () )
			{
				LogGame.DebugLog("[Spider Solitaire] Completed game.");

				return true;
			}

			return false;
		}

		public bool IsConditionLosing()
		{
			// TODO: Dectect empty cards.
			return HintZone.Instance.IsEmptyCards (Enums.IdTransformCard.TransformCards_A) && !PlayingZone.Instance.IsEmptyAllCards();
		}

		public bool IsHintAvailable(bool IsShow = true)
		{
			// TODO: Get the hint from playing zone.
			var valueDisplay = PlayingZone.Instance.GetHint();

			// TODO: Check null.
			if (!object.ReferenceEquals( valueDisplay.cardDisplay , null))
			{

				// TODO: The condition to show.
				if  (IsShow)
				{

					// TODO: Show the card.
					HintDisplay.Instance.ShowHint(valueDisplay.cardDisplay.TargetPosition, valueDisplay.positionTarget, valueDisplay.cardDisplay.GetProperties().GetCardSprite());
				}

				// Break the function.
				return true;
			}

			// TODO: Get the hint from helper zone.
			valueDisplay = HintZone.Instance.GetHint();

			// TODO: Check null.
			if (!object.ReferenceEquals(valueDisplay.cardDisplay, null))
			{

				// TODO: The condition to show.
				if (IsShow)
				{
					if (valueDisplay.cardDisplay.IsUnlocked ()) {
						
						// TODO: Show the card.
						HintDisplay.Instance.ShowHint (valueDisplay.cardDisplay.TargetPosition, valueDisplay.positionTarget, valueDisplay.cardDisplay.GetProperties().GetCardSprite());
					} else {
						
						// TODO: Show the card.
						HintDisplay.Instance.ShowHint (valueDisplay.cardDisplay.TargetPosition, valueDisplay.positionTarget, DataSystem.Instance.GetDefaultCard (Contains.GetThemeType));
					}
				}

				return true;
			}

			return false;
		}
		#endregion
	}
}
