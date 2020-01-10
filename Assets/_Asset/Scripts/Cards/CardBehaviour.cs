using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardBehaviour : MonoBehaviour , IDragHandler , IBeginDragHandler , IEndDragHandler , IPointerClickHandler {

	#region Variables

	/// <summary>
	/// The properties.
	/// </summary>
	protected CardDataProperties properties;

	/// <summary>
	/// The cache of transform.
	/// </summary>

	protected new Transform transform;

    [HideInInspector]
    /// <summary>
    /// The target or current position of cards.
    /// </summary>
    public Vector3 TargetPosition;

	/// <summary>
	/// The card unlocked?
	/// </summary>
	public bool isUnlock;

	[HideInInspector]
	/// <summary>
	/// The state touched.
	/// </summary>
	public Enums.StateTouch stateTouched;

	/// <summary>
	/// The state card.
	/// </summary>
	protected Enums.StateCard stateCard;

	[HideInInspector]
	/// <summary>
	/// The target properties.
	/// </summary>
	public CardBehaviour TargetBehaviour;

    [Header("ANIMATION")]
    [SerializeField]
    private Animation sourcePlay;

    [SerializeField]
    private AnimationClip failedCollectCard;

	#endregion

	#region Requirements

	[Header ("Requirements")]

	/// <summary>
	/// The user interface renderer.
	/// </summary>
	[SerializeField] Image UIRenderer;

	/// <summary>
	/// The user interface card trigger.
	/// </summary>
	[SerializeField] Image UICardTrigger;
	#endregion



	// ======================= Variables ========================= //
	/// <summary>
	/// The distance pointer.
	/// </summary>
	protected Vector3 distancePointer;

	/// <summary>
	/// The position follow.
	/// </summary>
	protected Vector3 positionFollow;

	[HideInInspector]
	/// <summary>
	/// The parent transform.
	/// </summary>
	public Transform parentTransform;

	// TODO: Get the cache of function Tripick.
	protected TRIPEAKS._CardBehaviour _TCardBehaviour;

	// TODO: Get the cache of function Tripick.
	protected SPIDER._CardBehaviour _SCardBehaviour;

	// TODO: Get the cache of function Klondike.
	protected KLONDIKE._CardBehaviour _KCardBehaviour;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// TODO: Set the cache of transform.

		transform = gameObject.transform;

		// TODO: Get the default of cards.
		UIRenderer.sprite = DataSystem.Instance.GetDefaultCard(Contains.GetThemeType);

		// TODO: Get the default of cards trigger.
		UICardTrigger.sprite = DataSystem.Instance.GetDefaultCard (Contains.GetThemeType);
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		// TODO: Reset the state of touches.
		stateTouched = Enums.StateTouch.None;

		// TODO: Reset the state of card.
		stateCard = Enums.StateCard.None;

		// TODO: Rize the images of cards.
		ResizeImage ();
	}

	public void ResetCards()
	{
		// TODO: Reset the state of touches.
		stateTouched = Enums.StateTouch.None;

		// TODO: Reset the state of card.
		stateCard = Enums.StateCard.None;

		// TODO: Rize the images of cards.
		ResizeImage ();

		switch (GameManager.Instance.GameType) {
		case Enums.GameScenes.Spider:

			if (_SCardBehaviour == null) {

				// TODO: Create the new class.
				_SCardBehaviour = new SPIDER._CardBehaviour (this);
			}
			break;
		case Enums.GameScenes.Tripeaks:

			if (_TCardBehaviour == null) {
				// TODO: Create the new class.
				_TCardBehaviour = new TRIPEAKS._CardBehaviour (this);
			}
			break;
		case Enums.GameScenes.Klondike:

			if (_KCardBehaviour == null) {
				// TODO: Create the new class.
				_KCardBehaviour = new KLONDIKE._CardBehaviour (this);
			}
			break;
		}

		// TODO: Reset the color.
		UIRenderer.color = new Color (UIRenderer.color.r, UIRenderer.color.g, UIRenderer.color.b, 1f);
	}

	void ResizeImage()
	{
		switch (GameManager.Instance.GameType) {
			
		case Enums.GameScenes.Tripeaks:

			// TODO: Set the default size display.
			UIRenderer.rectTransform.sizeDelta = Contains._TCardSize;

			// TODO: Set the default size display.
			UICardTrigger.rectTransform.sizeDelta = Contains._TCardSize;

			break;
		case Enums.GameScenes.Spider:

			// TODO: Set the default size display.
			UIRenderer.rectTransform.sizeDelta = Contains._SCardSize;

			// TODO: Set the default size display.
			UICardTrigger.rectTransform.sizeDelta = Contains._SCardSize;

			break;		

		case Enums.GameScenes.Klondike:

			// TODO: Set the default size display.
			UIRenderer.rectTransform.sizeDelta = Contains._KCardSize;

			// TODO: Set the default size display.
			UICardTrigger.rectTransform.sizeDelta = Contains._KCardSize;
			break;
		}
	}

	/// <summary>
	/// Init the specified data.
	/// </summary>
	/// <param name="data">Data.</param>
	public void Init(CardDataProperties data)
	{
		properties = data;
	}

	public void EnableCards()
	{
		if (gameObject.activeSelf == false) {
			gameObject.SetActive (true);
		}
	}

	#region Event System

	/// <summary>
	/// Raises the drag event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnDrag (PointerEventData eventData)
	{
		// TODO: if the state of this card is not ready. break the functions.
		if (!IsReady ())
			return;

		// TODO: Doing something.

		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Spider:

			// TODO: Call Drag function.
			_SCardBehaviour.OnTouchDrag ();
			break;
		case Enums.GameScenes.Tripeaks:
			break;

		case Enums.GameScenes.Klondike:

			// TODO: Call Drag function.
			_KCardBehaviour.OnTouchDrag ();
			break;
		}
	}

	/// <summary>
	/// Raises the begin drag event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnBeginDrag (PointerEventData eventData)
	{     

        // TODO: if the state of this card is not ready. break the functions.
        if (!IsReady ())
			return;

		// TODO: Doing something.

		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Spider:

			// TODO: Call Begin Drag function.
			_SCardBehaviour.OnTouchBeginDrag ();
			break;
		case Enums.GameScenes.Tripeaks:
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Call Begin Drag function.
			_KCardBehaviour.OnTouchBeginDrag ();
			break;
		}
	}

	/// <summary>
	/// Raises the end drag event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnEndDrag (PointerEventData eventData)
	{
        
        // TODO: if the state of this card is not ready. break the functions.
        if (!IsReady ())
			return;


        // TODO: Doing something.

        switch (GameManager.Instance.GameType) {
			
		case Enums.GameScenes.Spider:

			// TODO: Call End Drag function.
			_SCardBehaviour.OnTouchEndDrag ();
			break;
		case Enums.GameScenes.Tripeaks:
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Call End Drag function.
			_KCardBehaviour.OnTouchEndDrag ();
			break;
		}

        GamePlay.Instance.checkAutoWin();
    }

	/// <summary>
	/// Raises the pointer click event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick (PointerEventData eventData)
	{
        //Debug.Log("Card OnPointerClick");
        // TODO: if the state of this card is not ready. break the functions.

        if (!IsReady())
            return;
        

        // TODO: Turn off hint.
        HintDisplay.Instance.DisableHint();

        // TODO: Check if the card is using.
        if (stateTouched != Enums.StateTouch.None) {

            // TODO: Break the function.
            return;
        }

        SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

        // TODO: update the number of move.
        UIBehaviours.Instance.UpdateMove(1, true);

        // TODO: Set the state of touch.
        stateTouched = Enums.StateTouch.Touch;

        // TODO: Set Up the information before following.
        BeginFollow();
        GamePlay.Instance.checkAutoWin();
        // TODO: Check the Playing zone.
        if (DoCheckPlayingZone(false)) {

            // TODO: Reset the state of touching.
            stateTouched = Enums.StateTouch.None;

            // TODO: Break the functions.
            return;
        }

        // TODO: Reset the state of touching.
        stateTouched = Enums.StateTouch.None;
        //Debug.Log("--TargetPosition: " + TargetPosition);
        //Debug.Log("--pos: " + transform.position);
        // TODO: Back to the current position.

        Moving(TargetPosition, parentTransform, () => {

            // TODO: Distribute the follow cards.
            DistributeTheFollowCards();

            // TODO: Failed Collect;
            AnimationFailedCollect();

            
        });


    }

    public void cardClick () {
        // TODO: if the state of this card is not ready. break the functions.
        if (!IsReady()) {
            EventSystem.Instance.DrawHintCards();
            return;
        }

        // TODO: Turn off hint.
        HintDisplay.Instance.DisableHint();

        // TODO: Check if the card is using.
        if (stateTouched != Enums.StateTouch.None) {

            // TODO: Break the function.
            return;
        }

        SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

        // TODO: update the number of move.
        UIBehaviours.Instance.UpdateMove(1, true);

        // TODO: Set the state of touch.
        stateTouched = Enums.StateTouch.Touch;

        // TODO: Set Up the information before following.
        BeginFollow();

        // TODO: Check the Playing zone.
        if (DoCheckPlayingZone(false)) {

            // TODO: Reset the state of touching.
            stateTouched = Enums.StateTouch.None;

            // TODO: Break the functions.
            return;
        }

        // TODO: Reset the state of touching.
        stateTouched = Enums.StateTouch.None;
        //Debug.Log("--TargetPosition: " + TargetPosition);
        //Debug.Log("--pos: " + transform.position);
        // TODO: Back to the current position.
        Moving(TargetPosition, parentTransform, () => {

            // TODO: Distribute the follow cards.
            DistributeTheFollowCards();

            // TODO: Failed Collect;
            AnimationFailedCollect();

        });
    }
	#endregion

	#region Helper

	/// <summary>
	/// Gets the properties.
	/// </summary>
	/// <returns>The properties.</returns>
	public CardDataProperties GetProperties()
	{
		// TODO: Return the properties of card.
		return properties;
	}

	/// <summary>
	/// Get the type of the card.
	/// </summary>
	/// <value>The type of the card.</value>
	public Enums.CardType GetTypeCards()
	{
		// TODO: return the type of card from data.
		return properties.GetCardType ();
	}

	/// <summary>
	/// Determines whether this instance is same color card the specified anotherCard.
	/// </summary>
	/// <returns><c>true</c> if this instance is same color card the specified anotherCard; otherwise, <c>false</c>.</returns>
	/// <param name="anotherCard">Another card.</param>
	public bool IsSameColorCard(Enums.CardType anotherCard)
	{
		var cardType = GetTypeCards ();
		
		return (anotherCard == Enums.CardType.Diamond || anotherCard == Enums.CardType.Heart) && (cardType == Enums.CardType.Heart || cardType == Enums.CardType.Diamond)
			|| (anotherCard == Enums.CardType.Club || anotherCard == Enums.CardType.Spade) && (cardType == Enums.CardType.Club || cardType == Enums.CardType.Spade);
	}

	/// <summary>
	/// Gets the value.
	/// </summary>
	/// <returns>The value.</returns>
	public int GetValue()
	{
		// TODO: return the value of card.
		return properties.GetCardValue ();
	}

	/// <summary>
	/// Gets the enums card.
	/// </summary>
	/// <returns>The enums card.</returns>
	public Enums.CardVariables GetEnumsCard()
	{
		return properties.GetEnumCardValue ();
	}

	/// <summary>
	/// Determines whether this instance is unlocked.
	/// </summary>
	/// <returns><c>true</c> if this instance is unlocked; otherwise, <c>false</c>.</returns>
	public bool IsUnlocked()
	{
		// TODO: Return the state unlocking of card.
		return isUnlock;
	}

	/// <summary>
	/// Determines whether this instance is ready to use.
	/// </summary>
	/// <returns><c>true</c> if this instance is ready; otherwise, <c>false</c>.</returns>
	protected bool IsReady()
	{
		// TODO: Return true if the state of game is playing.
		return GameManager.Instance.GetStateGame () == Enums.StateGame.Playing && IsUnlocked() && stateCard == Enums.StateCard.None;
	}

	/// <summary>
	/// Determines whether this instance is state unlock.
	/// </summary>
	public Enums.StateCard GetStateCard()
	{
		return stateCard;
	}

    public void UpdateReadyToUse(Enums.StateCard stateCards)
    {
        // TODO: Check the status of card.
        if (!IsUnlocked())
        {

            // TODO: Lock the card is locking.
            stateCard = Enums.StateCard.Locking;

            // TODO: Break the function.
            return;
        }

        // TODO: Update the state of card.
        stateCard = stateCards;

        switch (stateCard)
        {
            case Enums.StateCard.Locking:

                // TODO: Change the color of cards.
                ChangeColorWithInteractable(false);
                break;
            case Enums.StateCard.None:

                // TODO: Change the color of cards.
                ChangeColorWithInteractable();
                break;
            case Enums.StateCard.Complete:

                // TODO: Change the color of cards.
                ChangeColorWithInteractable();
                break;
        }
    }
	#endregion

	#region Functions

	/// <summary>
	/// Moving to the specified position.
	/// </summary>
	public void Moving(Vector3 position, System.Action OnCompleted = null , bool IsRandomRotation = false)
	{
		// TODO: Stop the state of transform if it is using Dotween.
		transform.DOKill (true);

		// TODO: Moving the transform to the new position.
		transform.DOMove (position, Contains.DurationMoving).OnComplete (() => {

            // TODO: Check null.
			if ( !object.ReferenceEquals ( GamePlay.Instance , null ))
			{

				// TODO: Check if the callback does not null.
				if ( !ReferenceEquals ( OnCompleted , null ))
				{
					// TODO: Run the callback.
					OnCompleted();
				}	
			}		
		});

		if (IsRandomRotation) {
			transform.DORotate (new Vector3 (0, 0, Random.Range (-10, 10)), Contains.DurationMoving);
		} else {
			transform.DORotate (Contains.Vector3Zero, Contains.DurationMoving);
		}
	}

	/// <summary>
	/// Moving to the specified position and set the parent Transform.
	/// </summary>
	public void Moving(Vector3 position, Transform parentTransform , System.Action OnCompleted = null, bool IsRandomRotation = false)
	{
		// TODO: Stop the state of transform if it is using Dotween.
		transform.DOKill (true);

		// TODO: Set the parent transform.
		transform.SetParent (HelperZone.Instance.GetHolderTransform ());

		// TODO: Moving the transform to the new position.
		transform.DOMove (position, Contains.DurationMoving).OnComplete (() => {

			if ( !object.ReferenceEquals ( GamePlay.Instance , null ))
			{				
				// TODO: Set the parent transform.
				transform.SetParent ( parentTransform );

				// TODO: Check if the callback does not null.
				if ( !object.ReferenceEquals ( OnCompleted , null ))
				{
					// TODO: Run the callback.
					OnCompleted();
				}	
			}
		});

		if (IsRandomRotation) {
			transform.DORotate (new Vector3 (0, 0, Random.Range (-10, 10)), Contains.DurationMoving);
		} else {
			transform.DORotate (Contains.Vector3Zero, Contains.DurationMoving);
		}
	}

	public void Hiding(System.Action OnCompleted = null)
	{

		// TODO: Stop the state of image.
		UIRenderer.DOKill (true);

		// TODO: Fade the image.
		UIRenderer.DOFade (0, Contains.DurationMoving).OnComplete (() => {
		
			// TODO: Check the condition of callback.
			if ( !object.ReferenceEquals ( OnCompleted , null ))
			{
			
				// TODO: Run the callback.
				OnCompleted();
			}
		});
	}


	/// <summary>
	/// Follow the specified position.
	/// </summary>
	/// <param name="position">Position.</param>
	public void Follow()
	{
		positionFollow = Helper.GetPositionFromScreenPoint (Input.mousePosition);

		// TODO: Get the position X.
		positionFollow.x = positionFollow.x + distancePointer.x;

		// TODO: Get the position Y.
		positionFollow.y = positionFollow.y + distancePointer.y;

		// TODO: Get the position Z.
		positionFollow.z = positionFollow.z + distancePointer.z;

		// TODO: Get the position of transform.
		transform.position = positionFollow;
	}

	/// <summary>
	/// Setup the distance position and parent transform.
	/// </summary>
	public void BeginFollow()
	{
		// TODO: Get the parent of transform.
		parentTransform = transform.parent;

		// TODO: Get the world position.
		Vector3 positionGet = Helper.GetPositionFromScreenPoint (Input.mousePosition);

		// TODO: Get the position of X.
		distancePointer.x = TargetPosition.x - positionGet.x;

		// TODO: Get the position of Y.
		distancePointer.y = TargetPosition.y - positionGet.y;

		// TODO: Get the position of Z.
		distancePointer.z = TargetPosition.z - positionGet.z;

		// TODO: Set the parent of transform.
		transform.SetParent (HelperZone.Instance.GetHolderTransform ());

		// TODO: Get the follow cards.
		GetTheFollowCards ();
	}

	/// <summary>
	/// Updates the state of card.
	/// </summary>
	protected void UpdateState(bool IsUnlocking , bool IsUsingAnimation = false, System.Action OnCompleted = null)
	{
		// TODO: Check if the state unlock as same as the condition.
		if (IsUnlocked() == IsUnlocking) {

			// TODO: Check if the callback does not null.
			if ( OnCompleted != null )
			{
				// TODO: Run the callback.
				OnCompleted ();
			}

			return;
		}

        // TODO: Create the cache of UIRenderer.

		Transform TUIRenderer = UIRenderer.transform;

		// TODO: Stop the state of transform if it is using Dotween.
		TUIRenderer.DOKill(true);

		// TODO: Doing the Animation.
		if (IsUsingAnimation) {

			// TODO:  Set the state unlocking of card.
			isUnlock = IsUnlocking;

			// TODO: Scale the image to zero.
			TUIRenderer.DOScale (new Vector3 (0, 1.1f, 0), Contains.DurationFade / 2).OnComplete (() => {

                // TODO: Scale the image to one.
                TUIRenderer.DOScale ( new Vector3 ( 1, 1, 1 ) , Contains.DurationFade / 2 ).OnStart ( ()=> {

                    // TODO: Check the state of unlock.
                    if ( IsUnlocking )
					{

                        // TODO: Set the sprite unlocked.
                        UIRenderer.sprite = properties.GetCardSprite ();

					}else{

                        // TODO: Set the sprite locked.
                        UIRenderer.sprite = DataSystem.Instance.GetDefaultCard(Contains.GetThemeType);
					}
				}).OnComplete ( ()=>
					{

                        //UIRenderer.transform.localScale = new Vector3(1, 1, 1);
                        // TODO: Check if the callback does not null.
                        if ( OnCompleted != null )
						{

                            // TODO: Run the callback.
                            OnCompleted();
						}
                        GamePlay.Instance.checkAutoWin();
                    });			
			});				

			// TODO: Break the functions.
			return;
		}

		// TODO: Doing with non animation.

		// TODO: Check the state of unlock.
		if ( IsUnlocking )
		{
			// TODO: Set the state unlocking of card.
			isUnlock = true;

			// TODO: Set the sprite unlocked.
			UIRenderer.sprite = properties.GetCardSprite ();

		}else{

			// TODO: Set the state unlocking of card.
			isUnlock = false;

			// TODO: Set the sprite locked.
			UIRenderer.sprite = DataSystem.Instance.GetDefaultCard(Contains.GetThemeType);
		}

		// TODO: Check if the callback does not null.
		if ( OnCompleted != null )
		{
			// TODO: Run the callback.
			OnCompleted ();
		}

	}

	/// <summary>
	/// Unlock the card.
	/// </summary>
	public void Unlock(bool IsUsingAnimation = false , System.Action OnCompleted = null )
	{

		// TODO: Update the state of card is unlocking.
		UpdateState (true, IsUsingAnimation, OnCompleted);
	}

	/// <summary>
	/// Lock the card.
	/// </summary>
	public void Lock(bool IsUsingAnimation = false , System.Action OnCompleted = null )
	{

		// TODO: Update the state of card is locking.
		UpdateState (false, IsUsingAnimation, OnCompleted);
	}
	#endregion

	#region Game Play

	/// <summary>
	/// Gets the follow cards.
	/// </summary>
	public void GetTheFollowCards()
	{
		// TODO: Check if the list cards is not empty.
		if (HelperZone.Instance.GetCountCards () > 0) {

			// TODO: Distribute all the cards .
			DistributeTheFollowCards ();
		}

		// TODO: Get the id of the list.
		Enums.IdTransformCard id = PlayingZone.Instance.GetIdTransform ( this );

		// TODO: Create the list card will be follow.
		List < CardBehaviour > cardsFollow = PlayingZone.Instance.GetTheListCardsFromIndex (id, this );

		// TODO: Get the count of list.
		int count = cardsFollow.Count;

		// TODO: Break the function if don't have any cards follow.
		if (count == 0)
			return;

		// TODO: Get the cache of card.
		CardBehaviour cardCache = null;

		for (int i = 0; i < count; i++) {

			// TODO: Set the value of cache.
			cardCache = cardsFollow [i];

			// TODO: Add the card into the list.
			HelperZone.Instance.AddCardsFollow (cardCache);

			// TODO: Set the parrent follow.
			cardCache.transform.SetParent (this.transform);

			// TODO: Remove the card from the list.
			PlayingZone.Instance.RemoveTheCard (cardCache);

			if (!object.ReferenceEquals ( cardCache , this)) {

				// TODO: Locking moving.
				cardCache.UpdateReadyToUse (Enums.StateCard.Moving);
			}
		}
	}

	/// <summary>
	/// Distributes the follow cards.
	/// </summary>
	public void DistributeTheFollowCards()
	{
		// TODO: Get the list of cards follow.
		List < CardBehaviour > cardsFollow = HelperZone.Instance.GetListCards ();

		// TODO: Get the count of list.
		int count = cardsFollow.Count;

		// TODO: Break the function if don't have any cards follow.
		if (count == 0) {
			return;
		}

		// TODO: Get the id of the list.
		Enums.IdTransformCard id = PlayingZone.Instance.GetIdTransform ( this );

		// TODO: Get the parent transform.
		Transform parentFollow = PlayingZone.Instance.GetTransformCards (id);

		// TODO: Get the cache of card.
		CardBehaviour cardCache = null;

		// TODO: Loop to distribute the childs.
		for (int i = 0; i < cardsFollow.Count; i++) {         

            // TODO: Set the cache.
            cardCache = cardsFollow [i];

            // TODO: Set the parent.
            cardCache.transform.SetParent (parentFollow);

			// TODO: Get the target position.
			cardCache.TargetPosition = Helper.GetPositionInThePlayingZone (id, Enums.Direction.Down, cardCache.IsUnlocked());
            

			// TODO: Set the index view.
			cardCache.transform.SetAsLastSibling();

			// TODO: Update the new card to the list.
			PlayingZone.Instance.AddTheCard (id, cardCache );

			// TODO: Unlocking the state.
			cardCache.UpdateReadyToUse (Enums.StateCard.None);
		}

		// TODO: Clear all the old cards in the list.
		HelperZone.Instance.Clear();
	}

	/// <summary>
	/// Determines whether this instance can use for playing zone.
	/// </summary>
	/// <returns><c>true</c> if this instance can use for playing zone; otherwise, <c>false</c>.</returns>
	public bool DoCheckPlayingZone(bool IsMoving = true)
	{
		switch (GameManager.Instance.GameType) {
			
		case Enums.GameScenes.Spider:

			return _SCardBehaviour.DoCheckPlayingZone (IsMoving);
		case Enums.GameScenes.Tripeaks:
			
			return _TCardBehaviour.DoCheckPlayingZone (IsMoving);
		case Enums.GameScenes.Klondike:
			
			return _KCardBehaviour.DoCheckPlayingZone (IsMoving);
		}

		return false;
	}

	#endregion

	#region Soliatire Condition

	/// <summary>
	/// Determines whether this instance is ready to join zone the specified target.
	/// </summary>
	/// <returns><c>true</c> if this instance is ready to join zone the specified target; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	public bool IsReadyToJoinZone(CardDataProperties target, bool IsTargetBiggerThan = false , bool IsSameColor = true)
	{
		if (IsTargetBiggerThan) {
		
			// TODO: Check if this card has the value smaller than target and has the same card type.
			if (properties.GetCardValue () == target.GetCardValue () + 1) {

				if (!IsSameColor) {

					if (!IsSameColorCard (target.GetCardType ())) {

						return true;
					}
				} else {

					if (properties.GetCardType () == target.GetCardType ()) {

						// TODO: Return true if this condition is true.
						return true;
					}
				}
			}

		} else {

			// TODO: Check if this card has the value smaller than target and has the same card type.
			if (properties.GetCardValue () == target.GetCardValue () - 1 ) {

				if (!IsSameColor) {

					if (!IsSameColorCard (target.GetCardType ())) {
				
						return true;
					}
				} else {

					if (properties.GetCardType () == target.GetCardType ()) {
						
						// TODO: Return true if this condition is true.
						return true;
					}
				}
			}
		}

		// TODO: Return false.
		return false;
	}

	// TODO: kill the dotween.
	public void StopMoving(bool IsRuningAction = false){

		// TODO: Stop the dotween.
		transform.DOKill (IsRuningAction);
	}

	public void ChangeColorWithInteractable(bool Interactable = true)
	{
		if (Interactable) {
		
			UIRenderer.color = Color.white;

			return; 
		}

		UIRenderer.color = new Color (0.8f, 0.8f, 0.8f, 1);
	}

    public void AnimationFailedCollect()
    {
        // TODO: Playing failed animation.
        sourcePlay.Play(failedCollectCard.name);
    }

	void OnDisable()
	{
		// TODO: Reset the color.
		UIRenderer.color = new Color (UIRenderer.color.r, UIRenderer.color.g, UIRenderer.color.b, 1f);
	}
	#endregion
}


