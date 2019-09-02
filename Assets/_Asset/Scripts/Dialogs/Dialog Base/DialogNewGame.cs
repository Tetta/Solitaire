using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogNewGame : DialogInterface {

	[Header ("UI")]

	/// <summary>
	/// The user interface title.
	/// </summary>
	[SerializeField] private Text UITitle;

	public override void Show ()
	{
		base.Show ();	

		// TODO: Check if null.
		if (!object.ReferenceEquals (UITitle, null)) {

			switch (GameManager.Instance.GameType) {
				
			case Enums.GameScenes.Klondike:

				// TODO: update the title.
				UITitle.text = "KINGS KLONDIKE";
				break;
			case Enums.GameScenes.Spider:

				// TODO: update the title.
				UITitle.text = "KINGS SPIDER";
				break;
			case Enums.GameScenes.Tripeaks:

				// TODO: update the title.
				UITitle.text = "KINGS TRIPEAKS";
				break;
			}
		}
	}

	/// <summary>
	/// Raises the touch escape event.
	/// </summary>
	public override void OnTouchEscape (){}

	/// <summary>
	/// Easies the mode.
	/// </summary>
	public void EasyMode()
	{
		// TODO: playing the sound.
		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: close the dialog with actions.
		Close (() => {
			
			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: update the mode of game.
				GameManager.Instance.UpdateModeGame (Enums.ModeGame.Easy);

				// TODO: update the state of game.
				GameManager.Instance.UpdateState (Enums.StateGame.Start);
			}		
		});
	}

	/// <summary>
	/// Mediums the mode.
	/// </summary>
	public void MediumMode()
	{
		// TODO: playing the sound.
		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: close the dialog with actions.
		Close (() => {

			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: update the mode of game.
				GameManager.Instance.UpdateModeGame (Enums.ModeGame.Medium);

				// TODO: update the state of game.
				GameManager.Instance.UpdateState (Enums.StateGame.Start);
			}	
		});
	}

	/// <summary>
	/// Hards the mode.
	/// </summary>
	public void HardMode()
	{
		// TODO: playing the sound.
		SoundSystems.Instance.PlaySound (Enums.SoundIndex.Press);

		// TODO: close the dialog with actions.
		Close (() => {

			// TODO: Check the exists of GameManager.
			if ( !object.ReferenceEquals ( GameManager.Instance , null )) {

				// TODO: update the mode of game.
				GameManager.Instance.UpdateModeGame (Enums.ModeGame.Hard);

				// TODO: update the state of game.
				GameManager.Instance.UpdateState (Enums.StateGame.Start);
			}	
		});
	}

}
