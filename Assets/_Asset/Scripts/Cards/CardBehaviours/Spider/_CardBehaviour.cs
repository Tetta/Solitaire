using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ========================================= SPIDER GAME ====================================== //

#region SPIDER

namespace SPIDER
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

		public bool DoCheckPlayingZone(bool IsMoving = true)
		{
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

			// TODO: Get the old zone.
			var zoneOld = PlayingZone.Instance.GetIdTransform (card);

			for (int i = 0; i < paramGet.Count; i++) {

				// TODO: Get the zone of card.
				Enums.IdTransformCard zoneGet = ( Enums.IdTransformCard )paramGet[i];

				// TODO: Create the point to check ready to join.
				var IsReadyToJoin = false;

				// TODO: Check if this zone is not none.
				if (zoneGet != Enums.IdTransformCard.None && zoneOld != zoneGet) {

					// TODO: Get the card.
					card.TargetBehaviour = PlayingZone.Instance.GetTheLastCard (zoneGet);

					// TODO: Check if this card is not null.
					if (!object.ReferenceEquals (card.TargetBehaviour, null)) {

						// TODO: Check if this card can join with this zone.
						if (card.IsReadyToJoinZone (card.TargetBehaviour.GetProperties ())) {

							// TODO: Set ready.
							IsReadyToJoin = true;
						}
					} else {

						if (IsMoving == false && i == 0 )
						{
							// TODO: Set not ready.
							IsReadyToJoin = false;
						}
						else
						{
							// TODO: Set ready.
							IsReadyToJoin = true;
						}				
					}

					// TODO: Check if ready to join.
					if (IsReadyToJoin) {

						// TODO: Record Data.
						UndoSystem.Instance.Record(Enums.Zone.Play, zoneOld , card , card.IsUnlocked());

						// TODO: Get the list cards.
						var cardsFollow = HelperZone.Instance.GetListCards();

						for (int j = cardsFollow.Count - 1; j > -1; j--)
						{               
							// TODO: Record datas.
							UndoSystem.Instance.Record(Enums.Zone.Play, zoneOld, cardsFollow[j], cardsFollow[j].IsUnlocked(), true);
						}


						// TODO: Remove this card.
						PlayingZone.Instance.RemoveTheCard ( zoneOld , card );					

						// TODO: Get the new position need to moving.
						card.TargetPosition = Helper.GetPositionInThePlayingZone ( zoneGet, Enums.Direction.Down, card.IsUnlocked ());

						// TODO: Moving to the new position.
						card.Moving (card.TargetPosition, () => {		

							// TODO: Set the new of parent.
							card.transform.SetParent (PlayingZone.Instance.GetTransformCards (zoneGet));

							// TODO: Sort this card in the view.
							card.transform.SetAsLastSibling ();

							// TODO: Reset all the card follow.
							card.DistributeTheFollowCards ();							

							// TODO: Check the completed of a zone.
							if (!PlayingZone.Instance.DoCompletedAListCards(zoneGet))
							{
								// TODO: Unlocking the last card.
								PlayingZone.Instance.UnlockLastCard();

								// TODO: Update the state of all cards.
								PlayingZone.Instance.UpdateTheStateCardsInZone(zoneGet);
							} 

							// TODO: Update the state of cards.
							PlayingZone.Instance.UpdateTheStateCardsInZone(zoneOld);
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
