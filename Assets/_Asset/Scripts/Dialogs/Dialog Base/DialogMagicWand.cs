using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogMagicWand : DialogInterface {



    protected bool IsReadyToPress;

    private void OnEnable()
    {
        IsReadyToPress = false;

        Invoke("SetBool", 4f);

        //-----
        GamePlay.magicWandDialogShown = true;
    }

    private void SetBool()
    {
        IsReadyToPress = true;
    }

    public void MagicWandClick()
    {
        AdController.giveReward = () => {
            Debug.Log("giveReward MagicWand");

            GamePlay.Instance.magicWand();
            Close();
        };
        AdController.ShowRewarded();


    }

    public void NewGameClick()
    {
        UIBehaviours.Instance.DoNewGame();
        Close();
    }
}
