using UnityEngine;
using System.Collections;

public class ActiveZone : MonoBehaviour {
	public delegate void PlayerDeath ();
	public static event PlayerDeath onPlayerDeath;

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.GetComponent<Player> ().kill ();
			if (onPlayerDeath != null)
				onPlayerDeath ();
		}
        Destroy(other.gameObject);
	}
}
