using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	public delegate void Collection();
	public static event Collection onCollection;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.CompareTag("Player") && onCollection != null) {
			onCollection ();
			Destroy (gameObject);
		}
	}
}
