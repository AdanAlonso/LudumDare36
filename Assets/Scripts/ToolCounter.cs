using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolCounter : MonoBehaviour {

	public int counter;
	public string label;

	Text text;

	void Start() {
		text = GetComponent<Text> ();
		counter = 0;
	}

	void OnEnable() {
		Collectable.onCollection += OnCollection;
	}

	void OnDisable() {
		Collectable.onCollection -= OnCollection;
	}

	void OnCollection() {
		++counter;
		text.text = label + counter;
	}

}
