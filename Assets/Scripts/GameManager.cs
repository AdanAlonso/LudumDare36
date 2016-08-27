using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	void OnEnable() {
		PlayerKiller.onPlayerDeath += onPlayerDeath;
	}

	void OnDisable() {
		PlayerKiller.onPlayerDeath -= onPlayerDeath;
	}

	void onPlayerDeath() {
		StartCoroutine (resetLevel());
	}

	IEnumerator resetLevel() {
		GetComponent<Fade> ().FadeOut ();
		yield return new WaitForSeconds (GetComponent<Fade> ().fadeTime);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
