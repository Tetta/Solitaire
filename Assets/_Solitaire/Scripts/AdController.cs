using System;
using System.Collections;
//using AppodealAds.Unity.Api;
//using AppodealAds.Unity.Common;
using UnityEngine;
using System.Collections.Generic;
[DisallowMultipleComponent]
public class AdController : MonoBehaviour {
    public static AdController instance = null;

    public readonly string IOS_APP_KEY = "9f5ad33d";
    public readonly string ANDROID_APP_KEY = "51960c416717ce5e3d99a3404aabbf5b7a1beb8bdd42ddcd";



    public static Action giveReward;
    public event Action OnInterstitialWatched;

    public static bool IsVideoReady
    {
        get
        {
#if UNITY_EDITOR
            return true;
#endif
            return true;
            //return Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);
        }
    }
    public static bool IsInterstitialReady { get {
            if (!IronSource.Agent.isInterstitialReady()) IronSource.Agent.loadInterstitial();
                return IronSource.Agent.isInterstitialReady(); } }
    private float timer;
    public static bool timerTicked = true;
    public readonly float interstitialInterval = 20f;
    //private AudioManager audioManager;

    #region Interstitial event handlers

    public void onInterstitialFailedToLoad() { }
    public void onInterstitialClicked() { }
    public void onInterstitialExpired() { }
    public void onInterstitialShown() {
        AnalyticsController.sendEvent("InterstitialShown");
    }
    public void onInterstitialLoaded(bool isPrecache) { }
    public void onInterstitialClosed() {
        //StartCoroutine(InterstitialClosedCoroutine());
        //Pause(false);
        //point
        //StartCoroutine(UpdateTimer());
        timer = 0f;
        timerTicked = false;
        AnalyticsController.sendEvent("InterstitialClosed");

    }

    #endregion

    #region Rewarded event handlers

    public void onRewardedVideoExpired() { }
    public void onRewardedVideoFailedToLoad() { }
    public void onRewardedVideoFinished(double amount, string name) { }
    public void onRewardedVideoShown() { }
    public void onRewardedVideoLoaded(bool precache) { }
    public void onRewardedVideoClosed(bool finished) {
        //StartCoroutine(RewardedVideoClosedCoroutine(finished));
        RewardedVideoClosed(finished);
    }

    #endregion

    public static void ShowInterstitial() {
        Debug.Log("ShowInterstitial");
        Debug.Log("IAPManager.vip: " + IAPManager.vip);
        if (IsInterstitialReady && !IAPManager.vip/* && timerTicked*/) {
            Debug.Log("ShowInterstitial 2");
            //Pause(true);
            AnalyticsController.sendEvent("InterstitialShow");

           //Appodeal.show(Appodeal.INTERSTITIAL);

        }
    }

    public static void ShowRewarded() {
#if UNITY_EDITOR
        giveReward?.Invoke();
        //giveReward = null; 
#endif
        if (IsVideoReady) {
          //  Pause(true);
            //Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

    private void Start() {
        if (instance == null) {
            instance = this;

            DontDestroyOnLoad(gameObject);

            var appKey = "";
#if UNITY_IOS
        appKey = IOS_APP_KEY;
#elif UNITY_ANDROID
            appKey = ANDROID_APP_KEY;
#endif
            IronSource.Agent.validateIntegration();
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
            IronSource.Agent.loadInterstitial();
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        }
        else  {
            Destroy(gameObject);
        }

    }

    private IEnumerator InterstitialClosedCoroutine() {
        yield return new WaitForSecondsRealtime(0.1f);
        //Pause(false);
        OnInterstitialWatched?.Invoke();
    }
    private void RewardedVideoClosed(bool finished) {
        //Pause(false);
        if (finished) {
            Debug.Log("OnRewardedVideoClosed and finished=true");
            giveReward?.Invoke();
        }
        else {
            Debug.Log("OnRewardedVideoClosed and finished=false");
        }
    }

    private IEnumerator RewardedVideoClosedCoroutine(bool finished) {
        yield return new WaitForSecondsRealtime(0.1f);
        //Pause(false);
        if (finished) {
            Debug.Log("OnRewardedVideoClosed and finished=true");
            //giveReward?.Invoke();
        }
        else {
            Debug.Log("OnRewardedVideoClosed and finished=false");
        }
    }

    void OnApplicationPause(bool isPaused) {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }


    //Banner Events
    void BannerAdLoadedEvent ()
    {
    	Debug.Log ("unity-script: I got BannerAdLoadedEvent");
        showBanner();
    }

    public static void showBanner() {
        if (!IAPManager.vip)
            IronSource.Agent.displayBanner();
    }
    public static void hideBanner () {
        IronSource.Agent.hideBanner();
    }
}
