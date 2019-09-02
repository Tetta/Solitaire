using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperZone : Singleton < HelperZone > {

	[Header ("Hint Zone")]

	/// <summary> 
	/// The card renderer.
	/// </summary>
	[SerializeField] private Image cardRenderer;

	#region Holder Zone


	[Header ("Holder Zone")]

	/// <summary>
	/// The holder transform.
	/// </summary>
	[SerializeField] private RectTransform holderTransform;

	/// <summary>
	/// The list of cards follows the parent card.
	/// </summary>
	protected List < CardBehaviour > cardFollows = new List<CardBehaviour>();
	 
	#endregion

	/// <summary>
	/// Gets the holder transform.
	/// </summary>
	/// <returns>The holder transform.</returns>
	public Transform GetHolderTransform()
	{
		// TODO: Return the holder transform.
		return holderTransform;
	}

	/// <summary>
	/// Adds the cards follow.
	/// </summary>
	/// <param name="paramIn">Parameter in.</param>
	public void AddCardsFollow(CardBehaviour paramIn)
	{

		// TODO: Check if this card exists in the list.
		if (cardFollows.Contains (paramIn)) {
			return;
		}

		// TODO: Add the new card into the list.
		cardFollows.Add (paramIn);
	}

	/// <summary>
	/// Removes the card follow.
	/// </summary>
	/// <param name="paramIn">Parameter in.</param>
	public void RemoveCardFollow(CardBehaviour paramIn){
		// TODO: Check if this card exists in the list.
		if (cardFollows.Contains (paramIn)) {

			// TODO: Remove the card.
			cardFollows.Remove (paramIn);
		}
	}

	/// <summary>
	/// Gets the count cards.
	/// </summary>
	/// <returns>The count cards.</returns>
	public int GetCountCards()
	{
		// TODO: Return the number of cards. 
		return cardFollows.Count;
	}

	/// <summary>
	/// Gets the list cards.
	/// </summary>
	/// <returns>The list cards.</returns>
	public List < CardBehaviour > GetListCards(){

		// TODO: Get the list of cards.
		return cardFollows;
	}

	/// <summary>
	/// Refreshs the holder.
	/// </summary>
	public void Clear()
	{
		// TODO: Clear all values.
		cardFollows.Clear ();
	}
}
