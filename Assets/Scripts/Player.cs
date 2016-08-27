using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public delegate void OnPlatform(bool OnPlatform);
	public static event OnPlatform onOnPlatform;

	[System.Serializable]
	public enum States {
		Running,
		Jumping,
		Falling,
		Dead
	}
	public States state = States.Running;

	public KeyCode jumpKey;

	Rigidbody2D rb;
	public Animator a;

	[Range(0, 20)]
	public float runVelocity;
	[Range(0, 30)]
	public float jumpVelocity;
	[Range(0, 5)]
	public int maxJumps;

	int jumps = 0;
	bool grounded = false;
	bool wallOnSide = false;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine (FSM());
	}

	void OnCollisionEnter2D(Collision2D coll) {
		foreach (ContactPoint2D contact in coll.contacts) {
			if (Mathf.Abs(contact.normal.x) > 0.5f) {
				wallOnSide = true;
				break;
			}
			if (contact.point.y < transform.position.y) {
				grounded = true;
				if (coll.gameObject.CompareTag("Platform") && onOnPlatform != null)
					onOnPlatform (true);
				a.SetBool ("Grounded", grounded);
				jumps = 0;
				break;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		foreach (ContactPoint2D contact in coll.contacts) {
			if (Mathf.Abs(contact.normal.x) > 0.5f) {
				wallOnSide = false;
				break;
			}
			if (contact.point.y < transform.position.y) {
				grounded = false;
				if (coll.gameObject.CompareTag("Platform") && onOnPlatform != null)
					onOnPlatform (false);
				a.SetBool ("Grounded", grounded);
				++jumps;
				break;
			}
		}
	}

	IEnumerator FSM() {
		ChangeState (state);
		while (true) {
			yield return StartCoroutine(state.ToString());
		}
	}

	void ChangeState(States newState) {
		state = newState;
	}

	public void kill() {
		ChangeState (States.Dead);
	}

	IEnumerator Running() {
		while (state == States.Running) {
			moveForward ();

			if (Input.GetKeyDown (jumpKey))
				ChangeState (States.Jumping);
			yield return 0;
		}
	}

	IEnumerator Jumping() {
		if (jumps < maxJumps) {
			rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
			if (!grounded)
				++jumps;
		}
		ChangeState (States.Falling);
		yield return 0;
	}

	IEnumerator Falling() {
		while (state == States.Falling) {
			moveForward ();

			if (grounded)
				ChangeState(States.Running);
			if (Input.GetKeyDown (jumpKey))
				ChangeState (States.Jumping);
			yield return 0;
		}
	}

	IEnumerator Dead() {
		while (state == States.Dead) {
			yield return 0;
		}
	}

	void moveForward() {
		if (wallOnSide)
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y);
		else
			rb.velocity = new Vector2 (runVelocity, rb.velocity.y);
	}

}
