using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Helper.
/// </summary>
public static class Helper {

	// TODO: Create the new string fast.
	public static StringFast StringBulding = new StringFast();

	/// <summary>
	/// Sort random position of all the cards in the array.
	/// </summary>
	public static List < CardBehaviour > SortRandom( List < CardBehaviour > paramIn)
	{
		// TODO: Create the list of cards.
		var paramOut = new List < CardBehaviour > (paramIn);

		// TODO: Create the list of cards will be returned.
		var paramReturn = new List < CardBehaviour > ();

		// TODO: Create the cache of card.
		CardBehaviour card = null;

		// TODO: Loop the cards to get random the values.
		while (paramOut.Count > 0) {

			// TODO: Get the value from random in the list of cards.
			card = paramOut [Random.Range (0, paramOut.Count)];

			// TODO: Remove the current card from the list.
			paramOut.Remove (card);

			// TODO: Add the card to the list will be returned.
			paramReturn.Add (card);
		}
	
		// TODO: Return the list of cards.
		return paramReturn;
	}

    /// <summary>
    /// Gets the world position in the holder cards.
    /// </summary>
	public static Vector3 GetPositionInThePlayingZone( Enums.IdTransformCard id, Enums.Direction direction, bool IsUnlocked = false , float distanceUnlocked = Contains.DistanceSortUnlockedCards, float distanceLocked = Contains.DistanceSortLockedCards)
    {
        // TODO: Create the default position will be returned.
		Vector3 position = PlayingZone.Instance.GetWorldPosition(id);

		// TODO: Check if the position is not from the any cards.
		if (position.x != Contains.Vector3Null.x) {
			
			switch (direction) {
			case Enums.Direction.Down:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.y -= distanceUnlocked;
				
				} else {

					// TODO: Add Position.
					position.y -= distanceLocked;
				}

				break;
			case Enums.Direction.Left:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.x -= distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.x -= distanceLocked;
				}

				break;
			case Enums.Direction.Right:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {
					
					// TODO: Add Position.
					position.x += distanceUnlocked;

				} else {
					
					// TODO: Add Position.
					position.x += distanceLocked;
				}

				break;
			case Enums.Direction.Up:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.y += distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.y += distanceLocked;
				}

				break;
			}
	
		} else {
		
			position = PlayingZone.Instance.GetDefaultPosition (id);
		}      

        // TODO: Return the position.
        return position;
    }

	/// <summary>
	/// Gets the world position in the holder cards.
	/// </summary>
	public static Vector3 GetPositionInTheHintZone(Enums.IdTransformCard id, Enums.Direction direction, bool IsUnlocked = false , float distanceUnlocked = Contains.DistanceSortUnlockedCards, float distanceLocked = Contains.DistanceSortLockedCards)
	{
		// TODO: Set the default position.
		Vector3 position = HintZone.Instance.GetWorldPosition(id);

		// TODO: Check if the position is not from the any cards.
		if (position.x != Contains.Vector3Null.x) {

			switch (direction) {
			case Enums.Direction.Down:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.y -= distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.y -= distanceLocked;
				}

				break;
			case Enums.Direction.Left:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.x -= distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.x -= distanceLocked;
				}

				break;
			case Enums.Direction.Right:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.x += distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.x += distanceLocked;
				}

				break;
			case Enums.Direction.Up:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.y += distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.y += distanceLocked;
				}

				break;
			}

		} else {

			// TODO: Get the default position.
			position = HintZone.Instance.GetDefaultPosition (id);
		}      

		// TODO: Return the position.
		return position;
	}

	/// <summary>
	/// Gets the world position in the holder cards.
	/// </summary>
	public static Vector3 GetPositionInTheResultZone(Enums.IdTransformCard id, Enums.Direction direction, bool IsUnlocked = false , float distanceUnlocked = Contains.DistanceSortUnlockedCards, float distanceLocked = Contains.DistanceSortLockedCards)
	{
		// TODO: Create the default position will be returned.
		Vector3 position = ResultZone.Instance.GetWorldPosition(id);

		// TODO: Check if the position is not from the any cards.
		if (position.x != Contains.Vector3Null.x) {

			switch (direction) {
			case Enums.Direction.Down:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.y -= distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.y -= distanceLocked;
				}

				break;
			case Enums.Direction.Left:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.x -= distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.x -= distanceLocked;
				}

				break;
			case Enums.Direction.Right:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.x += distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.x += distanceLocked;
				}

				break;
			case Enums.Direction.Up:

				// TODO: Check if this card unlocked.
				if (IsUnlocked) {

					// TODO: Add Position.
					position.y += distanceUnlocked;

				} else {

					// TODO: Add Position.
					position.y += distanceLocked;
				}

				break;
			}

		} else {

			position = ResultZone.Instance.GetDefaultPosition (id);
		}      

		// TODO: Return the position.
		return position;
	}

	// TODO: Get the member.
	public static Camera mainCamera;

	public static Vector3 GetPositionFromScreenPoint(Vector3 point){

		// TODO:Check if the camera exists.
		if (!object.ReferenceEquals ( mainCamera , null)) {

			// TODO: Convert to the world position from the point of screen.
			return mainCamera.ScreenToWorldPoint (point);
		}

		// TODO: Get the main camera.
		mainCamera = Camera.main;

		// TODO: Return the screen point.
		return mainCamera.ScreenToWorldPoint (point);
	}

	/// <summary>
	/// Sorts the cards.
	/// </summary>
	/// <param name="cards">Cards.: The list of cards.</param>
	/// <param name="direction">Direction.: The direction sort.</param>
	/// <param name="position">Position: The start position.</param>
	/// <param name="limitCards">Limit cards.: The number of cards will show with distance sort.</param>
	/// <param name="distance">Distance.: This distance between each card.</param>
	public static void SortCards (List< CardBehaviour > cards, Enums.Direction direction, Vector3 position,int limitCards = 2, float distance = Contains.DistanceSortHintCards )
	{
		// TODO: Get the number of cards.
		int count = cards.Count;

		// TODO: Get the card cache.
		CardBehaviour card;

		// TODO: Get the offset position.
		Vector3 offsetPosition = Contains.Vector3Zero;

		// TODO: Get the start position.
		Vector3 startPosition = position;

		// TODO: Caculate the offset position.
		switch (direction) {

		case Enums.Direction.Down:

			// TODO: Set the distance.
			offsetPosition.y -= distance;

			break;
		case Enums.Direction.Left:


			// TODO: Set the distance.
			offsetPosition.x -= distance;
			break;
		case Enums.Direction.Right:


			// TODO: Set the distance.
			offsetPosition.x += distance;
			break;
		case Enums.Direction.Up:

			// TODO: Set the distance.
			offsetPosition.y += distance;
			break;
		}

		// TODO: Get the none card.
		int atCount = count - limitCards;

		// TODO: Check the condition refresh card.
		if (atCount < 0) {

			// TODO: Set the default card will be set.
			atCount = 0;
		}

		for (int i = 0; i < atCount; i++) {

			// TODO: Set the new target position.
			cards [i].TargetPosition = startPosition;
            //Debug.Log("SortCards -- " + cards[i].TargetPosition);

            // TODO: moving the new position.
            cards[i].Moving (startPosition);		
		}

		for (int i = atCount; i < count; i++) {

			// TODO: Set the new target position.
			cards [i].TargetPosition = startPosition;
            //Debug.Log("SortCards1 -- " + cards[i].TargetPosition);

            // TODO: moving the new target.
            cards[i].Moving (startPosition);

			// TODO: Increase the new position with offset.
			startPosition.x += offsetPosition.x;

			// TODO: Increase the new position with offset.
			startPosition.y += offsetPosition.y;

			// TODO: Increase the new position with offset.
			startPosition.z += offsetPosition.z;
		}
	}
}
