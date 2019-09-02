using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;
public class IAPManager : MonoBehaviour
{
    public static IAPManager instance;
    public static bool vip = false;
    public GameObject subscribeCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
            vip = Convert.ToBoolean(PlayerPrefs.GetInt("VIP", 0));
            PlayerPrefs.SetInt("SESSIONS_COUNT", PlayerPrefs.GetInt("SESSIONS_COUNT", 0) + 1);
            Debug.Log(PlayerPrefs.GetInt("SESSIONS_COUNT", 0));
            //on 2 session
            if (PlayerPrefs.GetInt("SESSIONS_COUNT", 0) >= 2) {

                ShowSubscriptionPanel("Start");
            }
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }



    public void buySubscription( Product product) {


        setVip(1);


        if (AnalyticsController.subscriptionFrom != "") {
            AnalyticsController.sendEvent("SubscriptionBought", new Dictionary<string, object> { { "from", AnalyticsController.subscriptionFrom } });
            AnalyticsController.LogAddedToCartEvent(product.definition.storeSpecificId, product.definition.storeSpecificId, "Subscribe", product.metadata.isoCurrencyCode, (float)product.metadata.localizedPrice);
        }
        HideSubscriptionPanel();
    }

    public static void setVip(int i) {
        Debug.Log("setVip: " + i);
        PlayerPrefs.SetInt("VIP", i);
        vip = Convert.ToBoolean(i);
        //if (vip) GameController.instance.showPreviousScreen();
        //setVipFeatures();

        //if (vip == 0 && PlayerPrefs.GetString("selectedBarName") == "vip")
        //    BarGUI.instance.updateBarPricesButtons();
    }
    //public void purchase

    public void ShowSubscriptionPanel(string from) {
        AnalyticsController.subscriptionFrom = from;
        if(!vip) subscribeCanvas.SetActive(true);
    }
    public void HideSubscriptionPanel() {
        subscribeCanvas.SetActive(false);
        //panel2
        subscribeCanvas.transform.parent.GetChild(1).gameObject.SetActive(false);

    }
    public void onPrivacyClick() {
        Application.OpenURL("https://docs.google.com/document/d/1FkOeftcs8gF9gZYcTyEXGgihmkdCb60x7Z1Q1o8ixbo/edit?usp=sharing");
    }
    public void onTermsClick() {
        Application.OpenURL("https://docs.google.com/document/d/16AMooYyxeEODGMWGF48IScPSj2w3dPtJ7qgowRByYb8/edit?usp=sharing");
    }
}
