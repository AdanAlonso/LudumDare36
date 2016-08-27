using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonsGameObject;
	public GameObject creditsGameObject;


	void Awake () {
		Time.timeScale = 0;
	}

	public void play () {
		Time.timeScale = 1;
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
