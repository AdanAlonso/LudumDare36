using UnityEngine;
using System.Collections;

public class Chroma : MonoBehaviour {

	public UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration chromaticAberration;

	public float speed;
	public float intensity;
	public bool tension;
	float timer;

	void Start() {
		tension = false;
		timer = 0;
	}

	void Update() {
		if (tension) {
			timer = timer + Time.deltaTime % 1;
			chromaticAberration.chromaticAberration = intensity * Mathf.Sin(timer * speed);
		}
	}

}
