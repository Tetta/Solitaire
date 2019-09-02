using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundHelper : MonoBehaviour {

	AudioSource soundPlayer;

	void OnEnable()
	{
		// TODO: update the status of sound.
		UpdateStatusSound ();
	}

	public void Init(Enums.SoundIndex soundIndex ,bool IsLoop = false, float volume = 0.5f)
	{
		if (object.ReferenceEquals (soundPlayer, null)) {

			soundPlayer = gameObject.AddComponent < AudioSource > ();
		}

		// TODO: disable play.
		soundPlayer.playOnAwake = false;

		// TODO: check loop.
		soundPlayer.loop = IsLoop;

		// TODO: update the volume
		soundPlayer.volume = volume;

		// TODO: don't play the sound.
		soundPlayer.Stop ();

		if (object.ReferenceEquals (SoundSystems.Instance, null)) {

			return;
		}

		var audioGet = SoundSystems.Instance.TryGetAudioClip (soundIndex);

		if (object.ReferenceEquals (audioGet, null)) {

			return;
		}

		soundPlayer.clip = audioGet;
	}

	public void PlaySound()
	{
		if (!object.ReferenceEquals (soundPlayer, null) && (!object.ReferenceEquals (soundPlayer.clip, null))) {

			// TODO: Check condition play.
			if (!soundPlayer.isPlaying) {

				// TODO: play sound.
				soundPlayer.Play ();
			}
		}
	}

	public void StopSound()
	{
		if (!object.ReferenceEquals (soundPlayer, null)) {

			// TODO: Stop the sound.
			soundPlayer.Stop ();
		}
	}

	public void UpdateStatusSound()
	{
		// TODO: Check condition mute.
		if (!object.ReferenceEquals ( soundPlayer , null) ){

			// TODO: Set the state of sound.
			soundPlayer.mute = !Contains.IsSoundOn;
		}
	}

	public void UpdatePitch(float value = 1, bool IsUseAnimation = false, float time = Contains.DurationFade, System.Action OnUpdateCompleted = null)
	{
		if (!object.ReferenceEquals (soundPlayer, null)) {

			soundPlayer.DOKill ();

			if (IsUseAnimation) {

				soundPlayer.DOPitch (value, time).OnComplete ( ()=>{

					if (!object.ReferenceEquals ( OnUpdateCompleted , null ))
					{
						OnUpdateCompleted();
					}

				});
			} else {

				// TODO: Stop the sound.
				soundPlayer.pitch = value;
			}
		}
	}

	public float GetPitch()
	{
		if (!object.ReferenceEquals (soundPlayer, null)) {

			return	soundPlayer.pitch;
		}

		return 1f;
	}
}