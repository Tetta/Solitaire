using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSoundsPlay : MonoBehaviour {

	[SerializeField]
	private int _numberOfParticles = 10;

	public Enums.SoundIndex[] soundIndex;

	public ParticleSystem particle;

	// Update is called once per frame
	void Update () {

		var count = particle.particleCount;

		if (count > _numberOfParticles)
		{ 
			//particle has been born
			SoundSystems.Instance.PlaySound (soundIndex[Random.Range (0, soundIndex.Length )]);
		}
		_numberOfParticles = count;
	}
}
