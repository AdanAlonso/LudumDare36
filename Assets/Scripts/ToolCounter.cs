using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolCounter : MonoBehaviour {

	public int counter;

	public Text text;

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
		++counter;
		text.text = counter.ToString();
	}

}
