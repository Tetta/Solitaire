using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWining : DialogInterface {

    protected bool IsReadyToPress;

    private void OnEnable()
    {
        IsReadyToPress = false;

        Invoke("SetBool", 4f);
    }

    private void SetBool()
    {
        IsReadyToPress = true;
    }

    public void Restart()
    {
        if (!IsReadyToPress)
        {
            return;
        }

        Close();

        UIBehaviours.Instance.DoNewGame(false);
    }
}
