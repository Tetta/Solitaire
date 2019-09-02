using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnEnable : MonoBehaviour {

	[Header("REFRENCES")]
	[SerializeField]
	private Transform transformRotate;

	[SerializeField] private float rotateOffset = 0.01f;

	// TODO: Get the euler angles
	Vector3 rotate = Contains.Vector3Zero;

	private void FixedUpdate()
	{
		// TODO: Add the offset.
		rotate.z += rotateOffset;

		// TODO: Set the value of rotate.
		transformRotate.eulerAngles = rotate;        
	}
}
