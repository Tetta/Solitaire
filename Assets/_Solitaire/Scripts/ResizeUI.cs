using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResizeUI : MonoBehaviour
{
    public CanvasScaler resultCanvas;
    public CanvasScaler playingCanvas;
    public CanvasScaler hintCanvas;
    public CanvasScaler drawCanvas;
    public CanvasScaler helperGroundsCanvas;
    public CanvasScaler helperHintCanvas;

    public HorizontalLayoutGroup playingZone;

    bool screenVert;
    bool screenVert2;
    int screenW1;

    int screenW2;
    private void Awake() {
        //screenVert = Screen.currentResolution.height > Screen.currentResolution.width;
        screenVert = Screen.height > Screen.width;
        updateView();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //screenVert2 = Screen.currentResolution.height > Screen.currentResolution.width;
        screenVert2 = Screen.height > Screen.width;
        if (screenVert != screenVert2) {
            screenVert = screenVert2;
            updateView();

        }

        screenW2 = Screen.width;
        //Debug.Log(screenW2);
        if (screenW1 != screenW2) {
            screenW1 = screenW2;
            updateView();

        }
    }
    void updateView() {
        
        if (screenVert) {
            resultCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            playingCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            hintCanvas.matchWidthOrHeight = 0f;
            drawCanvas.matchWidthOrHeight = 0f;
            helperGroundsCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            helperHintCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            playingZone.childForceExpandWidth = true;
        }
        else {
            resultCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            resultCanvas.matchWidthOrHeight = 0.7f;
            playingCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            playingCanvas.matchWidthOrHeight = 0.7f;
            hintCanvas.matchWidthOrHeight = 0.7f;
            drawCanvas.matchWidthOrHeight = 0.7f;
            helperGroundsCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            helperGroundsCanvas.matchWidthOrHeight = 0.7f;
            helperHintCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            helperHintCanvas.matchWidthOrHeight = 0.7f;
            playingZone.childForceExpandWidth = false;
        }
        
        
        //playingZone.childForceExpandWidth = false;
        /*
        resultCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        resultCanvas.matchWidthOrHeight = 1f;
        playingCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        playingCanvas.matchWidthOrHeight = 1;

        hintCanvas.matchWidthOrHeight = 1;
        drawCanvas.matchWidthOrHeight = 1;
        helperGroundsCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        helperGroundsCanvas.matchWidthOrHeight = 1;
        helperHintCanvas.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        helperHintCanvas.matchWidthOrHeight = 1;
        */
        
        playingZone.childForceExpandWidth = false;
       
        
        if (GamePlay.Instance != null && GamePlay.Instance._KGamePlay != null && GamePlay.Instance._KGamePlay.cardsGet != null && GamePlay.Instance._KGamePlay.cardsGet.Count > 0)
            for (int i = 0; i < 28; i++) {
                // TODO: Set value of card.
                CardBehaviour cardCache = GamePlay.Instance._KGamePlay.cardsGet[i];
                //Debug.Log(cardCache);
                // TODO: Check if this card is null.
                if (cardCache != null) {
                       //cardCache.TargetPosition = cardCache.gameObject.transform. position;
                    }
            }
        if (GamePlay.Instance != null && GamePlay.Instance._TGamePlay != null && GamePlay.Instance._TGamePlay.cardsGet != null && GamePlay.Instance._TGamePlay.cardsGet.Count > 0)
            for (int i = 0; i < GamePlay.Instance._SGamePlay.cardsGet.Count; i++) {
                // TODO: Set value of card.
                CardBehaviour cardCache = GamePlay.Instance._TGamePlay.cardsGet[i];
                //Debug.Log(cardCache);
                // TODO: Check if this card is null.
                if (cardCache != null) {
                    //cardCache.TargetPosition = cardCache.gameObject.transform.position;
                }
            }
        if (GamePlay.Instance != null && GamePlay.Instance._SGamePlay != null && GamePlay.Instance._SGamePlay.cardsGet != null && GamePlay.Instance._SGamePlay.cardsGet.Count > 0)
            for (int i = 0; i < GamePlay.Instance._SGamePlay.cardsGet.Count; i++) {
                // TODO: Set value of card.
                CardBehaviour cardCache = GamePlay.Instance._SGamePlay.cardsGet[i];

                // TODO: Check if this card is null.
                if (cardCache != null) {
                    //Debug.Log(cardCache.gameObject.transform.position);
                    //cardCache.TargetPosition = cardCache.gameObject.transform.position;
                }
            }

        //GamePlay.Instance._KGamePlay.cardsGet
        //playingZone.childForceExpandWidth = false;
        //Helper.SortCards(HintZone.Instance.GetTheListCards(Enums.IdTransformCard.TransformCards_B), Enums.Direction.Right, HintZone.Instance.GetDefaultPosition(Enums.IdTransformCard.TransformCards_B));
    }
}
