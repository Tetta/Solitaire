using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolItem
{
	[TooltipAttribute ("The id of prefab.")]
	public Enums.PoolType poolId;

	[TooltipAttribute ("The item of prefab.")]
	public GameObject poolItem;
}

/// <summary>
/// Pool system.
/// </summary>
public class PoolSystem : Singleton < PoolSystem > {

	// ============================= References =========================== //

	#region References

	/// <summary>
	/// The item properties.
	/// </summary> 
	protected List < CardBehaviour > ItemProperties = new List<CardBehaviour>();


	[Header ("PROPERTIES")]
	[Tooltip ("The list of item will create with pool.")]
	[SerializeField] private PoolItem[] poolItems;

	[Tooltip ("The number of item will be created.")]
	[SerializeField] private int Quantity;
	#endregion

	// TODO: The array save the pool items.
	protected Dictionary < int , List < GameObject > > poolArray = new Dictionary<int, List<GameObject>> ();

	#region Functional

	protected override void Awake ()
	{
		base.Awake ();

		// TODO: Set the object create.
		GameObject objectCreate = null;

		// TODO: Set the name of object;
		string objectName = string.Empty;

		// TODO: Get the list.
		List < GameObject > paramOut;

		// TODO: Create the key.
		int key = 0;

		for (int i = 0; i < poolItems.Length; i++) {

			// TODO: Get the prefab.
			objectCreate = poolItems [i].poolItem;

			// TODO: Get the name.
			objectName = objectCreate.name;

			// TODO: Clear the current list.
			paramOut = new List<GameObject>();

			for (int j = 0; j < Quantity; j++) {
			
				// TODO: Set the aram;
				objectCreate = Instantiate (objectCreate, this.transform); 

				// TODO: Set the name.
				objectCreate.name = objectName;

				if (objectCreate.activeSelf == true) {

					// TODO: Disable the gameobject.
					objectCreate.SetActive (false);
				}		

				// TODO: Add the object to the list.
				paramOut.Add (objectCreate);
			}

			// TODO: Get the keys.
			key = (int)poolItems [i].poolId;

			// TODO: Check the condition key.
			if (poolArray.ContainsKey (key)) {
				
				// TODO: Get the list.
				List < GameObject > paramFind;

				// TODO: Try to get the value from the dictionary.
				if (poolArray.TryGetValue (key, out paramFind)) {

					// TODO: Check the condition null.
					if (object.ReferenceEquals (paramFind, null)) {

						// TODO: Create the new list.
						paramFind = new List<GameObject> ();
					}

					// TODO: Add the range from another list.
					paramFind.AddRange (paramOut);

					// TODO: Set the key.
					poolArray [key] = paramFind;
				} else {
				
					// TODO: add the value to param.
					poolArray.Add ((int)poolItems [i].poolId, paramOut);
				}
				
			} else {

				// TODO: add the value to param.
				poolArray.Add ((int)poolItems [i].poolId, paramOut);
			}
		}
	}

	/// <summary>
	/// Gets all cards.
	/// </summary>
	/// <returns>The all cards.</returns>
	public List < CardBehaviour > GetAllCards()
	{
		List < CardBehaviour > paramReturn = new List<CardBehaviour> (ItemProperties);

		ItemProperties.Clear ();

		return paramReturn;
	}

	/// <summary>
	/// Gets the color of the cards.
	/// </summary>
	/// <returns>The cards color.</returns>
	/// <param name="type">Type.</param>
	/// <param name="numberLoad">Number load.</param>
	public List < CardBehaviour > GetCardsColor(Enums.CardType type)
	{
		// TODO: Create default list to return.
		List < CardBehaviour > paramReturn = new List<CardBehaviour> ();

		// TODO: Count the items.
		int count = ItemProperties.Count;

		if (type == Enums.CardType.None) {

			paramReturn.AddRange (ItemProperties);
		} else {

			// TODO: Create the cache of card.
			CardBehaviour paramCache;

			// TODO: Loop and check with the conditions.
			for (int i = 0; i < count; i++) {

				// TODO: Set the cache with the value of items.
				paramCache = ItemProperties [i];

				if (paramCache == null) {
					print ("Yes");
					continue;
				}

				// TODO: Check the condition to get this card.
				if (paramCache.GetTypeCards () == type && !paramReturn.Exists (x => x.GetValue () == paramCache.GetValue ())) {

					// TODO: Add the card to params Return.
					paramReturn.Add (paramCache);

					// TODO: Break the loop if paramReturn enough 13 cards.
					if (paramReturn.Count > 12)
						break;
				}
			}
		}

		//TODO: Check the number of paramReturn equal 13.
        //Debug.Log("paramReturn " +paramReturn.Count);
		if (paramReturn.Count > 12) {

			// TODO: Loop and remove all cards.
			for (int i = 0; i < paramReturn.Count; i++) {

				// TODO: remove this item out of List Cards.
				ItemProperties.Remove (paramReturn [i]);
			}
		} else {

			// TODO: clear the param return if don't enough cards.
			paramReturn.Clear ();
		}

		// TODO: Return the list of cards.
		return paramReturn;
	}

	/// <summary>
	/// Clears the cards.
	/// </summary>
	public void ClearCards()
	{
		for (int i = 0; i < ItemProperties.Count; i++) {
			if (ItemProperties [i] != null) {
				Destroy (ItemProperties [i].gameObject);
			}
		}

		ItemProperties.Clear ();
	}

	/// <summary>
	/// Clear this instance.
	/// </summary>
    public void Clear()
    {
        ItemProperties.Clear();
    }

	/// <summary>
	/// Returns to pool.
	/// </summary>
	/// <param name="param">Parameter.</param>
	public void ReturnToPool(CardBehaviour param)
	{
		// TODO: Check if this is not null.
		if (param != null) {

			// TODO: Disable the gameobject.
			//param.gameObject.SetActive (false);

			// TODO: Check if this exists in the list. Just remove this param.
			if (ItemProperties.Contains (param)) {
				
				LogGame.DebugLog (string.Format ("[Pool System] Card Was Found {0}", param.name));

				// TODO: Remove this param.
				ItemProperties.Remove (param);
			}

			// TODO: Set default parent transform.
			param.transform.SetParent (transform);

			param.transform.localPosition = Contains.Vector3Null;

			// TODO: Add this param to the list of cards.
			ItemProperties.Add (param);
		}
	}

	public void ReturnToPool(Enums.PoolType poolId, GameObject param)
	{
		// TODO: The list of object will get.
		List < GameObject > objectGet;

		// TODO: Disable the gameobject.
		param.SetActive (false);

		// TODO: Set the new parent.
		param.transform.SetParent (transform);

		// TODO: Get the id.
		int id = (int)poolId;

		if (poolArray.TryGetValue (id, out objectGet)) {

			if (object.ReferenceEquals (objectGet, null)) {

				// TODO: Create the new list will be saved.
				objectGet = new List<GameObject> ();
			}

			// TODO: Add the gameobject to the list.
			objectGet.Add (param);

			// TODO: Set the new value.
			poolArray [id] = objectGet;
		} else {

			// TODO: add the new object.
			poolArray.Add (id, new List<GameObject> (){ param });
		}
	}

	public GameObject GetFromPool(Enums.PoolType poolId)
	{
		// TODO: The param will be returned.
		GameObject paramReturn = null;

		// TODO: The list of object will get.
		List < GameObject > objectGet;

		if (poolArray.TryGetValue ((int)poolId, out objectGet)) {
		
			if (!object.ReferenceEquals (objectGet, null) && objectGet.Count > 0) {
			
				// TODO: Set the param return;
				paramReturn = objectGet [0];

				// TODO: Remove the first value/
				objectGet.RemoveAt (0);
			}
		}

		if (object.ReferenceEquals (paramReturn, null)) {

			for (int i = 0; i < poolItems.Length; i++) {

				if (poolItems [i].poolId == poolId) {

					// TODO: Create the pool item.
					paramReturn = Instantiate (poolItems [i].poolItem);

					break;
				}
			}
		}

		return paramReturn;
	}

	/// <summary>
	/// Gets the number of cards.
	/// </summary>
	public int GetNumberOfCards()
	{
        for (int i = 0; i < ItemProperties.Count; i++)
        {
            if (ItemProperties[i] == null)
            {
                ItemProperties.RemoveAt(i);
            }
        }

        return ItemProperties.Count;
	}

	#endregion
}
