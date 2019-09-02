using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : Singleton < DialogSystem > {

    // ============================= References ======================== //

#region Refenreces

	/// <summary>
	/// The current dialog.
	/// </summary>
    [HideInInspector]
    public DialogInterface CurrentDialog;

	[Header ("PREFAB")]

	/// <summary>
	/// The prefab dialog new game.
	/// </summary>
	[SerializeField] private DialogNewGame prefabDialogNewGame;

	/// <summary>
	/// The prefab settings.
	/// </summary>
	[SerializeField] private DialogSettings prefabDialogSettings;

	/// <summary>
	/// The prefab dialog message.
	/// </summary>
	[SerializeField] private DialogMessage prefabDialogMessage;
    
    /// <summary>
    /// The prefab of dialog wining.
    /// </summary>
    [SerializeField] private DialogWining prefabDialogWining;
    #endregion


	#region properties

	/// <summary>
	/// The dialog new game.
	/// </summary>
	protected DialogNewGame dialogNewGame;

	/// <summary>
	/// The dialog settings.
	/// </summary>
	protected DialogSettings dialogSettings;

	/// <summary>
	/// The dialog message.
	/// </summary>
	protected DialogMessage dialogMessage;

    /// <summary>
    /// The cache of dialog wining.
    /// </summary>
    protected DialogWining dialogWining;
	#endregion

    // ============================ Properties =========================== //

	/// <summary>
	/// Dos the show.
	/// </summary>
	/// <param name="dialog">Dialog.</param>
    protected void DoShow(DialogInterface dialog)
    {
        Timing.RunCoroutine(ShowDialog(dialog));
    }

	/// <summary>
	/// Shows the dialog.
	/// </summary>
	/// <returns>The dialog.</returns>
	/// <param name="dialog">Dialog.</param>
    IEnumerator < float > ShowDialog(DialogInterface dialog)
    {
        bool IsComplete = false;

		if (!object.ReferenceEquals ( CurrentDialog , null))
        {
            CurrentDialog.Close( ()=>
            {
                IsComplete = true;
            });
        }else
        {
            IsComplete = true;
        }

        while (IsComplete == false)
        {
            yield return Timing.WaitForOneFrame;
        }

        CurrentDialog = dialog;

        CurrentDialog.Show();
    }

	/// <summary>
	/// Instances the dialog.
	/// </summary>
	/// <returns>The dialog.</returns>
	/// <param name="dialog">Dialog.</param>
    protected GameObject InstanceDialog(DialogInterface dialog)
    {
        if ( ReferenceEquals (dialog, null ) )
        {
            LogGame.DebugLog("[Dialog System] Can't find the dialog.");

            return null;
        }

        var param = Instantiate(dialog.gameObject, this.transform) as GameObject;

		param.gameObject.SetActive (false);

        return param;
    }

#region Properties

	/// <summary>
	/// Determines whether this instance is have dialog using.
	/// </summary>
	/// <returns><c>true</c> if this instance is have dialog using; otherwise, <c>false</c>.</returns>
    public bool IsHaveDialogUsing()
    {
        return CurrentDialog != null;
    }

	/// <summary>
	/// Shows the dialog message.
	/// </summary>
	public void ShowDialogMessage(string message , string title = "SPIDER SOLITAIRE", string accept = "YES" , string cancel = "NO" , System.Action OnYES = null , System.Action OnNO = null , System.Action OnClose = null)
	{
		// TODO: Get the dialog.
		var dialog = GetTheDialog < DialogMessage > (prefabDialogMessage, dialogMessage);

		// TODO: Check if this null.
		if (object.ReferenceEquals (dialog, null)) {

			// TODO: Break the functions.
			return;
		}

		// TODO: set the default dialog.
		dialogMessage = dialog;

		// TODO: Set the default value.
		dialogMessage.Init (message, title, accept , cancel , OnYES , OnNO , OnClose);

		// TODO: Show the dialog.
		DoShow ( dialog );
	}

	/// <summary>
	/// Shows the dialog message.
	/// </summary>
	public void ShowDialogMessage(string message , string title , System.Action OnYES , System.Action OnNO , System.Action OnClose )
	{
		ShowDialogMessage (message, title, "YES", "NO", OnYES, OnNO, OnClose);
	}

	/// <summary>
	/// Shows the dialog message.
	/// </summary>
	public void ShowDialogMessage(string message , string title , string accept , string cancel)
	{
		ShowDialogMessage (message, title, accept, cancel, null, null, null);
	}

	/// <summary>
	/// Shows the dialog new game.
	/// </summary>
	public void ShowDialogNewGame()
	{
		// TODO: Get the dialog.
		var dialog = GetTheDialog < DialogNewGame > (prefabDialogNewGame, dialogNewGame);

		// TODO: Check if this null.
		if (object.ReferenceEquals (dialog, null)) {

			// TODO: Break the functions.
			return;
		}

		// TODO: set the default dialog.
		dialogNewGame = dialog;

		// TODO: Show the dialog.
		DoShow ( dialog );
	}

	/// <summary>
	/// Shows the dialog settings.
	/// </summary>
	public void ShowDialogSettings()
	{
		// TODO: Get the dialog.
		var dialog = GetTheDialog < DialogSettings > (prefabDialogSettings, dialogSettings);

		// TODO: Check if this null.
		if (object.ReferenceEquals (dialog, null)) {

			// TODO: Break the functions.
			return;
		}

		// TODO: set the default dialog.
		dialogSettings = dialog;

		// TODO: Show the dialog.
		DoShow ( dialog );;
	}

    /// <summary>
    /// Show the dialog wining.
    /// </summary>
    public void ShowDialogWining()
    {       
        // TODO: Get the dialog.
		var dialog = GetTheDialog < DialogWining > (prefabDialogWining, dialogWining);

		// TODO: Check if this null.
		if (object.ReferenceEquals (dialog, null)) {

			// TODO: Break the functions.
			return;
		}

        // TODO: set the default dialog.
        dialogWining = dialog;

		// TODO: Show the dialog.
		DoShow ( dialog );
    }

	protected T GetTheDialog<T>(T prefabIn, T paramOut) where T : DialogInterface
	{
		// TODO: Check if this null.
		if (object.ReferenceEquals (paramOut, null)) {

			// TODO: Check if this null.
			if (object.ReferenceEquals (prefabIn, null)) {

				// TODO: throw the exceptions.
				throw new UnityException (string.Format ("{0} - {1}", prefabIn.name, Contains.NullExceptions));
			}

			// TODO: Create the dialog from prefab.
			var dialog = Instantiate (prefabIn.gameObject, this.transform);

			if (object.ReferenceEquals (dialog.GetComponent < T > (), null)) {

				// TODO: throw the exceptions.
				throw new UnityException (string.Format ("{0} - {1}", prefabIn.name, Contains.NullExceptions));
			}

			// TODO: Get the script.
			paramOut = dialog.GetComponent < T > ();

			// TODO: Disable gameobject.
			dialog.gameObject.SetActive (false);
		}

		return paramOut;
	}
#endregion

}
