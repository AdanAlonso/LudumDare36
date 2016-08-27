using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject gameOver;
	public Animator gameOverAnimator;

	void OnEnable() {
		ActiveZone.onPlayerDeath += onPlayerDeath;
	}

	void OnDisable() {
		ActiveZone.onPlayerDeath -= onPlayerDeath;
	}

	void onPlayerDeath() {
		gameOver.SetActive (true);
		gameOverAnimator.SetTrigger ("GameOver");
	}

	public void resetLevel() {
		StartCoroutine (resetLevelCorroutine ());
	}

	IEnumerator resetLevelCorroutine() {
		GetComponent<Fade> ().FadeOut ();
		yield return new WaitForSeconds (GetComponent<Fade> ().fadeTime);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

}
