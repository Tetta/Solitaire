using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using MEC;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// Loading behaviour.
/// </summary>
public class LoadingBehaviour : Singleton < LoadingBehaviour > {

	// =============================== References ============================== //


	[Header("Controller")]

	/// <summary>
	/// The canvas group.
	/// </summary>
	[SerializeField]
	private CanvasGroup canvasGroup;

	/// <summary>
	/// The user interface loading information.
	/// </summary>
	[SerializeField]
	private Text UILoadingInformation;

	/// <summary>
	/// The on start loading.
	/// </summary>
    [HideInInspector]
    public System.Action OnStartLoading;

	// =============================== Functional ============================== //
	#region Functional 
	/// <summary>
	/// Shows the loading.
	/// </summary>
	/// <param name="sceneLoad">Scene load.</param>
	/// <param name="isFade">If set to <c>true</c> is fade.</param>
	/// <param name="isUseSplashScreen">If set to <c>true</c> is use splash screen.</param>
	/// <param name="message">Message.</param>
	public void ShowLoading(string sceneLoad , bool isFade = true , bool isUseSplashScreen = true ,string message = "")
	{
		transform.gameObject.SetActive(true);

		UILoadingInformation.text = message;

		canvasGroup.alpha = 0;

		Timing.RunCoroutine  (LoadingTime (sceneLoad , isFade , isUseSplashScreen)) ;
	}

	/// <summary>
	/// Loadings the time.
	/// </summary>
	/// <returns>The time.</returns>
	/// <param name="sceneLoad">Scene load.</param>
	/// <param name="isFade">If set to <c>true</c> is fade.</param>
	/// <param name="isUseSplashScreen">If set to <c>true</c> is use splash screen.</param>
	private IEnumerator < float > LoadingTime(string sceneLoad , bool isFade , bool isUseSplashScreen )
	{
        //fix
        if (!isFade && isUseSplashScreen) transform.gameObject.SetActive(false);
        if (isUseSplashScreen)
        {

            if (isFade)
            {

                while (canvasGroup.alpha < 1)
                {

                    canvasGroup.alpha = Mathf.Clamp(canvasGroup.alpha + Time.deltaTime, 0, 1);

					yield return Timing.WaitForOneFrame;
                }

            }
            else
            {
				yield return Timing.WaitForOneFrame;
            }
        }

        if (OnStartLoading != null)
        {
            OnStartLoading();

            OnStartLoading = null;
        }


        if (isUseSplashScreen)
        {

            AsyncOperation async = SceneManager.LoadSceneAsync(sceneLoad, LoadSceneMode.Single);

			async.allowSceneActivation = false;

			float timeLoading = Time.time;

			while (async.progress < 0.9f)
            {
				yield return Timing.WaitForOneFrame;
            }

			canvasGroup.alpha = 1;	

			if (isFade) {
                //fix
				float distance = Mathf.Clamp (1f - (Time.time - timeLoading), 0f, 1f);					

				yield return Timing.WaitForSeconds (distance);

			}


			async.allowSceneActivation = true;

			yield return Timing.WaitForOneFrame;

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha = Mathf.Clamp(canvasGroup.alpha - Time.deltaTime * 2, 0, 1);

				yield return Timing.WaitForOneFrame;
            }

        }else
        {
            SceneManager.LoadScene(sceneLoad, LoadSceneMode.Single);
        }

		transform.gameObject.SetActive(false);
	}
	#endregion
}