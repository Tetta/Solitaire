using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoSystem : Singleton < UndoSystem > {

    // TODO: Undo Ready.
    public bool IsUndoReady = true;

	private struct RecordDatas
	{
        // TODO: get the parent transform.
        public Enums.IdTransformCard idParents;

        // TODO: Get the zone.
        public Enums.Zone zone;

        // TODO: State of unlock.
        public bool IsUnlock;

        // TODO: The will be return.
        public CardBehaviour cards;

		// TODO: The card will be saved.
		public CardBehaviour cardSave;

		// TODO: Get the score.
		public int score;

		// TODO: Get the state is this follow card or not.
		public bool IsFollowCard ;
	}

	/// <summary>
	/// The records data.
	/// </summary>
	private Dictionary < int , List < RecordDatas  >> recordsData = new Dictionary<int, List <RecordDatas>>();

	public void Record(Enums.Zone zone, Enums.IdTransformCard id, CardBehaviour card, bool IsUnlock, bool IsRecordInTheLastData = false , bool IsFollowCard = true)
    {
        List<RecordDatas> datas;

        if (IsRecordInTheLastData == true)
        {

            if (recordsData.Count == 0)
            {

                // TODO: Break the function.
                return;
            }
            else
            {

                // TODO: Get the list of record.
                recordsData.TryGetValue(recordsData.Count - 1, out datas);

                if (object.ReferenceEquals(datas, null))
                {

                    datas = new List<RecordDatas>();
                }
            }
        }
        else
        {

            datas = new List<RecordDatas>();
        }

        RecordDatas data = new RecordDatas();

        // ======================================== Set the value ================================= //

        // TODO: Update the id transform.
        data.idParents = id;

        // TODO: Update the zone.
        data.zone = zone;

        // TODO: Update the state unlock.
        data.IsUnlock = IsUnlock;

        // TODO: Set the default card.
        data.cards = card;

		// TODO: set the card save.
		data.cardSave = GamePlay.Instance.cardSaveCache;

		// TODO: Set the score.
		data.score = Contains.Score;

		// TODO: Set the state follow.
		data.IsFollowCard = IsFollowCard;

        // ======================================== Ends ================================= //

        datas.Add(data);

        if (IsRecordInTheLastData)
        {
            recordsData[recordsData.Count - 1] = datas;
        }
        else
        {
            recordsData.Add(recordsData.Count, datas);
        }
    }

    public void Clear()
    {

        // TODO: Clear the data current.
        recordsData.Clear();
    }

    public void Undo()
    {
        // TODO: check if undo is ready.
        if (recordsData.Count == 0)
            return;

        // TODO: lock the undo.
        IsUndoReady = false;

        // TODO: Create the list .
        List<RecordDatas> record = recordsData[recordsData.Count - 1];

        // TODO: check if the list is null.
        if (object.ReferenceEquals(record, null) || record.Count == 0)
        {
            // TODO: Unlock the undo.
            IsUndoReady = true;

            recordsData.Remove(recordsData.Count - 1);

            // TODO: break the functions.
            return;
        }

        // TODO: Clear the holder.
        HelperZone.Instance.Clear();

        for ( int  i = record.Count - 1; i  > -1; i-- )
        {
            var data = record[i];

            // TODO: Check if not moving.
            if ( data.idParents == Enums.IdTransformCard.None )
            {
                if ( data.IsUnlock )
                {
                    // TODO: Unlock the current cards.
                    data.cards.Unlock(true);
                }
                else
                {
                    // TODO: Unlock the current cards.
                    data.cards.Lock(true);
                }

                continue;
            }		

			// TODO: Set the defaule score.
			Contains.Score = data.score;

            // TODO: Remove the cards currents.
            PlayingZone.Instance.RemoveTheCard(data.cards);

            // TODO: If this is first cards.
            if ( i == 0 )
            { 
				// TODO: Get the target position;
				Vector3 positionTarget = Contains.Vector3Zero;

				// TODO: Get the parent.
				Transform parrent = null;

				// TODO: Remove the cards.
				PlayingZone.Instance.RemoveTheCard (data.cards);

				// TODO: Remove the cards.
				ResultZone.Instance.RemoveTheCard (data.cards);

				// TODO: Remove the cards from the hint zone.
				HintZone.Instance.RemoveTheCard (data.cards);

				switch (record [i].zone) {
					
				case Enums.Zone.Play:

					positionTarget = Helper.GetPositionInThePlayingZone( data.idParents, Enums.Direction.Down, PlayingZone.Instance.IsLastCardUnlocked(data.idParents));

					// TODO: Get the parent.
					parrent = PlayingZone.Instance.GetTransformCards(data.idParents);

					// TODO: Update the holder zone to moving.
					data.cards.transform.SetParent(HelperZone.Instance.GetHolderTransform());

					// TODO: Update the target position.
					data.cards.TargetPosition = positionTarget;

					// TODO: Moving.
					data.cards.Moving(positionTarget, () => {

						// TODO: Set the new of parent.
						data.cards.transform.SetParent(parrent);

						// TODO: Sort this card in the view.
						data.cards.transform.SetAsLastSibling();

						// TODO: Reset all the card follow.
						data.cards.DistributeTheFollowCards();

						// TODO: Update the state of cards.
						PlayingZone.Instance.UpdateTheStateCardsInZone();

						// TODO: Update the ready Undo.
						IsUndoReady = true;

					});

					// TODO: update the cards.
					PlayingZone.Instance.AddTheCard(data.idParents, data.cards);

					break;
				case Enums.Zone.Hint:
					
					// TODO: Get the target position;
					positionTarget = Helper.GetPositionInTheHintZone (data.idParents, Enums.Direction.None, false);

					// TODO: Get the parent from the position.
					parrent = HintZone.Instance.GetTransformCards (data.idParents);

					// TODO: Update the holder zone to moving.
					data.cards.transform.SetParent (HelperZone.Instance.GetHolderTransform ());

					// TODO: Update the target position.
					data.cards.TargetPosition = positionTarget;

					// TODO: Moving.
					data.cards.Moving (positionTarget, () => {

						// TODO: Set the new of parent.
						data.cards.transform.SetParent (parrent);

						// TODO: Sort this card in the view.
						data.cards.transform.SetAsLastSibling ();

						// TODO: Reset all the card follow.
						data.cards.DistributeTheFollowCards ();					

						switch ( GameManager.Instance.GameType )
						{
						case Enums.GameScenes.Klondike:
						
							// TODO: Sort the cards of hint zone.
							Helper.SortCards ( HintZone.Instance.GetTheListCards (Enums.IdTransformCard.TransformCards_B), Enums.Direction.Right , HintZone.Instance.GetDefaultPosition (Enums.IdTransformCard.TransformCards_B), 3);

							break;
						}

						// TODO: Update the ready Undo.
						IsUndoReady = true;

					}, true);

					// TODO: update the cards.
					HintZone.Instance.AddTheCard(data.idParents, data.cards);							

					break;

				case Enums.Zone.Result:

					// TODO: Get the target position;
					positionTarget = Helper.GetPositionInTheResultZone (data.idParents, Enums.Direction.None, false);

					// TODO: Get the parent from the position.
					parrent = ResultZone.Instance.GetTransformCards (data.idParents);

					// TODO: Update the holder zone to moving.
					data.cards.transform.SetParent (HelperZone.Instance.GetHolderTransform ());

					// TODO: Update the target position.
					data.cards.TargetPosition = positionTarget;

					// TODO: Moving.
					data.cards.Moving (positionTarget, () => {

						// TODO: Set the new of parent.
						data.cards.transform.SetParent (parrent);

						// TODO: Sort this card in the view.
						data.cards.transform.SetAsLastSibling ();

						// TODO: Reset all the card follow.
						data.cards.DistributeTheFollowCards ();					

						// TODO: Update the ready Undo.
						IsUndoReady = true;

					}, GameManager.Instance.GameType == Enums.GameScenes.Tripeaks );

					// TODO: update the cards.
					ResultZone.Instance.AddTheCard(data.idParents, data.cards);	

					break;
				}	

				// TODO: Check the state of unlock.
				if (data.IsUnlock)
				{
					// TODO: Unlock the current cards.
					data.cards.Unlock();
				}
				else
				{
					// TODO: Unlock the current cards.
					data.cards.Lock();
				}	

				// TODO: Get the card saved.
				GamePlay.Instance.cardSaveCache = data.cardSave;

                continue;
            }		

			// TODO: Check the state unlock
			if (data.IsFollowCard) {

				// TODO: add the follow cards.
				HelperZone.Instance.AddCardsFollow (data.cards);

				// TODO: Set the parrent.
				data.cards.transform.SetParent (record [0].cards.transform);
			} else {

				// TODO: Remove the cards.
				PlayingZone.Instance.RemoveTheCard (data.cards);

				// TODO: Remove the cards.
				ResultZone.Instance.RemoveTheCard (data.cards);

				// TODO: Remove the cards from the hint zone.
				HintZone.Instance.RemoveTheCard (data.cards);

				switch (data.zone) {
					
				case Enums.Zone.Hint:

					HintZone.Instance.AddTheCard (data.idParents, data.cards);

					// TODO: Set the parrent.
					data.cards.transform.SetParent (HintZone.Instance.GetTransformCards (data.idParents));
					break;
				case Enums.Zone.Play:

					PlayingZone.Instance.AddTheCard (data.idParents, data.cards);

					// TODO: Set the parrent.
					data.cards.transform.SetParent (PlayingZone.Instance.GetTransformCards (data.idParents));
					break;
				case Enums.Zone.Result:

					ResultZone.Instance.AddTheCard (data.idParents, data.cards);

					// TODO: Set the parrent.
					data.cards.transform.SetParent (ResultZone.Instance.GetTransformCards (data.idParents));
					break;
				}
			}

			// TODO: Check the state of unlock.
			if (data.IsUnlock) {
				// TODO: Unlock the current cards.
				data.cards.Unlock ();
			} else {
				// TODO: Unlock the current cards.
				data.cards.Lock ();
			}
        }

		if (!object.ReferenceEquals (UIBehaviours.Instance, null)) {

			// TODO: Update the state score.
			UIBehaviours.Instance.UpdateScore ();
		}

        // TODO: Remove the last records;
        recordsData.Remove(recordsData.Count - 1);
    }
}
