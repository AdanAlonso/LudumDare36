using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public AudioMixer mixer;

	public AudioSource bgmSrc;
	public AudioSource sfxSrc;

	void Start() {
		if (instance == null)
			instance = this;
		if (this != instance)
			Destroy (gameObject);
	}

	public void playSfx(AudioClip sfx) {
		if (sfx == null || Time.timeScale == 0)
			return;
		sfxSrc.PlayOneShot (sfx);
	}
}
