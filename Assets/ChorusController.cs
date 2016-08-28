using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class ChorusController : MonoBehaviour {

	public Transform chorusLeft;
	public Transform chorusRight;
	public Transform player;
	public AudioMixer mixer;

	public float ratio;
	public float volume;

	void Start () {
		mixer.SetFloat("ChorusVol", -80);
	}

	void Update () {
		if (chorusLeft.position.x > player.position.x) {
			ratio = -80 / 0.01f * ((chorusLeft.position.x / chorusRight.position.x) + chorusLeft.position.x);
			volume = ratio * player.position.x / chorusLeft.position.x * 0.01f;
			mixer.SetFloat("ChorusVol", Mathf.Lerp(-80, 0, volume));
		}
	}
}
