using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	AudioSource sfxSrc;

	void Awake() {
		if (this != instance)
			return;
		if (instance == null)
			instance = this;
		sfxSrc = gameObject.AddComponent<AudioSource>();
	}

	public void playSfx(AudioClip sfx) {
		if (sfx == null)
			return;
		sfxSrc.PlayOneShot (sfx);
	}
}
