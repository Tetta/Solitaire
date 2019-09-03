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
    int screenH1;

    int screenH2;
    float cameraSizeAwake;
    float dDefault;
    private void Awake() {
        //screenVert = Screen.currentResolution.height > Screen.currentResolution.width;
        //screenVert = Screen.height > Screen.width;
        //updateView();
        cameraSizeAwake = Camera.main.orthographicSize;
        dDefault = (float)Screen.width / Screen.height;
        updateView2();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //screenVert2 = Screen.currentResolution.height > Screen.currentResolution.width;
        //screenVert2 = Screen.height > Screen.width;
        //if (screenVert != screenVert2) {
        //    screenVert = screenVert2;
            //updateView();

        //}

        screenW2 = Screen.width;
        screenH2 = Screen.height;
        //Debug.Log(screenW2);
        if (screenW1 != screenW2 || screenH1 != screenH2) {
            screenW1 = screenW2;
            screenH1 = screenH2;
            //updateView();
            updateView2();
        }
    }
    void updateView2() {



        float d = (float)Screen.width / Screen.height;
        float scale = dDefault / d;



        Debug.Log("scale " + scale);

        //if (d > 0.4618f && d < 1) {
        // d = d - 0.4618f;
        // Debug.Log(d);
        //d = (d / 0.01f) * (-60);// (-44.48f);
        //Debug.Log(d);
        //d = 3350 + d;

        //Debug.Log(d);
        //d = (float)Screen.height / Screen.width * 1500 + 100;

        d = (float)Screen.height / Screen.width * 1550;

        if ((GameManager.Instance.GameType == Enums.GameScenes.Klondike || GameManager.Instance.GameType == Enums.GameScenes.Tripeaks) && d < 1550) {
            float ration = d / 1550;
            scale = scale / ration;
            d = 1550;

        } else if (GameManager.Instance.GameType == Enums.GameScenes.Spider && d < 2400) {
            float ration = d / 2400;
            scale = scale / ration;
            d = 2400;
        }
                Camera.main.orthographicSize = 10f * scale;

            resultCanvas.referenceResolution = new Vector2(1500, d);
            playingCanvas.referenceResolution = new Vector2(1500, d);
            hintCanvas.referenceResolution = new Vector2(1500, d);
            drawCanvas.referenceResolution = new Vector2(1500, d);
            helperGroundsCanvas.referenceResolution = new Vector2(1500, d);
            helperHintCanvas.referenceResolution = new Vector2(1500, d);
            resultCanvas.referenceResolution = new Vector2(1500, d);

        //resultCanvas.transform.parent.scal
            Camera.main.transform.localPosition = new Vector3(-251, -157 + (1 - scale) * 10f);
            Debug.Log("cam pos " + new Vector3(-251, -157 + (1 - scale) * 10f));
        //}


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
