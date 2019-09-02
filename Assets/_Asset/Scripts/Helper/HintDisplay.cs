using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using DG.Tweening;

public class HintDisplay : Singleton < HintDisplay > {

	/// <summary>
	/// The holder hint cards.
	/// </summary>
	public Transform holder;

	/// <summary>
	/// The cards.
	/// </summary>
	public Image Cards;

	/// <summary>
	/// The is break.
	/// </summary>
	protected bool IsBreak;

    /// <summary>
    /// The condition to show the hint.
    /// </summary>
    public bool IsShowing;

	public void ShowHint(Vector3 startPosition, Vector3 endPosition , Sprite image)
	{
        // TODO: Break the loop.
		IsBreak = true;

        // TODO: Killing the coroutines.
		Timing.KillCoroutines (Enums.Tags.GameHint.ToString());

        // TODO: Add the image.
		Cards.sprite = image;

        // TODO: Runing the animation.
		Timing.RunCoroutine (StartHint(startPosition, endPosition ) , Enums.Tags.GameHint.ToString());
	}

	public void DisableHint()
	{
        // TODO: Check if this was break.
        if (IsBreak)
            return;

        // TODO: break the loop.
		IsBreak = true;

        // TODO: Killing the coroutines with tags.
	    Timing.KillCoroutines (Enums.Tags.GameHint.ToString());

        // TODO: Disable the hint gameobject.
		holder.gameObject.SetActive (false);

        // TODO: Disalbe show.
        IsShowing = false;

    }

	IEnumerator < float > StartHint(Vector3 startPosition, Vector3 endPosition)
	{
		holder.gameObject.SetActive (true);

		bool IsCompletedMoving = true;

		IsBreak = false;

        IsShowing = true;

        bool IsCompletedFirstTime = false;

		while (!IsBreak) {
			if (IsCompletedMoving == false)
				yield return 0f;
			else {

				if (IsCompletedFirstTime) {

					holder.gameObject.SetActive (false);

					yield return Timing.WaitForSeconds (Contains.DurationFade);

					holder.gameObject.SetActive (true);
				}

				Cards.DOComplete (true);

				IsCompletedMoving = false;

				Cards.transform.position = startPosition;

				Cards.color = Color.white;

                Cards.transform.localScale = Vector3.one;

                Cards.transform.DOMove (endPosition, Contains.DurationPreview).OnComplete ( ()=>
					{
						IsCompletedMoving = true;
					}).SetEase(Ease.OutSine);

                Cards.transform.DOScale(1.2f, Contains.DurationPreview / 3).OnComplete ( ()=> {

                    Cards.transform.DOScale(0.9f, Contains.DurationPreview * 2 / 3);
                });

				Cards.DOFade (0.5f, Contains.DurationPreview);

				IsCompletedFirstTime = true;
			}
		}

		yield return 0f;
	}
}
