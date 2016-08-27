using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.GetComponent<Player> ().kill ();
		}
	}
}
