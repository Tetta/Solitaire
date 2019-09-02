using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReturnToPools : MonoBehaviour {

	[Header ("OPTION")]

	[Tooltip ("The id of item in the pool list.")]
	[SerializeField] private Enums.PoolType poolId;

	[Tooltip ("This will return base on OnDisable Function")]
	[SerializeField] private bool IsOnDisable;

	[Tooltip ("This will return base on Time.")]
	[SerializeField] public bool IsOnTime;

	[HideInInspector]
	// TODO: The duration to return to pools.
	public float duration;

	private bool IsReturned = false;

	void OnEnable()
	{
		IsReturned = false;

		if (IsOnTime) {

			// TODO: Run coroutine disable.
			Timing.RunCoroutine (_DisableBaseOnTime ());
		}
	}

	public void DoReturnToPools()
	{
		if (IsReturned) {
			return;
		}

		IsReturned = true;

		// TODO: Return this item to pool.
		PoolSystem.Instance.ReturnToPool (poolId, gameObject);
	}

	IEnumerator <float > _DisableBaseOnTime()
	{
		// TODO: Waiting to return.
		yield return Timing.WaitForSeconds ( duration );

		if (IsReturned) {
			yield break;
		}

		IsReturned = true;

		// TODO: Return this item to pool.
		PoolSystem.Instance.ReturnToPool (poolId, gameObject);
	}

	void OnDisable()
	{
		if (IsOnDisable) {

			if (IsReturned) {
				return;
			}

			IsReturned = true;

			if (!object.ReferenceEquals (PoolSystem.Instance, null)) {
			
				// TODO: Return this item to pool.
				PoolSystem.Instance.ReturnToPool (poolId, gameObject);
			}
		}
	}
}

#if UNITY_EDITOR

[CustomEditor (typeof (ReturnToPools))]
public class _ReturnToPools : Editor 
{
	// TODO: The object find.
	ReturnToPools poolFind ;

	public override void OnInspectorGUI ()
	{
		if (object.ReferenceEquals (poolFind, null)) {

			// TODO: Get the target.
			poolFind = target as ReturnToPools;
		}

		base.OnInspectorGUI ();

		// TODO: Check the condition.
		if (poolFind.IsOnTime) {
		
			EditorGUILayout.LabelField ("Duration Time:");

			// TODO: Show the field.
			poolFind.duration = EditorGUILayout.FloatField (poolFind.duration);
		}	
	}
}
#endif
