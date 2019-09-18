using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogAutoWin : DialogInterface {



    protected bool IsReadyToPress;

    private void OnEnable()
    {
        IsReadyToPress = false;

        Invoke("SetBool", 4f);

        //-----
        GamePlay.autoWinShown = true;
    }

    private void SetBool()
    {
        IsReadyToPress = true;
    }

    public void autoWinClick()
    {
        GamePlay.Instance.autoWin();
        Close();
    }


}
