using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	public float moveVelocity;

	Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		rb.velocity = new Vector2 (moveVelocity, rb.velocity.y);
	}
}
