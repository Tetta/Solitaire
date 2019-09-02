
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class OMStoreEditor{

	[MenuItem ("OMStore/Tools/Remove")]
	public static void ClearDatas()
	{
		if (EditorUtility.DisplayDialog ("Warning!!!", "Do you want to remove all player's data?", "Yes", "No")) {
			// TODO: Check if this playing.
			if (EditorApplication.isPlaying) {

				// TODO: Stop the editor.
				EditorApplication.isPlaying = false;
			}

			// TODO: Delete all keys of player.
			PlayerPrefs.DeleteAll ();

			// TODO: show the message is completed remove.
			EditorUtility.DisplayDialog ("Information", "The application data has been removed!", "OK");
		}
	}

	[MenuItem ("OMStore/Documents")]
	public static void OpenDocuments()
	{

	}

	[MenuItem ("OMStore/Support/Email")]
	public static void ContactEmail()
	{
		// TODO: show the message is completed remove.
		EditorUtility.DisplayDialog ("Email", "Please send email support via onemanstorelab@gmail.com", "OK");
	}

	[MenuItem ("OMStore/Support/Facebook")]
	public static void ContactFacebook()
	{
		Application.OpenURL ("www.facebook.com/OMSL-1860941414169255");
	}

	[MenuItem ("OMStore/Support/FeedBack")]
	public static void ContactFeedBack()
	{
		Application.OpenURL ("www.facebook.com/OMSL-1860941414169255");
	}


}

#endif
