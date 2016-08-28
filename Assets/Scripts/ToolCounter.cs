using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolCounter : MonoBehaviour {

	public int counter;

	public Text text;

	public AudioClip getSfx;

	void Start() {
		counter = 0;
	}

	void OnEnable() {
		Collectable.onCollection += OnCollection;
	}

	void OnDisable() {
		Collectable.onCollection -= OnCollection;
	}

	void OnCollection() {
		AudioManager.instance.playSfx (getSfx);
		++counter;
		text.text = counter.ToString();
	}

}
