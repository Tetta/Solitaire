using UnityEngine;

/// <summary>
/// The class have any static global variables.
/// </summary>

public static class Contains
{

	#region Screen Card Size

	/// <summary>
	/// The size of the T card.
	/// </summary>
	public static Vector2Int _TCardSize = new Vector2Int(207, 258);

	/// <summary>
	/// The size of the S card.
	/// </summary>
	public static Vector2Int _SCardSize = new Vector2Int(180, 226);

	/// <summary>
	/// The size of the K card.
	/// </summary>
	public static Vector2Int _KCardSize = new Vector2Int(207, 258);
	#endregion


    /// <summary>
    /// The game play scene.
    /// </summary>
	public static string GamePlayScene = "Play";

    /// <summary>
    /// The distance sort unlocked cards.
    /// </summary>
    public const float DistanceSortUnlockedCards = 0.5f;

    /// <summary>
    /// The distance sort locked cards.
    /// </summary>
    public const float DistanceSortLockedCards = 0.2f;

    /// <summary>
    /// The distance sort review.
    /// </summary>
    public const float DistanceSortReview = 0.1f;

    /// <summary>
    /// The distance sort hint cards.
    /// </summary>
    public const float DistanceSortHintCards = 0.5f;

    /// <summary>
    /// The maximum holder cards.
    /// </summary>
    public const int MaximumHolderCards = 7;

    /// <summary>
    /// The off set width card.
    /// </summary>
    public const float OffSetWidthCard = 1.5f;

    /// <summary>
    /// The off set height card.
    /// </summary>
    public const float OffSetHeightCard = 2f;

    /// <summary>
    /// The string will be displayed.
    /// </summary>
    public static string TextON = "On";

    /// <summary>
    /// The string will be displayed.
    /// </summary>
    public static string TextOFF = "Off";

    #region Animation

    /// <summary>
    /// The duration moving.
    /// </summary>
    public const float DurationMoving = 0.1f;

    /// /// <summary>
    /// The duration draw.
    /// </summary>
    public const float DurationDraw = 0.04f;

    /// <summary>
    /// The duration fade.
    /// </summary>
    public const float DurationFade = 0.4f;

    #endregion

    #region Wait Coroutine

    /// <summary>
    /// The duration preview.
    /// </summary>
    public const float DurationPreview = 1f;

    #endregion

    #region static

    /// <summary>
    /// The number cards will be used in each mode.
    /// </summary>
    public static int TotalCardsAreUsing = 0;

    /// <summary>
    /// The moves.
    /// </summary>
    private static int moves = 0;

    /// <summary>
    /// Gets or sets the moves.
    /// </summary>
    /// <value>The moves.</value>
    public static int Moves
    {
        set
        {
            moves = Mathf.Clamp(value, 0, 99999);
        }

        get
        {
            return moves;
        }
    }
    #endregion
    /// <summary>
    /// The number cards.
    /// </summary>
    public const int NumberCards = 104;

	/// <summary>
	/// The is having remove ad.
	/// </summary>
	public static  bool IsHavingRemoveAd {
		get {

			return PlayerPrefs.GetInt ("IsHaveRemoveAd", 0) == 1;
		 
		}

		set {
			int param = value == true ? 1 : 0;

			PlayerPrefs.SetInt ("IsHaveRemoveAd", param);
		}
	}

	/// <summary>
	/// Gets or sets the type of the get theme.
	/// </summary>
	/// <value>The type of the get theme.</value>
	public static int GetThemeType {
		get { 
			return PlayerPrefs.GetInt ("Theme", 0);
		}

		set { 
			PlayerPrefs.SetInt ("Theme", value);
		}
	}

    #region Score

	/// <summary>
	/// The number of card for each type.
	/// </summary>
	public static int numberCardEachType = 13;

    public static float Time = 0;

    /// <summary>
    /// The score.
    /// </summary>
    public static int Score = 0;

    /// /// <summary>
    /// The score move cards.
    /// </summary>
    public const int ScoreMoveCards = 5;

    /// <summary>
    /// The score result cards.
    /// </summary>
    public const int ScoreResultCards = 10;

	/// <summary>
	/// The score reuslt cards.
	/// </summary>
	public const string _ScoreResultCards = "10";

	/// <summary>
	/// The score result cards.
	/// </summary>
	public const int ScoreResultClear = 100;

	/// <summary>
	/// The score result clear.
	/// </summary>
	public const string _ScoreResultClear = "100";

	/// <summary>
	/// The Vector 3 zero.
	/// </summary>
	public static Vector3 Vector3Zero = new Vector3(0, 0, 0);

	/// <summary>
	/// The vector3 null.
	/// </summary>
	public static Vector3 Vector3Null = new Vector3 (10000, 10000 ,10000 );

	/// <summary>
	/// The vector3 one.
	/// </summary>
	public static Vector3 Vector3One = new Vector3 (1, 1, 1);
	/// <summary>
	/// The number of column in game.
	/// </summary>
	public const int numberColumn = 8;

    /// <summary>
    /// 
    /// </summary>
    public static bool IsReadyShowAds;
    #endregion

    #region Message

    public static string TEXT_NO_MORE_HINT = "No more hint available.";

    public static string TEXT_TITLE_INFORMATION = "Information";

    public static string TEXT_TITLE_WARNING = "Warning";

    public static string TEXT_YES = "Yes";

    public static string TEXT_NO = "No";

    public static string TEXT_NOT_REALLY = "Not Really";

    #endregion

    #region Exceptions Message

    /// <summary>
    /// The null exceptions.
    /// </summary>
    public const string NullExceptions = "This value was null";

	#endregion

	public static string GetDisplayTime(float time)
	{
		// TODO: Create variable hour.
		float hour = ( time - time % 3600 ) / 3600;

		// TODO: Create variable minute.
		float minute = ( time - hour * 3600 - time % 60 ) / 60 ;

		// TODO: Create variable second.
		float second = time - hour * 3600 - minute * 60;

		// TODO: update the value of time.
		return string.Format ("{0}:{1}:{2}" , hour.ToString("00"), minute.ToString("00"), second.ToString("00") );
	}

    /// <summary>
    /// Define the state of sound.
    /// </summary>
    public static bool IsSoundOn
    {
        get
        {
            return PlayerPrefs.GetInt("SoundOn", 1) == 1;
        }

        set
        {
            if ( value == true )
            {
                PlayerPrefs.SetInt("SoundOn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("SoundOn", 0);
            }

        }
    }

    /// <summary>
    /// Define the state of music.
    /// </summary>
    public static bool IsMusicOn
    {
        get
        {
            return PlayerPrefs.GetInt("MusicOn", 1) == 1;
        }

        set
        {
            if (value == true)
            {
                PlayerPrefs.SetInt("MusicOn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("MusicOn", 0);
            }

        }
    }
}



/// <summary>
/// Player data.
/// </summary>
[System.Serializable]
public static class PlayerData
{
	/// <summary>
	/// Player properties.
	/// </summary>
	[System.Serializable]
	public class PlayerProperties
	{
		public int bestScore;
		public int bestMove;
		public int totalPlayed;
		public int totalWin;
		public float bestTime;

		public PlayerProperties()
		{
			bestScore = 0;
			bestMove = 0;
			totalPlayed = 0;
			totalWin = 0;
			bestTime = 0;
		}
	}

	/// <summary>
	/// The player properties.
	/// </summary>
	private static PlayerProperties playerPropertiesEasy;

	/// <summary>
	/// The player properties medium.
	/// </summary>
	private static PlayerProperties playerPropertiesMedium;

	/// <summary>
	/// The player properties hard.
	/// </summary>
	private static PlayerProperties playerPropertiesHard;

	/// <summary>
	/// Gets or sets the best score.
	/// </summary>
	/// <value>The best score.</value>
	public static int BestScore{
		get {

			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				return playerPropertiesEasy.bestScore;
			case Enums.ModeGame.Medium:
				return playerPropertiesMedium.bestScore;
			case Enums.ModeGame.Hard:
				return playerPropertiesHard.bestScore;
			default:
				return 0;
			}
		
		}

		set {
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				

				if (value > playerPropertiesEasy.bestScore) {

					playerPropertiesEasy.bestScore = value;
				}

				break;

			case Enums.ModeGame.Medium:

				if (value > playerPropertiesMedium.bestScore) {

					playerPropertiesMedium.bestScore = value;
				}

				break;

			case Enums.ModeGame.Hard:
				if (value > playerPropertiesHard.bestScore) {

					playerPropertiesHard.bestScore = value;
				}

				break;
			default:
				break;
			}
		}
	}

	/// <summary>
	/// Gets or sets the best move.
	/// </summary>
	/// <value>The best move.</value>
	public static int BestMove{
		get {
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				return playerPropertiesEasy.bestMove;
			case Enums.ModeGame.Medium:
				return playerPropertiesMedium.bestMove;
			case Enums.ModeGame.Hard:
				return playerPropertiesHard.bestMove;
			default:
				return 0;
			} 
		}
		set { 
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:


				if (value < playerPropertiesEasy.bestMove ||  playerPropertiesEasy.bestMove == 0) {

					playerPropertiesEasy.bestMove = value;
				}

				break;

			case Enums.ModeGame.Medium:

				if (value < playerPropertiesMedium.bestMove ||  playerPropertiesMedium.bestMove == 0) {

					playerPropertiesMedium.bestMove = value;
				}

				break;

			case Enums.ModeGame.Hard:
				if (value < playerPropertiesHard.bestMove ||  playerPropertiesHard.bestMove == 0) {

					playerPropertiesHard.bestMove = value;
				}

				break;
			default:
				break;
			}
		}
	}

	/// <summary>
	/// Gets or sets the best time.
	/// </summary>
	/// <value>The best time.</value>
	public static float BestTime{
		get {
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				return playerPropertiesEasy.bestTime;
			case Enums.ModeGame.Medium:
				return playerPropertiesMedium.bestTime;
			case Enums.ModeGame.Hard:
				return playerPropertiesHard.bestTime;
			default:
				return 0;
			}  
		}
		set { 
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:


				if (value < playerPropertiesEasy.bestTime ||  playerPropertiesEasy.bestTime == 0) {

					playerPropertiesEasy.bestTime = value;
				}

				break;

			case Enums.ModeGame.Medium:

				if (value < playerPropertiesMedium.bestTime ||  playerPropertiesMedium.bestTime == 0) {

					playerPropertiesMedium.bestTime = value;
				}

				break;

			case Enums.ModeGame.Hard:
				if (value < playerPropertiesHard.bestTime ||  playerPropertiesHard.bestTime == 0) {

					playerPropertiesHard.bestTime = value;
				}

				break;
			default:
				break;
			}
		}
	}

	/// <summary>
	/// Gets or sets the total played.
	/// </summary>
	/// <value>The total played.</value>
	public static int TotalPlayed{
		get {
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				return playerPropertiesEasy.totalPlayed;
			case Enums.ModeGame.Medium:
				return playerPropertiesMedium.totalPlayed;
			case Enums.ModeGame.Hard:
				return playerPropertiesHard.totalPlayed;
			default:
				return 0;
			}   
		}
		set { 
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				playerPropertiesEasy.totalPlayed = value;
				break;
			case Enums.ModeGame.Medium:
				playerPropertiesMedium.totalPlayed = value;
				break;
			case Enums.ModeGame.Hard:
				playerPropertiesHard.totalPlayed = value;
				break;
			default:
				break;
			} 
		}
	}

	/// <summary>
	/// Gets or sets the total win.
	/// </summary>
	/// <value>The total win.</value>
	public static int TotalWin{
		get { 
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				return playerPropertiesEasy.totalWin;
			case Enums.ModeGame.Medium:
				return playerPropertiesMedium.totalWin;
			case Enums.ModeGame.Hard:
				return playerPropertiesHard.totalWin;
			default:
				return 0;
			}    
		}
		set { 
			switch (GameManager.Instance.GetModeGame ()) {
			case Enums.ModeGame.Easy:
				playerPropertiesEasy.totalWin = value;
				break;
			case Enums.ModeGame.Medium:
				playerPropertiesMedium.totalWin = value;
				break;
			case Enums.ModeGame.Hard:
				playerPropertiesHard.totalWin = value;
				break;
			default:
				break;
			}
		}
	}

	/// <summary>
	/// Save this instance.
	/// </summary>
	public static void Save()
	{
		// TODO: Parse data to json.
		var stringSaveEasy = JsonUtility.ToJson (playerPropertiesEasy);

		// TODO: Parse data to json.
		var stringSaveMedium = JsonUtility.ToJson (playerPropertiesMedium);

		// TODO: Parse data to json.
		var stringSaveHard = JsonUtility.ToJson (playerPropertiesHard);

		// TODO: Check if this empty.
		if (!string.IsNullOrEmpty (stringSaveEasy)) {
		
			// TODO: Save the data.
			PlayerValueEasy = stringSaveEasy;
		}

		// TODO: Check if this empty.
		if (!string.IsNullOrEmpty (stringSaveMedium)) {

			// TODO: Save the data.
			PlayerValueMedium = stringSaveMedium;
		}

		// TODO: Check if this empty.
		if (!string.IsNullOrEmpty (stringSaveHard)) {

			// TODO: Save the data.
			PlayerValueHard = stringSaveHard;
		}	

		// TODO: Save the playerprefs.
		PlayerPrefs.Save ();

		LogGame.DebugLog ("SAVING...");
	}

	/// <summary>
	/// Load this instance.
	/// </summary>
	public static void Load()
	{
		var stringGetEasy = PlayerValueEasy;

		var stringGetMedium = PlayerValueMedium;

		var stringGetHard = PlayerValueHard;

		// TODO: Check if this empty.
		if (string.IsNullOrEmpty (stringGetEasy)) {

			// TODO: Create the new properties.
			playerPropertiesEasy = new PlayerProperties ();

		} else {
			// TODO: Get the properties.
			playerPropertiesEasy = JsonUtility.FromJson<PlayerProperties> (stringGetEasy);
		}

		// TODO: Check if this empty.
		if (string.IsNullOrEmpty (stringGetMedium)) {

			// TODO: Create the new properties.
			playerPropertiesMedium = new PlayerProperties ();

		} else {
			// TODO: Get the properties.
			playerPropertiesMedium = JsonUtility.FromJson<PlayerProperties> (stringGetMedium);
		}

		// TODO: Check if this empty.
		if (string.IsNullOrEmpty (stringGetHard)) {

			// TODO: Create the new properties.
			playerPropertiesHard = new PlayerProperties ();

		} else {
			// TODO: Get the properties.
			playerPropertiesHard = JsonUtility.FromJson<PlayerProperties> (stringGetHard);
		}

		LogGame.DebugLog ("LOADING...");
	}
		
	private static string PlayerValueEasy{
		get { 
			return PlayerPrefs.GetString (string.Format ("{0}-{1}",Enums._GameScene[(int)GameManager.Instance.GameType], "Player-Datas-Easy"), string.Empty);		
		}
		set { 
			PlayerPrefs.SetString (string.Format ("{0}-{1}",Enums._GameScene[(int)GameManager.Instance.GameType],"Player-Datas-Easy"), value);
		}
	}

	private static string PlayerValueMedium{
		get { 
			return PlayerPrefs.GetString (string.Format ("{0}-{1}",Enums._GameScene[(int)GameManager.Instance.GameType],"Player-Datas-Medium"), string.Empty);		
		}
		set { 
			PlayerPrefs.SetString (string.Format ("{0}-{1}",Enums._GameScene[(int)GameManager.Instance.GameType],"Player-Datas-Medium"), value);
		}
	}

	private static string PlayerValueHard{
		get { 
			return PlayerPrefs.GetString (string.Format ("{0}-{1}",Enums._GameScene[(int)GameManager.Instance.GameType],"Player-Datas-Hard"), string.Empty);		
		}
		set { 
			PlayerPrefs.SetString (string.Format ("{0}-{1}",Enums._GameScene[(int)GameManager.Instance.GameType],"Player-Datas-Hard"), value);
		}
	}
}
