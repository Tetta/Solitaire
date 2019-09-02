using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dialog settings.
/// </summary>
public class DialogMessage : DialogInterface {

	[Header("UI")]

	/// <summary>
	/// The user interface title.
	/// </summary>
	[SerializeField] private Text UITitle;

	/// <summary>
	/// The user interface message.
	/// </summary>
	[SerializeField] private Text UIMessage;

	/// <summary>
	/// The user interface yes.
	/// </summary>
	[SerializeField] private Text UIYes;

	/// <summary>
	/// The user interface no.
	/// </summary>
	[SerializeField] private Text UINo;

	#region Action

	/// <summary>
	/// The on yes.
	/// </summary>
	protected System.Action OnYes;

	/// <summary>
	/// The on no.
	/// </summary>
	protected System.Action OnNo;

	/// <summary>
	/// The on close.
	/// </summary>
	protected System.Action OnClose;

	#endregion

	public override void Show ()
	{
		base.Show ();	
	}

	public void Init(string message, string title, string yes, string no, System.Action onYes, System.Action onNo, System.Action onClose)
	{
		// TODO: set the title.
		UITitle.text = title;

		// TODO: set the message.
		UIMessage.text = message;

		// TODO: Set the yes title.
		UIYes.text = yes;

		// TODO: Set the no title.
		UINo.text = no;

		// TODO: Set the action.
		this.OnYes = onYes;

		// TODO: Set the action.
		this.OnNo = onNo;

		// TODO: Set the action.
		this.OnClose = onClose;
	}

	public void DoYes()
	{
		// TODO: close this dialog with action.
		Close (OnYes);
	}

	public void DoNo()
	{

		// TODO: close this dialog with action.
		Close (OnNo);
	}

	public void DoClose()
	{

		// TODO: close this dialog with action.
		Close (OnClose);
	}
}
