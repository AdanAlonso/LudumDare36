using UnityEngine;
using System.Collections;

public class CameraReadjust : MonoBehaviour {

	public Transform player;
	public float readjustSpeed;

	Vector3 offset;
	public bool onPlatform;

	void OnEnable() {
		Player.onOnPlatform += onOnPlatform;
	}

	void OnDisable() {
		Player.onOnPlatform -= onOnPlatform;
	}

	void Start () {
		offset = transform.position - player.position;
	}

	void Update() {
		readjust ();
	}

	void onOnPlatform(bool onPlatform) {
		this.onPlatform = onPlatform;
		if (onPlatform)
			StartCoroutine (readjust());
	}

	IEnumerator readjust() {
		while (onPlatform) {
			Vector3 currentOffset = transform.position - player.position;
			if (Vector3.Distance (offset, currentOffset) < 0.5f)
				break;
			Vector3 position = new Vector3 (player.position.x + offset.x, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, position, Time.deltaTime);
			yield return 0;
		}
	}

}
