using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOnEnable : MonoBehaviour {

	[Header ("PROPERTIES")]
	[SerializeField] private Transform[] transforms;

	void OnEnable()
	{
		for (int i = 0; i < transforms.Length; i++) {

			if (transforms [i].gameObject.activeSelf == true) {

				transforms [i].gameObject.SetActive (false);
			}
		}
        //point backgrounds
        transforms[0].gameObject.SetActive(true);
        //transforms [Random.Range (0, transforms.Length)].gameObject.SetActive (true);
	}
}