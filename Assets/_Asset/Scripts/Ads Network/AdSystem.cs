using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

/// <summary>
/// Ad system.
/// </summary>
public class AdSystem : Singleton<AdSystem>
{
    // ================================ Action ============================= //
    #region Action
	/// <summary>
	/// The is use admob.
	/// </summary>
    public bool IsUseAdmob = true;

    [HideInInspector]
    public bool IsBannerShowed;

    #endregion

    #region Functional

    /// <summary>
    /// Awake this instance.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        InitAdmobs();

        InitDelegate();
    }		

	#endregion

    #region Admobs

    // =========================== References ======================== //

	/// <summary>
	/// The banner.
	/// </summary>
    BannerView banner;

	/// <summary>
	/// The interstitial ad.
	/// </summary>
    InterstitialAd interstitialAd;

	/// <summary>
	/// The banner android ad unit I.
	/// </summary>
    [Header("Admobs")]
    public string BannerAndroidAdUnitID = "INSERT_ANDROID_BANNER_AD_UNIT_ID_HERE";

	/// <summary>
	/// The banner IOS ad unit I.
	/// </summary>
    public string BannerIOSAdUnitID = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";

	/// /// <summary>
	/// The interstitial android ad unity I.
	/// </summary>
    public string InterstitialAndroidAdUnityID = "INSERT_ANDROID_INTERSTITIAL_AD_UNIT_ID_HERE";

	/// <summary>
	/// The interstitial IOS ad unity I.
	/// </summary>
    public string InterstitialIOSAdUnityID = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";

    // ========================== Init Admob =========================== //

	/// /// <summary>
	/// Inits the admobs.
	/// </summary>
    void InitAdmobs()
    {
        RequestBanner();

        RequestInterstitial();
    }

    void InitDelegate()
    {
        if ( banner != null )
        {
            banner.OnAdClosed += Banner_OnAdClosed;
            banner.OnAdLoaded += Banner_OnAdLoaded;
        }
    }

    private void Banner_OnAdLoaded(object sender, System.EventArgs e)
    {
        IsBannerShowed = true;
    }

    private void Banner_OnAdClosed(object sender, System.EventArgs e)
    {
        IsBannerShowed = false;
    }

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowInterstitialAd()
    {
		if (Contains.IsHavingRemoveAd)
			return;

        if (!IsUseAdmob)
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }

            return;
        }
            
        if ( interstitialAd != null && interstitialAd.IsLoaded() )
        {
            interstitialAd.Show();

            Contains.IsReadyShowAds = false;
        }
        else
        {
            RequestInterstitial();
        }
    }

	/// <summary>
	/// Hides the interstitial ad.
	/// </summary>
    public void HideInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

	/// <summary>
	/// Shows the banner.
	/// </summary>
    public void ShowBanner()
    {
		if (Contains.IsHavingRemoveAd)
			return;

        if (!IsUseAdmob)
        {
            if ( banner != null)
            {
                banner.Destroy();
            }

            IsBannerShowed = false;

            return;
        }

        if ( banner != null)
        {
            banner.Show();

           IsBannerShowed = true;
        } else
        {
            RequestBanner();
        }
    }

	/// <summary>
	/// Hides the banner.
	/// </summary>
    public void HideBanner()
    {
        if ( banner != null )
        {
            banner.Hide();
        }

        IsBannerShowed = false;
    }

	/// <summary>
	/// Requests the banner.
	/// </summary>
    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = BannerAndroidAdUnitID;
#elif UNITY_IOS
		string adUnitId = BannerIOSAdUnitID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
		banner = new BannerView(adUnitId, AdSize.Banner , AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        banner.LoadAd(request);
    }

		/// <summary>
		/// Requests the interstitial.
		/// </summary>
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = InterstitialAndroidAdUnityID;
#elif UNITY_IOS
		string adUnitId = InterstitialIOSAdUnityID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitialAd = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitialAd.LoadAd(request);
    }

    #endregion

}
