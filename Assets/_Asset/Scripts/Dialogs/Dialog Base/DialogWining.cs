using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogWining : DialogInterface {

    [Header("STATISTICS")]

    /// <summary>
    /// The user interface best score value.
    /// </summary>
    [SerializeField] private Text UIScoreValue;
    [SerializeField] private Text UIBestScoreValue;

    /// <summary>
    /// The user interface best move values.
    /// </summary>
    [SerializeField] private Text UIMoveValues;
    [SerializeField] private Text UIBestMoveValues;

    /// <summary>
    /// The user interface best time values.
    /// </summary>
    [SerializeField] private Text UITimeValues;
    [SerializeField] private Text UIBestTimeValues;

    protected bool IsReadyToPress;

    private void OnEnable()
    {
        IsReadyToPress = false;

        Invoke("SetBool", 4f);

        //-----

        // TODO: Set the value of best score.
        UIScoreValue.text = Contains.Score.ToString();
        UIBestScoreValue.text = PlayerData.BestScore.ToString();
        UIBestScoreValue.gameObject.SetActive(PlayerData.BestScore >= Contains.Score);
        UIBestScoreValue.transform.parent.Find("NewBest").gameObject.SetActive(PlayerData.BestScore < Contains.Score);

        // TODO: Set the value will be displayed on the best move.
        bool newBestFlag = PlayerData.BestMove >= Contains.Moves || PlayerData.BestMove == 0;
        UIMoveValues.text = Contains.Moves.ToString();
        UIBestMoveValues.text = PlayerData.BestMove.ToString();
        UIBestMoveValues.gameObject.SetActive(!newBestFlag);
        UIBestMoveValues.transform.parent.Find("NewBest").gameObject.SetActive(newBestFlag);

        // TODO: Set the value will be displayed on the best move.
        newBestFlag = PlayerData.BestTime >= Contains.Time || PlayerData.BestTime == 0;
        UITimeValues.text = Contains.GetDisplayTime(Contains.Time);
        UIBestTimeValues.text = Contains.GetDisplayTime(PlayerData.BestTime);
        UIBestTimeValues.gameObject.SetActive(!newBestFlag);
        UIBestTimeValues.transform.parent.Find("NewBest").gameObject.SetActive(newBestFlag);

        // TODO: Set the total played.
        PlayerData.TotalWin++;

        // TODO: Save the datas.
        PlayerData.Save();
        AnalyticsController.sendEvent("WinGame", new Dictionary<string, object> { { "Type", GameManager.Instance.GameType }, { "Mode", GameManager.Instance.GetModeGame() } });

    }

    private void SetBool()
    {
        IsReadyToPress = true;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        GamePlay.autoWinShown = false;
        GamePlay.magicWandDialogShown = false;
        AnalyticsController.sendEvent("StartGame", new Dictionary<string, object> { { "Type", GameManager.Instance.GameType }, { "Mode", GameManager.Instance.GetModeGame() } });

        if (!IsReadyToPress)
        {
            return;
        }

        Close();

        UIBehaviours.Instance.DoNewGame(false);
    }
    public void changeModeClick() {
        DialogSystem.Instance.ShowDialogNewGame();
        Close();
    }
    //public override void Show() {
    //base.Show();





    //}
}
