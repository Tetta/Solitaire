using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviours : MonoBehaviour {

    [Header("REFRENCES")]
    [SerializeField]
    private Transform planet;

    [SerializeField] private float rotateOffset = 0.01f;

    private void FixedUpdate()
    {
        // TODO: Get the euler angles
        Vector3 rotate = planet.eulerAngles;

        // TODO: Add the offset.
        rotate.z += rotateOffset;

        // TODO: Set the value of rotate.
        planet.eulerAngles = rotate;        
    }
}
