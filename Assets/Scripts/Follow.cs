using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform follow;
	public bool followX;
	public bool followY;

	Vector3 offset;

	void Start () {
		offset = transform.position - follow.position;
	}

	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		if (followX)
			x = follow.position.x + offset.x;
		if (followY)
			y = follow.position.y + offset.y;
		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
