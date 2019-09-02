using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEnable : MonoBehaviour {

	[Header ("OPTIONS")]
	[SerializeField] private Behaviour[] componentsDisable;

	IEnumerator Start()
	{
		yield return null;

		for (int i = 0; i < componentsDisable.Length; i++) {

			// TODO: Disable the componets.
			componentsDisable [i].enabled = false;
		}
	}
}
