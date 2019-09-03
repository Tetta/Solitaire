using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscriptionCanvas : MonoBehaviour
{
    public Text closeButton1;
    public Image closeButton2;
    public Text closeButton11;
    public Image closeButton22;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _panel2;
    //[SerializeField] private Button _privacyPolicyButton;
	//[SerializeField] private Button _termsButton;
	//[SerializeField] private Button _closeButton;
	//[SerializeField] private RectTransform _closeButtonRect;
	//[SerializeField] private Text _bottomText;

	
	[SerializeField] private float _closeWaitTime = 3f;
	[SerializeField] private float _panelMoveTime = 1f;

	//[Space] [Header("Data")] 
	//[SerializeField] private SubscriptionData _androidData;
	//[SerializeField] private SubscriptionData _iOSData;

	

	private Vector3 _panelStartPosition = Vector3.one;
	private Color _closeButtonStartColor;
	private bool _alreadyShown = false;

    //public static string from = "";
    bool screenVert;
    bool screenVert2;
    private void Awake() {

        screenVert = Screen.height > Screen.width;
    }
    private void Start ()
	{
        updateView();

        StartCoroutine(WaitFrame());
	}
	
	private IEnumerator WaitFrame()
	{
		yield return new WaitForFixedUpdate();
	}
	
	private void OnDisable()
	{
		AdController.showBanner();
	}

	private void OnEnable()
	{
        AdController.hideBanner();

		if (_panelStartPosition == Vector3.one)
		{
			//_panelStartPosition = _panel.transform.position;
			//_closeButtonStartColor = _closeButtonRect.GetComponent<Image>().color;
		}
		else
		{
			//_panel.transform.position = _panelStartPosition;
			//_closeButtonRect.GetComponent<Image>().color = _closeButtonStartColor;
		}

		//LeanTween.moveLocalY(_panel, 0f, _panelMoveTime).setEase(LeanTweenType.easeOutQuint);

        _alreadyShown = false;
        closeButton1.gameObject.SetActive(false);
        closeButton11.gameObject.SetActive(false);
        StartCoroutine(WaitAndPrint());
        AnalyticsController .sendEvent("SubscriptionShown", new Dictionary<string, object> {  { "From", AnalyticsController.subscriptionFrom } });

    }

    public void showPanel2 () {
        _alreadyShown = false;
        closeButton2.gameObject.SetActive(false);
        closeButton22.gameObject.SetActive(false);
        StartCoroutine(WaitAndPrint());

        _panel2.SetActive(true);
        AnalyticsController.sendEvent("SubscriptionShown2", new Dictionary<string, object> { { "From", AnalyticsController.subscriptionFrom } });

    }
    public void hidePanel2() {
        _panel2.SetActive(false);
    }
    private IEnumerator WaitAndPrint()
	{


        if (!_alreadyShown)
			yield return new WaitForSecondsRealtime(_closeWaitTime);
		_alreadyShown = true;
        closeButton1.gameObject.SetActive(true);
        closeButton11.gameObject.SetActive(true);
        closeButton2.gameObject.SetActive(true);
        closeButton22.gameObject.SetActive(true);
        LeanTween.alphaText(closeButton1.GetComponent<RectTransform>(), 0f, 0).setEase(LeanTweenType.linear);
        LeanTween.alphaText(closeButton11.GetComponent<RectTransform>(), 0f, 0).setEase(LeanTweenType.linear);
        LeanTween.alpha(closeButton2.GetComponent<RectTransform>(), 0f, 0).setEase(LeanTweenType.linear);
        LeanTween.alpha(closeButton22.GetComponent<RectTransform>(), 0f, 0).setEase(LeanTweenType.linear);

        LeanTween.alphaText(closeButton1.GetComponent<RectTransform>(), 1f, 0.7f).setEase(LeanTweenType.linear);
        LeanTween.alphaText(closeButton11.GetComponent<RectTransform>(), 1f, 0.7f).setEase(LeanTweenType.linear);
        LeanTween.alpha(closeButton2.GetComponent<RectTransform>(), 1f, 0.7f).setEase(LeanTweenType.linear);
        LeanTween.alpha(closeButton22.GetComponent<RectTransform>(), 1f, 0.7f).setEase(LeanTweenType.linear);
    }

    private void Update() {
        
        screenVert2 = Screen.height > Screen.width;
        if (screenVert != screenVert2) {
            screenVert = screenVert2;
            updateView();
        }
    }

    void updateView () {
        if (screenVert) {
            _panel.transform.Find("Vertical").gameObject.SetActive(true);
            _panel.transform.Find("Horizontal").gameObject.SetActive(false);
            _panel2.transform.Find("Vertical").gameObject.SetActive(true);
            _panel2.transform.Find("Horizontal").gameObject.SetActive(false);

        }
        else {
            _panel.transform.Find("Vertical").gameObject.SetActive(false);
            _panel.transform.Find("Horizontal").gameObject.SetActive(true);
            _panel2.transform.Find("Vertical").gameObject.SetActive(false);
            _panel2.transform.Find("Horizontal").gameObject.SetActive(true);
        }
    }
}

