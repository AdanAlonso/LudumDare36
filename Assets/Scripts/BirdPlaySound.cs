using UnityEngine;
using System.Collections;

public class BirdPlaySound : MonoBehaviour {

	public AudioClip birdSfx;
	public float waitSfx;

	bool alive;

	void Start() {
		alive = true;
		StartCoroutine (playBirdSfx ());
	}

	void OnEnable() {
		ActiveZone.onPlayerDeath += ActiveZone_onPlayerDeath;
	}

	void OnDisable() {
		ActiveZone.onPlayerDeath -= ActiveZone_onPlayerDeath;
	}

	void ActiveZone_onPlayerDeath ()
	{
		alive = false;
	}

	IEnumerator playBirdSfx() {
		while (alive) {
			yield return new WaitForSeconds (Random.Range(0.2f, waitSfx));
			AudioManager.instance.playSfx (birdSfx);
		}
	}

}
