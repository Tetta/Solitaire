using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dialog settings.
/// </summary>
public class DialogSettings : DialogInterface {

	[Header ("STATISTICS")]

	/// <summary>
	/// The user interface best score value.
	/// </summary>
	[SerializeField] private Text UIBestScoreValue;

	/// <summary>
	/// The user interface best move values.
	/// </summary>
	[SerializeField] private Text UIBestMoveValues;

	/// <summary>
	/// The user interface best time values.
	/// </summary>
	[SerializeField] private Text UIBestTimeValues;

	/// <summary>
	/// The user interface total played.
	/// </summary>
	[SerializeField] private Text UITotalPlayed;

	/// <summary>
	/// The user interface total window.
	/// </summary>
	[SerializeField] private Text UITotalWin;

	/// <summary>
	/// The user interface title mode.
	/// </summary>
	[SerializeField] private Text UITitleMode;

    [Header("SETTINGS")]

    /// <summary>
    /// Controller of sound.
    /// </summary>
    [SerializeField] private Button UISound;

    /// <summary>
    /// Text display on button.
    /// </summary>
    [SerializeField] private Text UITextSound;

    /// <summary>
    /// Controller of music.
    /// </summary>
    [SerializeField] private Button UIMusic;

    /// <summary>
    /// Text display on button.
    /// </summary>
    [SerializeField] private Text UITextMusic;

    /// <summary>
    /// Ready to press.
    /// </summary>
    protected bool SoundIsReady = true;

    /// <summary>
    /// Ready to press.
    /// </summary>
    protected bool MusicIsReady = true;

    [Header("FACEBOOK")]

    /// <summary>
    /// Open facebook from the web.
    /// </summary>
    [SerializeField] private string FacebookLink;

    [Header("TWITTER")]

    /// <summary>
    /// Open facebook from the web.
    /// </summary>
    [SerializeField]
    private string TwitterLink;

    [Header("ADS")]
    [SerializeField]
    private Button UIRemoveAds;

    [SerializeField]
    private Button UIRestorePurchase;

    private void OnEnable()
    {
        // TODO: Set the default press.
        SoundIsReady = true;

        // TODO: Set the default press.
        MusicIsReady = true;

        // TODO: Set the dafault toggle.
        DefaultToggle();

        if (IAPManager.vip) UIRemoveAds.gameObject.SetActive(false);
#if UNITY_ANDROID
        UIRemoveAds.gameObject.SetActive(false);
#endif

        if ( Contains.IsHavingRemoveAd )
        {
            UIRemoveAds.interactable = false;
            UIRestorePurchase.interactable = false;
        }
        else
        {
            UIRemoveAds.interactable = true;
            UIRestorePurchase.interactable = true;
        }

#if UNITY_ANDROID
        UIRestorePurchase.interactable = false;
#endif

    }

    private void Start()
    {

        // TODO: Set the event for sound button.
        if  (!object.ReferenceEquals ( UISound , null))
        {
            UISound.onClick.AddListener( SoundAction );
        }

        // TODO: Set the event for music button.
        if (!object.ReferenceEquals (UIMusic, null ))
        {
            UIMusic.onClick.AddListener(MusicAction);
        }
    }

    public override void Show ()
	{
		base.Show ();	

		switch (GameManager.Instance.GetModeGame ()) {
		case Enums.ModeGame.Easy:

			UITitleMode.text = "Easy";

			break;
		case Enums.ModeGame.Medium:

			UITitleMode.text = "Medium";

			break;
		case Enums.ModeGame.Hard:

			UITitleMode.text = "Hard";

			break;
		default:

			UITitleMode.text = "Comming soon...";

			break;
		}

        // TODO: Set the value of best score.
        UIBestScoreValue.text = PlayerData.BestScore.ToString();

        // TODO: Set the value will be displayed on the best move.
		UIBestMoveValues.text = PlayerData.BestMove.ToString ();

        // TODO: Set the value will be displayed on the best move.
        UIBestTimeValues.text = Contains.GetDisplayTime (PlayerData.BestTime);

        // TODO: Set the value will be displayed on the total played.
		UITotalPlayed.text = PlayerData.TotalPlayed.ToString ();

        // TODO: Set the value will be displayed on the total win.
		UITotalWin.text = PlayerData.TotalWin.ToString ();		
	}

    /// <summary>
    /// Action of Sound.
    /// </summary>
    public void SoundAction()
    {
        if (!SoundIsReady)
            return;

         // TOOD: Set the state of sound.
        Contains.IsSoundOn = !Contains.IsSoundOn;

        // TODO: Break the function if this not ready.
        SoundIsReady = false;

        if ( Contains.IsSoundOn )
        {
            UISound.transform.DOLocalMoveX(80, 0.1f).OnComplete ( ()=>
            {
                SoundIsReady = true;

                // TODO: Check the state of sound.
                SoundSystems.Instance.SoundState();

                // TODO: Set the text will be displayed.
                UITextSound.text = Contains.TextON;
            });
        }
        else
        {
            UISound.transform.DOLocalMoveX(-80, 0.1f).OnComplete(() =>
            {
                SoundIsReady = true;

                // TODO: Check the state of sound.
                SoundSystems.Instance.SoundState();

                // TODO: Set the text will be displayed.
                UITextSound.text = Contains.TextOFF;
            });
        }     
    }

    /// <summary>
    /// Action of Music
    /// </summary>
    public void MusicAction()
    {
        if (!MusicIsReady)
            return;

        // TODO: Set the state of music.
        Contains.IsMusicOn = !Contains.IsMusicOn;

        // TODO: Check the state of music.
        SoundSystems.Instance.MusicState();

        // TODO: Break the function if this not ready.
        MusicIsReady = false;

        if (Contains.IsMusicOn)
        {
            UIMusic.transform.DOLocalMoveX(80, 0.1f).OnComplete(() =>
            {
                MusicIsReady = true;

                // TODO: Check the state of sound.
                SoundSystems.Instance.MusicState();

                // TODO: Set the text will be displayed.
                UITextMusic.text = Contains.TextON;
            });
        }
        else
        {
            UIMusic.transform.DOLocalMoveX(-80, 0.1f).OnComplete(() =>
            {
                MusicIsReady = true;

                // TODO: Check the state of sound.
                SoundSystems.Instance.MusicState();

                // TODO: Set the text will be displayed.
                UITextMusic.text = Contains.TextOFF;
            });
        }
    }

    private void DefaultToggle()
    {

        // TODO: Check the reference.
        if (!object.ReferenceEquals(UIMusic, null) && !object.ReferenceEquals(UITextMusic, null))
        {
            // TODO: Check the state of music.
            if (Contains.IsMusicOn)
            {

                // TODO: Set the default position UI;
                UIMusic.transform.DOLocalMoveX(80, 0f);

                // TODO: Set the text will be displayed.
                UITextMusic.text = Contains.TextON;
            }
            else
            {

                // TODO: Set the default position UI;
                UIMusic.transform.DOLocalMoveX(-80, 0f);

                // TODO: Set the text will be displayed.
                UITextMusic.text = Contains.TextOFF;
            }

        }
        else
        {
            // TODO: Write the log.
            LogGame.DebugLog("[SETTINGS] Music button was null.");
        }

        // TODO: Check the reference.
        if (!object.ReferenceEquals(UISound, null) && !object.ReferenceEquals(UITextSound, null))
        {
            // TODO: Check the state of sound.
            if (Contains.IsSoundOn)
            {

                // TODO: Set the default position UI;
                UISound.transform.DOLocalMoveX(80, 0f);

                // TODO: Set the text will be displayed.
                UITextSound.text = Contains.TextON;
            }
            else
            {

                // TODO: Set the default position UI;
                UISound.transform.DOLocalMoveX(-80, 0f);

                // TODO: Set the text will be displayed.
                UITextSound.text = Contains.TextOFF;
            }
        }
        else
        {

            // TODO: Write the log.
            LogGame.DebugLog("[SETTINGS] Sound button was null.");
        }
    }

    #region Social

    public void FacebookFanpage()
    {
            if ( !string.IsNullOrEmpty ( FacebookLink ))
            {
            Application.OpenURL(FacebookLink);              
            }           
    }

    public void TwitterFanpage()
    {
        if (!string.IsNullOrEmpty(TwitterLink))
        {
            Application.OpenURL(TwitterLink);
        }
    }

    public void Feedback()
    {

    }

    public void RemoveAds()
    {
        IAPManager.instance.ShowSubscriptionPanel("RemoveAdsClick");
        /*

            if (!AdSystem.Instance.IsUseAdmob) {
                return;
            }

            if (Contains.IsHavingRemoveAd) {
                return;
            }

            SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

            DialogSystem.Instance.ShowDialogMessage(string.Format("Would you like to remove ads with {0}", IapManager.Instance.ReturnThePrice()), "REMOVE ADS", "OK", "Not really", () => {

                IapManager.Instance.BuyNonConsumable();

            });
        */
    }

    public void RestorePurchase()
    {

        if (Contains.IsHavingRemoveAd)
            return;

#if UNITY_ANDROID
        return;
#else
        SoundSystems.Instance.PlaySound(Enums.SoundIndex.Press);

        Close();

        IapManager.Instance.RestorePurchases();
#endif
    }
    #endregion
}
