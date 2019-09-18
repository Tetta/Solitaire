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
    public readonly string ANDROID_APP_KEY = "a12b91dd";



    public static Action giveReward;
    public event Action OnInterstitialWatched;

    public static int interstitialCounter;
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
        if (IsInterstitialReady && !IAPManager.vip/* && timerTicked*/ && interstitialCounter > 0) {
            Debug.Log("ShowInterstitial 2");
            //Pause(true);
            AnalyticsController.sendEvent("InterstitialShow");
            IronSource.Agent.showInterstitial();
           //Appodeal.show(Appodeal.INTERSTITIAL);

        }
        interstitialCounter++;
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

            // Add Banner Events
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;		
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent; 
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent; 
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
            Debug.Log("unity-script: IronSource.Agent.init");
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



    public void showBannerTest() {
        showBanner();
    }
    public static void showBanner() {
        //fix
        if (!IAPManager.vip && !IAPManager.instance.subscribeCanvas.activeSelf) {
            Debug.Log("showBanner");

            IronSource.Agent.displayBanner();
        }
        
    }
    public static void hideBanner () {
        IronSource.Agent.hideBanner();
    }

    //Banner Events
    void BannerAdLoadedEvent ()
    {
    	Debug.Log ("unity-script: I got BannerAdLoadedEvent");
        showBanner();
    }

    void BannerAdLoadFailedEvent (IronSourceError error)
    {
    	Debug.Log ("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode () + ", description : " + error.getDescription ());
    }

    void BannerAdClickedEvent ()
    {
    	Debug.Log ("unity-script: I got BannerAdClickedEvent");
    }

    void BannerAdScreenPresentedEvent ()
    {
    	Debug.Log ("unity-script: I got BannerAdScreenPresentedEvent");
    }

    void BannerAdScreenDismissedEvent ()
    {
    	Debug.Log ("unity-script: I got BannerAdScreenDismissedEvent");
    }

    void BannerAdLeftApplicationEvent () { 
    	Debug.Log ("unity-script: I got BannerAdLeftApplicationEvent");
    }
}
