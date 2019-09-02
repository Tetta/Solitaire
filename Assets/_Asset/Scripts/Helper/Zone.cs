using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ZoneCards
{
    /// <summary>
    /// The id of Transform.
    /// </summary>
    public Enums.IdTransformCard Id;

    /// <summary>
    /// The parent transform of cards.
    /// </summary>
    public Transform ZoneHandle;
}

public abstract class Zone < T > : MonoBehaviour where T : MonoBehaviour {

    #region References

	/// <summary>
	/// The is persistence.
	/// </summary>
	public bool IsPersistence;

	/// <summary>
	/// The m instance.
	/// </summary>
	protected static T m_Instance;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get { return m_Instance; }
	}

    [Header("Zone")]

    /// <summary>
    /// The zone of cards.
    /// </summary>
    [SerializeField]
	public ZoneCards[] zoneCards;


	[HideInInspector]
    /// <summary>
    /// Create the default list of cards.
    /// </summary>
	public Dictionary<string, List<CardBehaviour>> cards = new Dictionary<string, List<CardBehaviour>>();
    #endregion

    #region Functional


	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected virtual void Awake()
	{
		if (IsPersistence)
		{
			if (ReferenceEquals(m_Instance, null))
			{
				m_Instance = this as T;

				DontDestroyOnLoad(gameObject);
			}
			else if (!ReferenceEquals(m_Instance, this as T))
			{
				Destroy(gameObject);
			}
		}
		else
		{
			m_Instance = this as T;
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
    protected virtual void Start()
    {

        // TODO: Init the functions.
        Init();
    }

    /// <summary>
    /// Init all the functional.
    /// </summary>
    protected void Init()
    {
        // TODO: Loop with the number of zone.
        for (int i = 0; i < zoneCards.Length; i++)
        {
            // TODO: Add the keys and values.
			cards.Add(Enums._IdTransformCard [(int)zoneCards[i].Id], new List<CardBehaviour>());
        }
    }

    #endregion


    #region Helper

	/// <summary>
	/// Determines whether this instance is card moving inside zone.
	/// </summary>
	public bool IsCardMovingInsideZone(Enums.IdTransformCard id , Vector3 positionTarget)
	{
		// TODO: Get the position of the last card in the list of cards.
		Vector3 positionZone =  GetWorldPosition (id);

		// TODO: Check if the value is null.
		if (Contains.Vector3Null.x == positionZone.x) {

			// TODO: Get the default position of zone.
			positionZone = GetDefaultPosition (id);

		}	

		// TODO: Return the condition.
		return IsPositionInsideZone (positionTarget, positionZone);
	}

	/// <summary>
	/// Determines whether this instance is card moving inside any zone.
	/// </summary>
	/// <returns><c>true</c> if this instance is card moving inside any zone; otherwise, <c>false</c>.</returns>
	public bool IsCardMovingInsideAnyZone(Vector3 position)
	{
		// TODO: Loop to check all of zones.
		for (int i = 0; i < zoneCards.Length; i++) {

			// TODO: Check if the card is inside any zone.
			if (IsCardMovingInsideZone (zoneCards [i].Id, position)) {

				// TODO: Return true if the condition is true.
				return true;
			}
		}

		// TODO: Return false if the condition is false.
		return false;
	}

	/// <summary>
	/// Gets the identifier zone where the card is moving inside.
	/// </summary>
	public Enums.IdTransformCard GetTheIdZoneMovingInside(Vector3 position){

		// TODO: Loop to check all of zones.
		for (int i = 0; i < zoneCards.Length; i++) {

			// TODO: Check if the card is inside any zone.
			if (IsCardMovingInsideZone (zoneCards [i].Id, position)) {

				// TODO: return the id of zone if the condition is true.
				return zoneCards[i].Id;
			}
		}

		// TODO: Return none if can not find any zone.
		return Enums.IdTransformCard.None;
	}

	/// <summary>
	/// Gets the identifier of zones where the card is moving inside.
	/// </summary>
	public List < int > GetTheIdZonesMovingInside ( Vector3 position )
	{
		// TODO: Create the list of zone will be returned.
		List < int > ZoneReturn = new List<int> ();

		// TODO: Loop to check all of zones.
		for (int i = 0; i < zoneCards.Length; i++) {

			// TODO: Check if the card is inside any zone.
			if (IsCardMovingInsideZone (zoneCards [i].Id, position)) {

				// TODO: return the id of zone if the condition is true.
				ZoneReturn.Add ( (int)zoneCards[i].Id );
			}
		}

		// TODO: Return none if can not find any zone.
		return ZoneReturn;
	}
		
	/// <summary>
	/// Determines whether this instance is position inside zone.
	/// </summary>
	/// <returns><c>true</c> if this instance is position inside zone; otherwise, <c>false</c>.</returns>
	protected bool IsPositionInsideZone(Vector3 positionTarget , Vector3 positionZone){

		// TODO: Check if the position of card inside the zone of target.
		if (positionTarget.x < positionZone.x + Contains.OffSetWidthCard &&
		    positionTarget.x > positionZone.x - Contains.OffSetWidthCard &&
		    positionTarget.y < positionZone.y + Contains.OffSetHeightCard &&
		    positionTarget.y > positionZone.y - Contains.OffSetHeightCard) {

			// TODO: Return true if the condition is true.
			return true;
		}

		// TODO: Return false if the condition is false.
		return false;
	}

    /// <summary>
    /// Return the parent transform of cards.
    /// </summary>
    public Transform GetTransformCards(Enums.IdTransformCard id)
    {
        // TODO: Loop with number of zonecards.
        for (int i = 0; i < zoneCards.Length; i++)
        {
            // TODO: Check if exists any id equal with the id of condition.
            if (zoneCards[i].Id == id)
            {
                // TODO: Return the transform of zone.
                return zoneCards[i].ZoneHandle;
            }
        }
        // TODO: Return null if can not find the transform.
        return null;
    }

	/// <summary>
	/// Gets the identifier the list of cards.
	/// </summary>
	public Enums.IdTransformCard GetIdTransform(CardBehaviour paramIn)
	{
		// TODO: Create the param will be returned.
		Enums.IdTransformCard paramReturn = Enums.IdTransformCard.None;

		// TODO: Get the number of zone.
		int count = zoneCards.Length;

		// TODO: Create the list of cards.
		List<CardBehaviour> paramOut;

		// TODO: Loop to get the id.
		for (int i = 0; i < count; i++) {

			// TODO: Try to get the value of cards.
			if (cards.TryGetValue (Enums._IdTransformCard [(int)zoneCards[i].Id], out paramOut)) {

				// TODO: Check if the card exists.
				if (paramOut.Contains (paramIn)) {
					
					// TODO: Get the value will be returned.
					paramReturn = zoneCards[i].Id;

					// TODO: Break the functions.
					break;
				}
			}
		}

		// TODO: Return the value.
		return paramReturn;
	}

	public List < int > GetTheListIdZones()
	{
		// TODO: Create the list param.
		List < int > paramReturn = new List<int> ();

		// TODO: Loop to check the ids.
		for (int i = 0; i < zoneCards.Length; i++) {

			// TODO: Add the id.
			paramReturn.Add ((int)zoneCards [i].Id);
		}

		// TODO: Return the list.
		return paramReturn;
	}

	/// <summary>
	/// Gets the next identifier of transform.
	/// </summary>
	/// <returns>The next identifier of transform.</returns>
	public Enums.IdTransformCard GetNextIDTransform(Enums.IdTransformCard id)
	{
		// TODO: Create the param will be returned.
		Enums.IdTransformCard paramReturn = Enums.IdTransformCard.None;

		// TODO: Loop to check the id.
		for (int i = 0; i < zoneCards.Length; i++) {

			// TODO: Check if this id equal with the id condition.
			if (zoneCards [i].Id == id) {

				// TODO: Check if the index of array is max.
				if (i == zoneCards.Length - 1) {

					// TODO: Return the first value.
					paramReturn = zoneCards [0].Id;
				
				} else {
				
					// TODO: Return the next value.
					paramReturn = zoneCards [i + 1].Id;

				}

				break;
			}
		}

		// TODO: Return the value.
		return paramReturn;
	}

    /// <summary>
    /// Get the position of cards.
    /// </summary>
    public Vector3 GetWorldPosition(Enums.IdTransformCard id)
    {
        // TODO: Create the list of cards will be gotten from the Dictionary.
        List<CardBehaviour> paramOut = new List<CardBehaviour>();
        
		if ( cards.ContainsKey ( Enums._IdTransformCard [(int)id] ))
        {
			if ( cards.TryGetValue ( Enums._IdTransformCard [(int)id], out paramOut ))
            {
                if ( paramOut.Count > 0 )
                {
                    return paramOut[paramOut.Count - 1].TargetPosition;
                }
            }
        }

		return Contains.Vector3Null;
    }

	/// <summary>
	/// Gets the default position.
	/// </summary>
	/// <returns>The default position.</returns>
	/// <param name="id">Identifier.</param>
	public Vector3 GetDefaultPosition(Enums.IdTransformCard id)
	{

		for ( int i = 0; i < zoneCards.Length; i++ )
		{
			if ( zoneCards[i].Id == id)
			{
				return zoneCards[i].ZoneHandle.position;
			}
		}

		return Contains.Vector3Zero;
	}

    /// <summary>
    /// Return true if this card exists.
    /// </summary>
    public bool IsExistsCards(Enums.IdTransformCard id, CardBehaviour paramIn)
    {
        switch (id)
        {
            case Enums.IdTransformCard.None:

                // TODO: Loop with the number of zone.
                for (int i = 0; i < zoneCards.Length; i++)
                {
                    // TODO: Check if it exists.
					if (cards.ContainsKey(Enums._IdTransformCard [(int)zoneCards[i].Id]))
                    {
                        // TODO: Create the list of cards.
                        List<CardBehaviour> paramOut;

                        // TODO: Try to get the value of cards.
						if (cards.TryGetValue(Enums._IdTransformCard [(int)zoneCards[i].Id], out paramOut))
                        {

                            // TODO: Check if card exists.
                            if (paramOut.Contains(paramIn))
                            {
                                // TODO: Return the value.
                                return true;
                            }
                        }
                    }
                }

                break;
            default:
                // TODO: Check if it exists.
				if (cards.ContainsKey(Enums._IdTransformCard [(int)id]))
                {

                    // TODO: Create the list of cards.
                    List<CardBehaviour> paramOut;

                    // TODO: Try to get the value of cards.
					if (cards.TryGetValue(Enums._IdTransformCard [(int)id], out paramOut))
                    {

                        // TODO: Check if the card exists.
                        if (paramOut.Contains(paramIn))
                        {
                            // TODO: Return the value.
                            return true;
                        }
                    }
                }
                break;
        }

        // TODO: Return the value.
        return false;
    }

    /// <summary>
    /// Check if this card exists in any transforms.
    /// </summary>
    public bool IsExistsCards(CardBehaviour paramIn)
    {

        // TODO: Return the value.
        return IsExistsCards(Enums.IdTransformCard.None, paramIn);
    }


	/// <summary>
	/// Determines whether this instance is empty cards the specified id.
	/// </summary>
	/// <returns><c>true</c> if this instance is empty cards the specified id; otherwise, <c>false</c>.</returns>
	/// <param name="id">Identifier.</param>
	public bool IsEmptyCards(Enums.IdTransformCard id)
	{
		var cardFounds = GetTheListCards (id);

		// TODO: Detect condition.
		if (object.ReferenceEquals ( cardFounds , null ) || cardFounds.Count == 0)
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Determines whether this instance is empty all cards.
	/// </summary>
	/// <returns><c>true</c> if this instance is empty all cards; otherwise, <c>false</c>.</returns>
	public bool IsEmptyAllCards()
	{
		// TODO: Check the condition empty.
		bool IsEmpty = true;

		for (int i = 0; i < zoneCards.Length; i++) {

			if (!IsEmptyCards (zoneCards [i].Id)) {
			
				// TODO: Set the value.
				IsEmpty = false;

				// TODO: Break the looping.
				break;
			}
		}

		// TODO: Return the value;
		return IsEmpty;
	}

	/// <summary>
	/// Adds the card into the list of cards.
	/// </summary>
	public void AddTheCard(Enums.IdTransformCard id, CardBehaviour paramIn)
	{
		// TODO: Check if this card is non exists in any transform.
		if (id == Enums.IdTransformCard.None)
			return;

		// TODO: Check if it exists.
		if (cards.ContainsKey (Enums._IdTransformCard [(int)id])) {
			// TODO: Create the list of cards.
			List<CardBehaviour> paramCheck;

			// TODO: Try to get the value of cards.
			if (cards.TryGetValue (Enums._IdTransformCard [(int)id], out paramCheck)) {
				// TODO: Check if this card exists.
				if (paramCheck.Contains (paramIn)) {

					// TODO: Remove the old card.
					paramCheck.Remove (paramIn);
				}

				// TODO: Add the new card to the list.
				paramCheck.Add (paramIn);
			} else {

				// TODO: Create the new list with the new card.
				cards[Enums._IdTransformCard [(int)id]] = new List<CardBehaviour> (new CardBehaviour[] { paramIn });
			}
		} else {
			
			// TODO: Create the new list with the new card.
			cards.Add (Enums._IdTransformCard [(int)id], new List<CardBehaviour> (new CardBehaviour[] { paramIn }));
		}
	}

	/// <summary>
	/// Removes the card.
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="paramIn">Parameter in.</param>
	public void RemoveTheCard(Enums.IdTransformCard id, CardBehaviour paramIn)
	{
		// TODO: Check if it exists.
		if (cards.ContainsKey(Enums._IdTransformCard [(int)id]))
		{
			// TODO: Create the list of cards.
			List<CardBehaviour> paramCheck;

			// TODO: Try to get the value of cards.
			if (cards.TryGetValue(Enums._IdTransformCard [(int)id], out paramCheck))
			{

				// TODO: Check if the card exists.
				if (paramCheck.Contains(paramIn))
				{
					// TODO: Return the value.
					paramCheck.Remove ( paramIn );
				}
			}
		}
	}

	/// <summary>
	/// Removes the card.
	/// </summary>
	/// <param name="paramIn">Parameter in.</param>
	public void RemoveTheCard(CardBehaviour paramIn)
	{
		// TODO: Get the length of zones.
		int length = zoneCards.Length;

		// TODO: Loop to remove the card.
		for (int i = 0; i < length; i++) {
		
			// TODO: Remove the card from the list of cards.
			RemoveTheCard (zoneCards [i].Id, paramIn);
		}
	}

	/// <summary>
	/// Gets the last card.
	/// </summary>
	public CardBehaviour GetTheLastCard(Enums.IdTransformCard id)
	{
		// TODO: Create the value will be returned.
		CardBehaviour paramReturn = null;

		// TODO: Check if it exists.
		if (cards.ContainsKey(Enums._IdTransformCard [(int)id]))
		{
			// TODO: Create the list of cards.
			List<CardBehaviour> paramCheck;

			// TODO: Try to get the value of cards.
			if (cards.TryGetValue( Enums._IdTransformCard [(int)id], out paramCheck))
			{
				// TODO: Check if it has any card.
				if (paramCheck.Count > 0) {

					// TODO: Set the value will be returned.
					paramReturn = paramCheck [paramCheck.Count - 1];
				}
			}
		}

		// TODO: Return the value.
		return paramReturn;
	}

	/// <summary>
	/// Gets the last card. Where exist this card
	/// </summary>
	public CardBehaviour GetTheLastCard(CardBehaviour card)
	{
		// TODO: Create the value will be returned.
		CardBehaviour paramReturn = null;

		// TODO: Create the list of cards.
		List<CardBehaviour> paramCheck;

		for (int i = 0; i < zoneCards.Length; i++)
		{
			// TODO: Check if it exists.
			if (cards.ContainsKey(Enums._IdTransformCard [(int)zoneCards[i].Id]))
			{
				// TODO: Try to get the value of cards.
				if (cards.TryGetValue( Enums._IdTransformCard [(int)zoneCards[i].Id], out paramCheck))
				{
					// TODO: Check if it has any card.
					if (paramCheck.Count > 0 && paramCheck.Contains ( card )) {

						// TODO: Set the value will be returned.
						paramReturn = paramCheck [paramCheck.Count - 1];
					}
				}
			}
		}

		// TODO: Return the value.
		return paramReturn;
	}

	/// <summary>
	/// Gets the first card. From the id of card.
	/// </summary>
	public CardBehaviour GetTheFirstCard(Enums.IdTransformCard id){

		// TODO: Create the value will be returned.
		CardBehaviour paramReturn = null;

		// TODO: Check if it exists.
		if (cards.ContainsKey(Enums._IdTransformCard [(int)id]))
		{
			// TODO: Create the list of cards.
			List<CardBehaviour> paramCheck;

			// TODO: Try to get the value of cards.
			if (cards.TryGetValue(Enums._IdTransformCard [(int)id], out paramCheck))
			{
				// TODO: Check if it has any card.
				if (paramCheck.Count > 0) {

					// TODO: Set the value will be returned.
					paramReturn = paramCheck [0];
				}
			}
		}

		// TODO: Return the value.
		return paramReturn;
	}

	/// <summary>
	/// Gets the first card. Where exist this card
	/// </summary>
	public CardBehaviour GetTheFirstCard(CardBehaviour card){

		// TODO: Create the value will be returned.
		CardBehaviour paramReturn = null;

		// TODO: Create the list of cards.
		List<CardBehaviour> paramCheck;

		for (int i = 0; i < zoneCards.Length; i++) {

			// TODO: Check if it exists.
			if (cards.ContainsKey(Enums._IdTransformCard [(int)zoneCards[i].Id]))
			{	
				// TODO: Try to get the value of cards.
				if (cards.TryGetValue(Enums._IdTransformCard [(int)zoneCards[i].Id], out paramCheck))
				{
					// TODO: Check if it has any card.
					if (paramCheck.Count > 0 && paramCheck.Contains ( card )) {

						// TODO: Set the value will be returned.
						paramReturn = paramCheck [0];

						// TODO: Break the function.
						break;
					}
				}
			}
		}

		// TODO: Return the value.
		return paramReturn;
	}

	/// <summary>
	/// Gets the list of cards from the index of card in the list.
	/// </summary>
	public List < CardBehaviour > GetTheListCardsFromIndex(Enums.IdTransformCard id, CardBehaviour paramIn)
	{
		// TODO: Create the value will be returned.
		List < CardBehaviour > paramReturn = new List<CardBehaviour>();

		// TODO: Check if it exists.
		if (cards.ContainsKey(Enums._IdTransformCard [(int)id]))
		{
			// TODO: Create the list of cards.
			List<CardBehaviour> paramCheck;

			// TODO: Try to get the value of cards.
			if (cards.TryGetValue(Enums._IdTransformCard [(int)id], out paramCheck))
			{
				//TODO: Get the count of list.
				int count = paramCheck.Count;
		
				// TODO: Time to start getting the cards.
				bool isReadyGet = false;

				// TODO: Create the cache of cards.
				CardBehaviour cacheCard = null;

				// TODO: Loop to get the cards.
				for (int i = 0; i < count; i++) {

					// TODO: Check if is not time to start getting the cards.
					if (isReadyGet == false) {

						// TODO: Set the value to get.
						cacheCard = paramCheck [i];

						// TODO: Check the condition of cards.
						if (cacheCard == paramIn) {
							isReadyGet = true;
						}

					} else {

						// TODO: Add the value will be returned.
						paramReturn.Add (paramCheck [i]);
					}
				}
			}
		}

		// TODO: return the value.
		return paramReturn;
	}

	/// <summary>
	/// Gets the list cards.
	/// </summary>
	public List < CardBehaviour > GetTheListCards(Enums.IdTransformCard id )
	{
		// TODO: Create the list will be returned.
		List < CardBehaviour > paramReturn = new List<CardBehaviour> ();

		// TODO: Cache the key.
		string key = Enums._IdTransformCard [(int)id];

		// TODO: Check if it eixist.
		if (cards.ContainsKey (key)) {

			// TODO: Try to get the value.
			cards.TryGetValue (key, out paramReturn);
		}

		// TODO: Check if is null.
		if (object.ReferenceEquals (paramReturn, null)) {

			// TODO: Create the value.
			paramReturn = new List<CardBehaviour> ();
		}

		// TODO: Return the list.
		return paramReturn;
	}

	/// <summary>
	/// Determines whether this instance is the latest card unlocked.
	/// </summary>
	public bool IsLastCardUnlocked(Enums.IdTransformCard id)
	{
		// TODO: Get the last cards in the list.
		CardBehaviour cardGet = GetTheLastCard (id);

		// TODO: Check if is null.
		if (object.ReferenceEquals (cardGet, null)) {

			// TODO: Return false if is null.
			return false;
		}

		// TODO: Return the state of the card.
		return cardGet.IsUnlocked ();
	}


	/// <summary>
	/// Gets the lenght zone.
	/// </summary>
	/// <returns>The lenght zone.</returns>
	public int GetLenghtZone()
	{
		// TODO: Return the lenght of zone.
		return zoneCards.Length;
	}

    public int GetTotalCards(Enums.IdTransformCard id)
    {
        // TODO: Create the param will be returned.
        int total = 0;

        for ( int i = 0;  i <  zoneCards.Length; i++ )
        {

            // TODO: Check the condition to get total cards.
            if ( zoneCards[i].Id == id )
            {
                // TODO: Get the list cards from id.
                var cardParam = GetTheListCards(id);

                // TODO: Get the count
                total = cardParam.Count;
            }
        }

        return total;
    }

    #endregion

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	protected virtual void OnDestroy()
	{
		// TODO: Destroy the static value.
		m_Instance = null;
	}

}
