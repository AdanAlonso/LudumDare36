using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public AudioSource bgmSrc;
	public AudioSource sfxSrc;

	void Awake() {
		if (instance == null)
			instance = this;
		if (this != instance)
			Destroy (gameObject);
	}

	public void playSfx(AudioClip sfx) {
		if (sfx == null)
			return;
		sfxSrc.PlayOneShot (sfx);
	}
}
