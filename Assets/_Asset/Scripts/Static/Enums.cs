using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enums.
/// </summary>
public static class Enums  {

    public enum Zone
    {
        Play,
        Hint,
		Result,
		None,
    }

	public enum GameScenes
	{
		Klondike = 0 ,
		Spider = 1,
		Tripeaks = 2,
		Menus = 3,
	}

	public static string[] _GameScene = new string[]{
		"KlondikeSolitaire",
		"SpiderSolitaire",
		"TripeaksSolitaire",
		"Menus",
	};

	// ======================== Cards Type ======================== //

	/// <summary>
	/// Card type.
	/// </summary>
	public enum CardType 
	{
		None 	= 0,
		Club 	= 1,
		Heart 	= 2,
		Diamond = 3,
		Spade 	= 4,
	}

	/// <summary>
	/// State card.
	/// </summary>
	public enum StateCard
	{
		None 	  = 0,
		Locking   = 1,
		Moving 	  = 2,
        Complete  = 3,
	}

	/// <summary>
	/// Card board.
	/// </summary>
	public enum CardBoard
	{
		CardHint = 0,
		CardUse  = 1,
	}

	/// <summary>
	/// Card variables.
	/// </summary>
	public enum CardVariables
	{
		One 	= 1,
		Two 	= 2,
		Three 	= 3,
		Four 	= 4,
		Five 	= 5,
		Six 	= 6,
		Seven	= 7,
		Eight	= 8,
		Nine 	= 9,
		Ten		= 10,
		Jack	= 11,
		Queen	= 12,
		King	= 13,
	}

	/// <summary>
	/// The State touches.
	/// </summary>
	public enum StateTouch
	{
		None = 0,
		Touch = 1,
		BeginDrag = 2,
		Drag = 3,
	}

	// ======================== Game State ======================== //

	/// <summary>
	/// State game.
	/// </summary>
	public enum StateGame
	{
		None 	 = 0,
		Start	 = 1,
		Pause 	 = 2,
		Playing  = 3,
		Waiting  = 4,
		GameOver = 5,
		Drawing  = 6,
		Wining   = 7,
	}

	/// <summary>
	/// Mode game.
	/// </summary>
    public enum ModeGame
    {
		None,
        Easy,
		Medium,
        Hard,
    }

	// ======================== Direction ========================= //

	/// <summary>
	/// Direction.
	/// </summary>
	public enum Direction
	{
		Up = 0,
        Right = 1,
        Left = 2,
        Down = 3,
        None = 4,
	}

    // ======================= Sound ========================= //

	/// <summary>
	/// Sound index.
	/// </summary>
    public enum SoundIndex
    {
        None = 0,
		Draw = 1,
		Error = 2,
		Press = 3,
		Correct = 4,
		Completed = 5,
		FireworksExploseI = 6,
		FireowrksExploseII = 7,
		FireowrksExploseIII = 8,
    }


    // ====================== Music ========================== //

	/// <summary>
	/// Music index.
	/// </summary>
    public enum MusicIndex
    {
        None = 0,
		Background_I = 1,
		Background_II = 2,
		Background_III = 3,
		WinMusic = 4,
		LoseMusic = 5,
		StartMusic = 6,
    }

	// ========================= Tags ============================ //

	/// <summary>
	/// Tags.
	/// </summary>
	public enum Tags
	{
		GamePlaying,
        GameHint,
	}

	/// <summary>
	/// Theme type.
	/// </summary>
	public enum ThemeType{
		
		Normal = 0,
	}

    /// <summary>
    /// The id of parent transform cards.
    /// </summary>
    public enum IdTransformCard
    {
        TransformCards_A = 0,
        TransformCards_B = 1,
        TransformCards_C = 2,
        TransformCards_D = 3,
        TransformCards_E = 4,
        TransformCards_G = 5,
        TransformCards_H = 6,
        TransformCards_K = 7,
		TransformCards_J = 9,
		TransformCards_M = 10,
		None             = 8,
		
	}

	public static string[] _IdTransformCard = new string[] {		
		"TransformCards_A",
		"TransformCards_B",
		"TransformCards_C",
		"TransformCards_D",
		"TransformCards_E",
		"TransformCards_G",
		"TransformCards_H",
		"TransformCards_K",
		"None",
		"TransformCards_J",
		"TransformCards_M",	
	};

	public enum DialogState
	{
		Appear,
		Disappear,
	}

	public enum PoolType
	{
		PExploise,
		TColorText,
		Fireworks,
	}
}
