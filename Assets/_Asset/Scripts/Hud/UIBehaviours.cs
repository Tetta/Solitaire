using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// User interface behaviours.
/// </summary>
public class UIBehaviours : Singleton < UIBehaviours > {

	[Header("TOP UI")]

	/// <summary>
	/// The user interface time.
	/// </summary>
	[SerializeField] private Text UITime;

	/// <summary>
	/// The user interface score.
	/// </summary>
	[SerializeField] private Text UIScore;

	/// <summary>
	/// The user interface move.
	/// </summary>
	[SerializeField] private Text UIMove;

    /// <summary>
    /// The user interface bottom.
    /// </summary>
    [SerializeField] private RectTransform UIBottom;

    protected bool IsMovingTop;

	#region Mono

	protected void Start()
	{
        Debug.Log("UIBehaviours Start");
        // TODO: Update the time.
		UpdateTime (0);

		// TODO: Update the score.
		UpdateScore ();

		// TODO: Update the number steps of move.
		UpdateMove (0);

        // TODO: Update the ads display.
        InvokeRepeating("UpdateAds", 1f, 1f);

    }

	#endregion

	#region Functional

	public void UpdateTime(float time , bool IsAnimation = false)
	{
		// TODO: update the value of time.
		UITime.text = Contains.GetDisplayTime(time);

		if (IsAnimation) {

			UITime.rectTransform.DOScale (Vector3.one * 1.1f, 0.2f).OnComplete (() => {

				UITime.rectTransform.DOScale ( Vector3.one , 0.1f);
			}).SetEase (Ease.OutBack);
		}
	}

    protected void UpdateAds()
    {
        bool IsRemoveAds = Contains.IsHavingRemoveAd;
        //point bottom panel
        if (!IAPManager.vip)// AdSystem.Instance.IsBannerShowed && IsMovingTop == false && IsRemoveAds == false)
        {
            //UIBottom.DOAnchorPosY(280f, 0.3f);
            UIBottom.DOAnchorPosY(370f, 0.3f);

            IsMovingTop = true;
        }
        else //if (AdSystem.Instance.IsBannerShowed == false && IsMovingTop == true || AdSystem.Instance.IsUseAdmob == false || IsRemoveAds)
        {
			UIBottom.DOAnchorPosY(125f, 0.3f);

            IsMovingTop = false;
        }

        //if (AdSystem.Instance.IsUseAdmob == false || IsRemoveAds)
        if (IAPManager.vip) { 
            CancelInvoke();
        }
    }

	public void UpdateScore()
	{

        // TODO: Update display.
        UIScore.text = Contains.Score.ToString("00");
	}

    public void ShowScoreAtPoint(Vector3 position, int point = 5)
    {
        // TODO: Add the score.
        Contains.Score += point;

        // TODO: Update the display score.
        UpdateScore();
    }

	public void UpdateMove(int value = 1 , bool IsAnimation = false)
	{
        // TODO: add the value move;
        Contains.Moves += value;

        // TODO: Update display.
        UIMove.text = Contains.Moves.ToString("00");

        if (IsAnimation)
        {
            // TODO: Stop the state current.
            UIMove.DOKill();

            // TODO: Scale animation
            UIMove.rectTransform.DOScale(Vector3.one * 1.05f, 0.1f).OnComplete(() => {

                // TODO: Scale to one.
                UIMove.rectTransform.DOScale(Vector3.one, 0.05f);
            }).SetEase(Ease.OutBack);
        }
    }

	public void DoSelectMenu(){
		
		// TODO: Playing the sound.
		SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

		string title = string.Empty;

		switch (GameManager.Instance.GameType) {

		case Enums.GameScenes.Klondike:

			// TODO: update the title.
			title = "KINGS KLONDIKE";
			break;
		case Enums.GameScenes.Spider:

			// TODO: update the title.
			title= "KINGS SPIDER";
			break;
		case Enums.GameScenes.Tripeaks:

			// TODO: update the title.
			title = "KINGS TRIPEAKS";
			break;
		}

		// TODO: Open dialog.
		DialogSystem.Instance.ShowDialogMessage("Would you like to back to the menu?", title, () =>
			{
				// TODO: stoping the game.
				GameManager.Instance.UpdateState(Enums.StateGame.None);

				// TODO: Check if not null.
				if (!object.ReferenceEquals(GamePlay.Instance, null))
				{
					// TODO: Clear the handles.
					GamePlay.Instance.StopTimingHandle();
				}

				// TODO: Load the new game.
				LoadingBehaviour.Instance.ShowLoading(Enums._GameScene[(int)Enums.GameScenes.Menus], true);

				// TODO: Ready to show ads.
				Contains.IsReadyShowAds = true;
			}, null, null);

		// TODO: Turn off hint.
		HintDisplay.Instance.DisableHint();
	}

    public void DoChangeMode() {

        DialogSystem.Instance.ShowDialogNewGame();
    }

    public void DoNewGame()
	{
        GamePlay.autoWinShown = false;
        GamePlay.magicWandDialogShown = false;
        AnalyticsController.sendEvent("StartGame", new Dictionary<string, object> { { "Type", GameManager.Instance.GameType }, { "Mode", GameManager.Instance.GetModeGame() } });

        DoNewGame(false);
        
    }

    public void DoNewGame(bool IsShowDialog)
    {
        // TODO: Playing the sound.
        SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

        if (IsShowDialog)
        {
			string title = string.Empty;

			switch (GameManager.Instance.GameType) {

			case Enums.GameScenes.Klondike:

				// TODO: update the title.
				title = "KINGS KLONDIKE";
				break;
			case Enums.GameScenes.Spider:

				// TODO: update the title.
				title= "KINGS SPIDER";
				break;
			case Enums.GameScenes.Tripeaks:

				// TODO: update the title.
				title = "KINGS TRIPEAKS";
				break;
			}
            // TODO: Open dialog.
			DialogSystem.Instance.ShowDialogMessage("Would you like to play again?", title, () =>
            {

                // TODO: stoping the game.
                GameManager.Instance.UpdateState(Enums.StateGame.None);

                // TODO: Check if not null.
                if (!object.ReferenceEquals(GamePlay.Instance, null))
                {
                    // TODO: Clear the handles.
                    GamePlay.Instance.StopTimingHandle();
                }

                // TODO: Load the new game.
				LoadingBehaviour.Instance.ShowLoading(Contains.GamePlayScene, true);

                // TODO: Ready to show ads.
                Contains.IsReadyShowAds = true;
            }, null, null);

            // TODO: Turn off hint.
            HintDisplay.Instance.DisableHint();
        }
        else
        {
            // TODO: stoping the game.
            GameManager.Instance.UpdateState(Enums.StateGame.None);

            // TODO: Check if not null.
            if (!object.ReferenceEquals(GamePlay.Instance, null))
            {
                // TODO: Clear the handles.
                GamePlay.Instance.StopTimingHandle();
            }

            // TODO: Load the new game.
            LoadingBehaviour.Instance.ShowLoading(Contains.GamePlayScene, false);

            // TODO: Ready to show ads.
            Contains.IsReadyShowAds = true;
        }
    }

	public void DoSetting()
	{
		// TODO: Playing the sound.
		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: Open dialog.
		DialogSystem.Instance.ShowDialogSettings();

        // TODO: Turn off hint.
        HintDisplay.Instance.DisableHint();
    }

	public void DoUndo()
	{
        // TODO: Break the function if this not ready.
        if (GameManager.Instance.GetStateGame() != Enums.StateGame.Playing)
        {
            return;
        }

        // TODO: Break the function if this not ready.
        if (!UndoSystem.Instance.IsUndoReady)
        {
            return;
        }

        // TODO: Playing the sound.
        SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);


        //point
        bool flag = IAPManager.vip;
#if UNITY_ANDROID
        flag = true;
#endif
        //if (!flag) IAPManager.instance.ShowSubscriptionPanel("UndoClick");
        //else {

            // TODO: Turn off hint.
            HintDisplay.Instance.DisableHint();
            // TODO: Undo.
            UndoSystem.Instance.Undo();
        //}
	}

	public void DoHint()
	{
		// TODO: Playing the sound.
		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

        // TODO: Check if this is showing, return.
        if (HintDisplay.Instance.IsShowing)
            return;

        // TODO: Break the function if this not ready.
        if ( GameManager.Instance.GetStateGame() != Enums.StateGame.Playing )
        {
            return;
        }
        //point
        bool flag = IAPManager.vip;
#if UNITY_ANDROID
        flag = true;
#endif
        //if (!flag) IAPManager.instance.ShowSubscriptionPanel("HintClick");
        //else
            // TODO: Check the hint.
            GamePlay.Instance.IsHintAvailable();


    }

	#endregion
}
