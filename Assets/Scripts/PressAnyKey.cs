using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PressAnyKey : MonoBehaviour {
	
	void Start () {
		StartCoroutine (pressAnyKey());
	}

	IEnumerator pressAnyKey() {
		yield return new WaitForSeconds (1f);
		while (true) {
			yield return 0;
			if (Input.anyKeyDown)
				break;
		}
		GetComponent<Fade> ().FadeOut ();
		yield return new WaitForSeconds (GetComponent<Fade> ().fadeTime);
		SceneManager.LoadScene (1);
	}
}
