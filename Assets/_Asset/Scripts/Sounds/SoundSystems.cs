using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Sound items.
/// </summary>
[System.Serializable]
public struct SoundItems
{
	/// <summary>
	/// The index of the sound.
	/// </summary>
	public Enums.SoundIndex soundIndex;

	/// /// <summary>
	/// The sound.
	/// </summary>
	public AudioClip sound;
}

/// /// <summary>
/// Music items.
/// </summary>
[System.Serializable]
public struct MusicItems
{
	/// /// <summary>
	/// The index of the music.
	/// </summary>
	public Enums.MusicIndex musicIndex;

	/// <summary>
	/// The music.
	/// </summary>
	public AudioClip music;
}

/// <summary>
/// Sound systems.
/// </summary>
public class SoundSystems : Singleton < SoundSystems > {

	// =============================== Audio Player ============================ //

	[Header ("CONTROLLER")]

	[SerializeField] [Tooltip ("If this null, the system will create when running.")]
	/// /// <summary>
	/// The sound player.
	/// </summary>
	private AudioSource SoundPlayer;

	[SerializeField] [Tooltip("If this null, the system will create when running.")]
	/// <summary>
	/// The music player.
	/// </summary>
	private AudioSource MusicPlayer;

	// =============================== References ============================== //

	#region References

	[Header ("REFERENCES")]

	/// <summary>
	/// The sound items.
	/// </summary>
	[SerializeField]
	private SoundItems[] soundItems;

	/// <summary>
	/// The music items.
	/// </summary>
	[SerializeField]
	private MusicItems[] musicItems;

	#endregion


	// ================================ Variables ========================= //

	#region Variables
	/// <summary>
	/// The sound library.
	/// </summary>
	protected Dictionary< int, AudioClip> soundLibrary = new Dictionary<int, AudioClip>();

	/// <summary>
	/// The music library.
	/// </summary>
	protected Dictionary< int, AudioClip> musicLibrary = new Dictionary<int, AudioClip>();

	#endregion

	// =============================== Monobehaviour =========================== //
	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected override void Awake()
	{
		base.Awake();

		if (MusicPlayer == null)
		{
			MusicPlayer = gameObject.AddComponent<AudioSource>();

			MusicPlayer.loop = false;
		}

		if (SoundPlayer == null)
		{
			SoundPlayer = gameObject.AddComponent<AudioSource>();

			SoundPlayer.loop = false;
		}

		InitCache();

		InitSound ();
	}

	// TODO: Check the state of sound.
	void InitSound(){

		if (Contains.IsSoundOn) {

			// TODO: The condition to check sound.
			if (!object.ReferenceEquals (SoundSystems.Instance, null)) {

				// TODO: Update state of sounds.
				SoundSystems.Instance.EnableSound ();

				// TODO: Update state of music.
				SoundSystems.Instance.EnableMusic ();
			}

		}else{

			// TODO: The condition to check sound.
			if (!object.ReferenceEquals (SoundSystems.Instance, null)) {

				// TODO: Update state of sounds.
				SoundSystems.Instance.DisableSound (false);

				// TODO: Update state of music.
				SoundSystems.Instance.DisableMusic (false);
			}
		}
	}

	/// <summary>
	/// Registers the music.
	/// </summary>
	public void RegisterMusic(MusicItems item)
	{
		// TODO: get the index of sound.
		var indexMusic = (int)item.musicIndex;

		// TODO: check the exists.
		if (!musicLibrary.ContainsKey (indexMusic)) {

			// TODO: remove the index.
			musicLibrary.Add (indexMusic , item.music);
		}
	}

	/// <summary>
	/// Registers the sound.
	/// </summary>
	public void RegisterSound(SoundItems item)
	{
		// TODO: get the index of sound.
		var indexSound = (int)item.soundIndex;

		// TODO: check the exists.
		if (!soundLibrary.ContainsKey (indexSound)) {

			// TODO: remove the index.
			soundLibrary.Add (indexSound , item.sound);
		}
	}

	/// <summary>
	/// Removes the music.
	/// </summary>
	public void RemoveMusic(MusicItems item)
	{
		// TODO: Check null.
		if (!object.ReferenceEquals (MusicPlayer, null)) {

			// TODO: Check the current play.
			if ( MusicPlayer.isPlaying && MusicPlayer.clip.name == item.music.name )
			{
				// TODO: Stop the current music is playing.
				StopMusic ();
			}
		} 

		// TODO: get the index of music.
		var indexMusic = (int)item.musicIndex;

		// TODO: check the exists.
		if (musicLibrary.ContainsKey (indexMusic)) {

			// TODO: remove the index.
			musicLibrary.Remove (indexMusic);
		}
	}

	/// <summary>
	/// Removes the sound.
	/// </summary>
	public void RemoveSound(SoundItems item){

		// TODO: get the index of sound.
		var indexSound = (int)item.soundIndex;

		// TODO: check the exists.
		if (soundLibrary.ContainsKey (indexSound)) {

			// TODO: remove the index.
			soundLibrary.Remove (indexSound);
		}
	}

	/// <summary>
	/// Clear all the old library and register the original sound and music.
	/// </summary>
	public void ResetStateLibrary()
	{
		// TODO: Clear the sound library.
		soundLibrary.Clear ();

		// TODO: Clear the music library.
		musicLibrary.Clear();

		// TODO: Register origin sound and music.
		InitCache ();
	}

	public void SoundState()
	{
		if ( Contains.IsSoundOn )
		{
			EnableSound();
		}
		else
		{
			DisableSound();
		}
	}

	public void MusicState()
	{
		if (Contains.IsMusicOn)
		{
			EnableMusic();
		}
		else
		{
			DisableMusic();
		}
	}

	/// <summary>
	/// Inits the cache.
	/// </summary>
	protected void InitCache()
	{
		for (int i = 0; i < soundItems.Length; i++) {

			// TODO: Get the index sound.
			int soundIndex = (int)soundItems [i].soundIndex;

			// TODO: Check the exists.
			if (!soundLibrary.ContainsKey (soundIndex)) {

				// TODO: add the sound to list.
				soundLibrary.Add (soundIndex, soundItems [i].sound);
			}
		}

		for (int i = 0; i < musicItems.Length; i++) {

			// TODO: Get the index of music.
			int musicIndex = (int)musicItems [i].musicIndex;

			// TODO: Check the exists.
			if (!musicLibrary.ContainsKey (musicIndex)) {

				// TODO: add the music to the list.
				musicLibrary.Add (musicIndex, musicItems [i].music);
			}
		}
	}

	public AudioClip TryGetAudioClip(Enums.SoundIndex index)
	{
		// TODO: Create the param return.
		AudioClip audioFound = null;

		// TODO: Get the param.
		soundLibrary.TryGetValue ((int)index, out audioFound);

		// TODO: return.
		return audioFound;
	}

	#region Play 

	/// <summary>
	/// Plaies the sound.
	/// </summary>
	/// <param name="soundIndex">Sound index.</param>
	public void PlaySound(Enums.SoundIndex soundIndex)
	{
		AudioClip audioFound;

		if (soundLibrary.TryGetValue((int)soundIndex, out audioFound))
		{
			SoundPlayer.PlayOneShot(audioFound);
		}
	}

	/// <summary>
	/// Players the music.
	/// </summary>
	/// <param name="musicIndex">Music index.</param>
	/// <param name="IsLoop">If set to <c>true</c> is loop.</param>
	/// <param name="IsReset">if set to <c>true</c> the music player will restart play the sound anyway.</c></param>
	public void PlayerMusic(Enums.MusicIndex musicIndex, bool IsLoop = false, bool IsReset = true)
	{
		AudioClip audioFound;

		MusicPlayer.DOComplete (true);

		musicLibrary.TryGetValue ((int)musicIndex, out audioFound);		

		// TODO: The condition to reset.
		if ( object.ReferenceEquals ( MusicPlayer.clip , null ) || IsReset || !object.ReferenceEquals ( audioFound , null ) && audioFound.name != MusicPlayer.clip.name ) {

			// TODO: animation fade
			MusicPlayer.DOFade (0, Contains.DurationFade).OnComplete (() => {

				if ( !object.ReferenceEquals ( audioFound , null ))
				{
					if (musicLibrary.TryGetValue ((int)musicIndex, out audioFound)) {

						MusicPlayer.clip = audioFound;				

						MusicPlayer.Play ();

						MusicPlayer.loop = IsLoop;
					}
				}

				MusicPlayer.DOFade (0.7f, Contains.DurationFade);

			});
		}
	}

	/// <summary>
	/// Stops the music.
	/// </summary>
	public void StopMusic()
	{
		MusicPlayer.DOKill (true);

		MusicPlayer.DOFade ( 0, Contains.DurationFade).OnComplete ( ()=>{

			MusicPlayer.Stop();

		});
	}

	#endregion

	#region Controller

	/// <summary>
	/// Disables the sound.
	/// </summary>
	public void DisableSound(bool IsUseAnimation = true)
	{
		if (SoundPlayer != null )
		{
			SoundPlayer.DOKill();

			if (IsUseAnimation) {

				SoundPlayer.DOFade (0, Contains.DurationFade).OnComplete (() => {

					SoundPlayer.mute = true;

				});
				;
			} else {

				SoundPlayer.mute = true;

				SoundPlayer.volume = 0;
			}
		}

		// TODO: find all sound helper.
		var soundFinds = GameObject.FindObjectsOfType < SoundHelper > ();

		for (int i = 0; i < soundFinds.Length; i++) {

			// TODO: update the status of sound.
			soundFinds [i].UpdateStatusSound ();
		}
	}

	/// <summary>
	/// Disables the music.
	/// </summary>
	public void DisableMusic(bool IsUseAnimation = true)
	{
		if (MusicPlayer != null)
		{
			MusicPlayer.DOKill();

			if (IsUseAnimation) {

				MusicPlayer.DOFade (0, Contains.DurationFade).OnComplete (() => {

					MusicPlayer.mute = true;

				});
			} else {

				MusicPlayer.mute = true;

				MusicPlayer.volume = 0;
			}
		}
	}

	/// <summary>
	/// Enables the sound.
	/// </summary>
	public void EnableSound(bool IsUseAnimation = true)
	{
		if (SoundPlayer != null)
		{
			SoundPlayer.mute = false;

			SoundPlayer.DOKill();

			if (IsUseAnimation) {

				SoundPlayer.DOFade (1, Contains.DurationFade);

			} else {

				SoundPlayer.volume = 1;
			}
		}

		// TODO: find all sound helper.
		var soundFinds = GameObject.FindObjectsOfType < SoundHelper > ();

		for (int i = 0; i < soundFinds.Length; i++) {

			// TODO: update the status of sound.
			soundFinds [i].UpdateStatusSound ();
		}
	}

	/// <summary>
	/// Enables the music.
	/// </summary>
	public void EnableMusic(bool IsUseAnimation = true)
	{
		if (MusicPlayer != null)
		{
			MusicPlayer.mute = false;

			MusicPlayer.DOKill();

			if (IsUseAnimation) {

				MusicPlayer.DOFade (0.7f, Contains.DurationFade);

			} else {
				MusicPlayer.volume = 0.7f;
			}
		}
	}
	#endregion
}