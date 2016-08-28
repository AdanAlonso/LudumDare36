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

	public AudioClip jumpSfx;
	public AudioClip runSfx;
	public AudioClip runSandSfx;
	public float runSfxWait;
	public float runSandSfxWait;
	public AudioClip dieSfx;
	public AudioClip sandColisionSfx;

	[Range(0, 20)]
	public float runVelocity;
	[Range(0, 30)]
	public float jumpVelocity;
	[Range(0, 5)]
	public int maxJumps;

	int jumps = 0;
	bool grounded = false;
	bool onPlatform = false;
	bool wallOnSide = false;

	void Start() {
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
				if (coll.gameObject.CompareTag ("Platform") && onOnPlatform != null) {
					onPlatform = true;
					onOnPlatform (onPlatform);
				}
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
				if (coll.gameObject.CompareTag ("Platform") && onOnPlatform != null) {
					AudioManager.instance.playSfx (sandColisionSfx);
					onPlatform = false;
					onOnPlatform (onPlatform);
				}
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
		StartCoroutine (playRunningSfx());
		while (state == States.Running) {
			moveForward ();

			if (Input.GetKeyDown (jumpKey) || (Input.touchCount > 0))
				ChangeState (States.Jumping);
			yield return 0;
		}
	}

	IEnumerator playRunningSfx() {
		while (state == States.Running) {
			AudioManager.instance.playSfx (onPlatform ? runSfx : runSandSfx);
			yield return new WaitForSeconds (onPlatform ? runSfxWait : runSandSfxWait);
			yield return 0;
		}
	}

	IEnumerator Jumping() {
		if (jumps < maxJumps) {
			rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
			AudioManager.instance.playSfx (jumpSfx);
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
			if (Input.GetKeyDown (jumpKey) || (Input.touchCount > 0))
				ChangeState (States.Jumping);
			yield return 0;
		}
	}

	IEnumerator Dead() {
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
		AudioManager.instance.mixer.SetFloat("MelodyVol", -80);
		AudioManager.instance.mixer.SetFloat("ChorusVol", -80);
		yield return new WaitForSeconds (0.1f);
		AudioManager.instance.playSfx (dieSfx);
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
