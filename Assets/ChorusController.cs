﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class ChorusController : MonoBehaviour {

	public Transform chorusThreshold;
	public Transform player;
	public AudioMixer mixer;

	public float ratio;
	public float volume;

	void Start () {
		mixer.SetFloat("ChorusVol", -80);
	}

	void Update () {
		if (player.GetComponent<Player> ().state != Player.States.Dead) {
			mixer.SetFloat ("ChorusVol", chorusThreshold.position.x > player.position.x ? 0 : -80);
		}
	}
}
