using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInterface : MonoBehaviour {

    [Header("CONTROLLER")]

	/// <summary>
	/// The controller.
	/// </summary>
    [SerializeField] private Animation controller;

	[Header ("ANIMATION")]

	/// <summary>
	/// The animation open.
	/// </summary>
	[SerializeField] private AnimationClip AnimationOpen;

	/// <summary>
	/// The animation close.
	/// </summary>
	[SerializeField] private AnimationClip AnimationClose;

    [Header("OPTIONS")] [Tooltip ("Enable Escape Button.")]
    [SerializeField] private bool IsTouchEscape = true;

	/// <summary>
	/// The state of the dialog.
	/// </summary>
	protected Enums.DialogState dialogState = Enums.DialogState.Disappear;

	/// <summary>
	/// The on completed close.
	/// </summary>
	protected System.Action OnCompletedClose;

    public virtual void OnTouchEscape()
    {
		if (dialogState == Enums.DialogState.Disappear || IsTouchEscape == false)
			return;

        //TODO: Close some things with key escape.
		Close();
    }

    public virtual void Show()
    {

        if (dialogState == Enums.DialogState.Appear)
			return;
		
		dialogState = Enums.DialogState.Appear;

		gameObject.SetActive (true);

		controller.Play (AnimationOpen.name);
    }

    public virtual void Close(System.Action OnClose = null)
    {
		if (dialogState == Enums.DialogState.Disappear)
			return;
		
		Close ();

		OnCompletedClose = OnClose;
    }

    public virtual void Close()
    {
		if (dialogState == Enums.DialogState.Disappear)
			return;

		dialogState = Enums.DialogState.Disappear;

		controller.Play (AnimationClose.name);

		if ( DialogSystem.Instance.CurrentDialog == this)
		{
			DialogSystem.Instance.CurrentDialog = null;
		}
    }

	public void DisableDialog()
	{
		gameObject.SetActive (false);
	}

	public virtual void OnDisable()
	{
		// TODO: Check if null.
		if (!object.ReferenceEquals (OnCompletedClose, null)) {
		
			// TODO: Call the action.
			OnCompletedClose ();

			// TODO: Return null.
			OnCompletedClose = null;
		}
	}

}
