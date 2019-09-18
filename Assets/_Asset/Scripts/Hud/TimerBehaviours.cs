using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class TimerBehaviours : MonoBehaviour {
	
	/// <summary>
	/// The current time.
	/// </summary>
	protected float _currentTime = 0;

	// TODO: Cache the time count.
	protected bool IsCountTime = false;

	// TODO: Handle the time.
	public static  CoroutineHandle handleCountTiming;

	IEnumerator<float> InitTiming()
	{
		while (IsCountTime) {		

			// TODO: Calculate time.
			_currentTime = Mathf.Clamp (_currentTime + 1, 0, float.MaxValue);

			// TODO: Check if this null.
			if (object.ReferenceEquals (UIBehaviours.Instance, null)) {

				// TODO: break the function.
				yield break;
			}

			Contains.Time = _currentTime;

            // TODO: Update the time on hud.
            //Debug.Log("Timer UIBehaviours.Instance.UpdateTime (_currentTime);");
			UIBehaviours.Instance.UpdateTime (_currentTime);

			// TODO: Waiting the corountine.
			yield return Timing.WaitForSeconds (1f);
		}
	}

	public void Resume()
	{
		// TODO: Set the state count the time.
		IsCountTime = true;

		// TODO: Check the condition null.
		if (!object.ReferenceEquals (handleCountTiming, null)) {

			// TODO: Kill the old process.
			Timing.KillCoroutines (handleCountTiming);
		}

		// TODO: Call the invoke.
		handleCountTiming = Timing.RunCoroutine (InitTiming(), Enums._GameScene[(int)GameManager.Instance.GameType]);
	}

	public void Pause()
	{
		// TODO: Set the state count time.
		IsCountTime = false;

		// TODO: Check the condition null.
		if (!object.ReferenceEquals (handleCountTiming, null)) {

			// TODO: Kill the old process.
			Timing.KillCoroutines (handleCountTiming);
		}
	}

}
