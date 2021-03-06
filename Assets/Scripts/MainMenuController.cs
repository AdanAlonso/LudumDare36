﻿using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonsGameObject;
	public GameObject creditsGameObject;

	public Animator villainAnim;
	public float waitToPlayTime;

	void Awake () {
		Time.timeScale = 0;
	}

	public void play () {
		StartCoroutine (playCoroutine());
	}

	IEnumerator playCoroutine() {
		GetComponent<CanvasGroup> ().alpha = 0f;
		float timer = 0f;
		while (timer < waitToPlayTime) {
			timer += Time.fixedDeltaTime;
			yield return 0;
		}
		villainAnim.SetTrigger ("Start");
		Time.timeScale = 1;
		AudioManager.instance.mixer.SetFloat("MelodyVol", 0);
		gameObject.SetActive (false);
	}

	public void credits () {
		creditsGameObject.SetActive (true);
		buttonsGameObject.SetActive (false);
	}

	public void quit () {
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void back () {
		buttonsGameObject.SetActive (true);
		creditsGameObject.SetActive (false);
	}
}
