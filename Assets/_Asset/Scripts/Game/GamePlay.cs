using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GamePlay : Singleton < GamePlay > {

	public static bool magicWandDialogShown;
    [Header ("UI")]
	[SerializeField] public Transform TDrawCards;
    // =============================== Variables =============================== //

    #region Cache

    // TODO: Get the cache gameplay from Spider.
    public SPIDER._GamePlay _SGamePlay;

    // TODO: Get the cache gameplay from Tripick.
    public TRIPEAKS._GamePlay _TGamePlay;

	// TODO: Get the cache gameplay from Klondike.
	public KLONDIKE._GamePlay _KGamePlay;

	[HideInInspector] 
	public CardBehaviour cardSaveCache;

	[HideInInspector]
	public TimerBehaviours TimeController;
	#endregion



	#region Functional System.

	/// <summary>
	/// Start this instance.
	/// </summary>

	protected override void Awake ()
	{
        AdController.ShowInterstitial();

        // TODO: Reset the main camera.
		Helper.mainCamera = null;

		base.Awake ();
	
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Create the new class.
			_TGamePlay = new TRIPEAKS._GamePlay ();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Create the new class.
			_SGamePlay = new SPIDER._GamePlay ();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Create the new class.
			_KGamePlay = new KLONDIKE._GamePlay ();
			break;
		} 

		// TODO: Add the time controller.
		TimeController = gameObject.AddComponent < TimerBehaviours > ();
	}

	void Start()
	{
        Debug.Log("Gameplay Start");

        switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Create the new class.
			_TGamePlay.InitStart();
			break;
		case Enums.GameScenes.Spider:

			// TODO: Create the new class.

			_SGamePlay.InitStart();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Init start.
			_KGamePlay.InitStart ();
			break;
		}

    }

	#endregion

	#region Init System



	public void StopTimingHandle()
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO:Stop the time.
			_TGamePlay.StopTimingHandle();
			break;
		case Enums.GameScenes.Spider:

			// TODO:Stop the time.
			_SGamePlay.StopTimingHandle();
			break;
		case Enums.GameScenes.Klondike:

			// TODO: Stop the time.
			_KGamePlay.StopTimingHandle ();
			break;
		} 
	}



	#endregion

    #region Condition Solitaire


    /// <summary>
    /// Check the condition to win this game.
    /// </summary>
    public bool IsConditionWining()
    {
		switch (GameManager.Instance.GameType) {
			
		case Enums.GameScenes.Tripeaks:

			// TODO: Check condition wining.
			return _TGamePlay.IsConditionWining ();
		case Enums.GameScenes.Spider:


			// TODO: Check condition wining.
			return _SGamePlay.IsConditionWining ();
		case Enums.GameScenes.Klondike:

			// TODO: Check condition wining.
			return _KGamePlay.IsConditionWining ();
		default:
			
			// TODO: Check condition wining.
			return _SGamePlay.IsConditionWining ();
		}
    }

	public bool IsConditionLosing()
	{
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Check condition losing.
			return _TGamePlay.IsConditionLosing ();
		case Enums.GameScenes.Klondike:

			// TODO: Check condition losing.
			return _KGamePlay.IsConditionLosing ();
		}

		return false;
	}

    public bool IsHintAvailable(bool IsShow = true)
    {
		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Tripeaks:

			// TODO: Checking Hint.
			return _TGamePlay.IsHintAvailable (IsShow);
		case Enums.GameScenes.Spider:


			// TODO: Checking Hint.
			return _SGamePlay.IsHintAvailable (IsShow);

		case Enums.GameScenes.Klondike:

			// TODO: Check hint.
			return _KGamePlay.IsHintAvailable (IsShow);
		default:

			// TODO: Checking Hint.
			return _SGamePlay.IsHintAvailable (IsShow);
		}
    }
    #endregion

    public static bool autoWinShown = false;
	void OnDisable()
	{
		if (!object.ReferenceEquals (Timing.Instance, null)) {
			// TODO: Call the invoke.
			Timing.KillCoroutines (Enums._GameScene [(int)GameManager.Instance.GameType]);
		}
	}
    public void checkAutoWin () {
        //Debug.Log("checkAutoWin: " + autoWinShown);
        if (autoWinShown || GameManager.Instance.GameType != Enums.GameScenes.Klondike) return;
        List<CardBehaviour> cardsGet;
        switch (GameManager.Instance.GameType) {
            case Enums.GameScenes.Tripeaks:
                cardsGet = _TGamePlay.cardsGet;
                break;
            case Enums.GameScenes.Spider:
                cardsGet = _SGamePlay.cardsGet;
                break;
            case Enums.GameScenes.Klondike:
                cardsGet = _KGamePlay.cardsGet;
                break;
            default:
                cardsGet = _KGamePlay.cardsGet;
                break;
        }
        bool allFlips = true;
        foreach (CardBehaviour card in cardsGet) {
            //Debug.Log(card.transform.parent.name);
            //Debug.Log(card.transform.parent.parent.name);
            //Debug.Log(card.transform.parent.parent.parent.name);
            if (!card.isUnlock && card.transform.parent.parent.parent.name == "[D] Playing") allFlips = false;
        }
        //Debug.Log(allFlips);

        if (allFlips) {
            DialogSystem.Instance.ShowDialogAutoWin();
            //StartCoroutine(autoWin());
        }


        //Debug.Log(magicWandDialogShown);
        if ( magicWandDialogShown) return;

        var valueDisplay = PlayingZone.Instance.GetHint();
        
        if (valueDisplay.cardDisplay == null)
        {
             var c = HintZone.Instance.GetHint2();
            //Debug.Log("valueDisplay(): " + c);
            if (c != null) Debug.Log("valueDisplay()2: " + c.GetProperties());
            else {

                magicWandDialogShown = true;
                DialogSystem.Instance.ShowDialogMagicWand();
            }

        } else Debug.Log(valueDisplay.cardDisplay.GetProperties());

    }
    public void autoWin () {
        StartCoroutine(autoWinCoroutine());

    }

    public IEnumerator autoWinCoroutine () {
        
        bool flag = true;
        int counter = 0;
        while (flag) {
            Debug.Log(counter);
            HintValueDisplay hint = PlayingZone.Instance.GetHint();
            if (hint.cardDisplay == null) hint = HintZone.Instance.GetHint();
            if (hint.cardDisplay == null ) {
                //flag = false;
                Debug.Log("hint.cardDisplay");
                Debug.Log(hint.cardDisplay == null);
                
                //break;
            } else 
                hint.cardDisplay.cardClick();

            Debug.Log("IsConditionWining(): " + (IsConditionWining()));
            if (IsConditionWining()) flag = false;
            Debug.Log("!IsHintAvailable(false): " + (!IsHintAvailable(false)));
            if (!IsHintAvailable(false)) {
                EventSystem.Instance.DrawHintCards();
                //flag = false;
            }
            counter++;
            Debug.Log("counter > 1000: " + (counter > 1000));
            if (counter > 1000) flag = false;
            Debug.Log("flag: " + flag);
            Debug.Log("time: " + Time.timeScale);
            yield return new WaitForSeconds(0.1f);
        }
        
        //chechAutoWin();
        /*
        HintValueDisplay hint =  PlayingZone.Instance.GetHint();
        if (hint.cardDisplay == null ) hint = HintZone.Instance.GetHint();
        hint.cardDisplay.cardClick();
        */
    }


    public void magicWand()
    {
        Debug.Log("magicWand");
        magicWandDialogShown = false;
        bool moved = false;

        var zoneCards = PlayingZone.Instance.GetTheListIdZones();
        for (int i = 0; i < zoneCards.Count; i++)
        {
            var cards = PlayingZone.Instance.GetTheListCards((Enums.IdTransformCard)zoneCards[i]);
            foreach (CardBehaviour playingCard in cards)
            {
                if (playingCard.IsUnlocked()) continue;
                Debug.Log(playingCard.GetProperties());
                var resultZoneCards = ResultZone.Instance.GetTheListIdZones();
                for (int j = 0; j < resultZoneCards.Count; j++)
                {
                    var resCard = ResultZone.Instance.GetTheLastCard((Enums.IdTransformCard)resultZoneCards[j]);
                    //if (resCard == null) continue;
                    if (
                        (playingCard.GetProperties().GetCardValue() == 1 && resCard == null)
                        ||
                        resCard != null && playingCard.IsReadyToJoinZone(resCard.GetProperties(), true, true)

                        )
                    {
                        Debug.Log("111111111111111");
                        Debug.Log(playingCard.GetProperties());
                        Debug.Log((Enums.IdTransformCard)resultZoneCards[j]);
                        if (resCard != null) Debug.Log(resCard.GetProperties());
                        PlayingZone.Instance.RemoveTheCard(playingCard);
                        ResultZone.Instance.AddTheCard((Enums.IdTransformCard)resultZoneCards[j], playingCard);

                        playingCard.Unlock(true, () => {

                            // TODO: Ready to draw another cards.
                            // playingCard.IsReady = true;
                        });

                        playingCard.Moving(ResultZone.Instance.GetTransformCards((Enums.IdTransformCard)resultZoneCards[j]).position,
                            ResultZone.Instance.GetTransformCards((Enums.IdTransformCard)resultZoneCards[j]), () => {

                                // TODO: Set the target position x for the card.
                                playingCard.TargetPosition.x = playingCard.transform.position.x;

                                // TODO: Set the target position y for the card.
                                playingCard.TargetPosition.y = playingCard.transform.position.y;

                                // TODO: Set the target position z for the card.
                                playingCard.TargetPosition.z = playingCard.transform.position.z;


                            });
                        moved = true;
                        break;
                    }
                    if (moved) break;
                }
                if (moved) break;

            }
            if (moved) break;
        }


    }

}


/// <summary>
/// The properties of card hint will be shown on the hud.
/// </summary>
[System.Serializable]
public struct HintValueDisplay
{
    // TODO: Card will be display.
    public CardBehaviour cardDisplay; 

    // TODO: moving to.
    public Vector3 positionTarget;
}


