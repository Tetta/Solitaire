using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TScoreDisplay : MonoBehaviour {

	[Header ("PROPERTIES")]
	[SerializeField] private TextMesh[] meshDisplay;

	// TODO: The color display on text.
	[SerializeField] private Color[] colorText;

	//TODO: Show the value.
	public void Show(string value)
	{
		//TODO: Loop to check the condition.
		for (int i = 0; i < meshDisplay.Length; i++) {

			// TODO: Set the value.
			meshDisplay [i].text = value;
		}

		// TODO: Check condition to set the new color.
		if (meshDisplay.Length > 0 && colorText.Length > 0) {

			// TODO: Set the new color to display.
			meshDisplay [0].color = colorText [Random.Range (0, colorText.Length)];
		}
	}
}
