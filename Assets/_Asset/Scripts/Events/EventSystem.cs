using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event system.
/// </summary>
public class EventSystem : Singleton < EventSystem > {

	#region  Function Game

	/// <summary>
	/// Draws the hint cards.
	/// </summary>
	public void DrawHintCards(){

        Debug.Log("DrawHintCards");
        // TODO: Playing the sound.
		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: Draw the cards.
		HintZone.Instance.DrawCards ();

        // TODO: Turn off hint.
        HintDisplay.Instance.DisableHint();
    }

	#endregion

}
