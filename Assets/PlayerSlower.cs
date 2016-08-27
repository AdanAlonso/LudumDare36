using UnityEngine;
using System.Collections;

public class PlayerSlower : MonoBehaviour {

	public float multiplier;

	public float originalVelocity;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			originalVelocity = other.GetComponent<Player> ().runVelocity;
			other.GetComponent<Player>().runVelocity = originalVelocity * multiplier;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.GetComponent<Player>().runVelocity = originalVelocity;
		}
	}
}
