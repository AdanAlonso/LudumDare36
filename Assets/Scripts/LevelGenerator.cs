﻿using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	[System.Serializable]
	public enum States {
		s1,
		s2,
		s4
	}
	public States state = States.s4;

	public Transform[] heights;

	public GameObject platformPrefab;
	public float timeBetweenPlatforms;
	public float platformLifetime;

	public GameObject collectablePrefab;
	public float collectableProbability;
	public float collectableLifetime;
	public float collectableHeight;

	public Rigidbody2D playerRb;

	bool onSand;

	void Awake() {
		StartCoroutine (FSM());
	}

	void Update() {
		onSand = playerRb.velocity.x < 3;
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

	IEnumerator s1() {
		yield return new WaitForSeconds(timeBetweenPlatforms);

		spawnPlatform(heights[2]);

		float random = Random.Range(0f, 1f);

		if (random < 1f / 3)
			ChangeState (States.s1);
		else if (random < 1f / 3 * 2)
			ChangeState (States.s2);
		else
			ChangeState (States.s4);
	}

	IEnumerator s2() {
		yield return new WaitForSeconds(timeBetweenPlatforms);

		spawnPlatform(heights[1]);

		float random = Random.Range(0f, 1f);

		if (random < 1f / 3)
			ChangeState (States.s1);
		else if (random < 1f / 3 * 2)
			ChangeState (States.s2);
		else
			ChangeState (States.s4);
	}

	IEnumerator s4() {
		yield return new WaitForSeconds(timeBetweenPlatforms);

		spawnPlatform(heights[0]);

		float random = Random.Range(0f, 1f);

		if (random < 1f / 2)
			ChangeState (States.s2);
		else
			ChangeState (States.s4);
	}

	void spawnPlatform(Transform at) {
		GameObject plat = Instantiate (platformPrefab, at.position, Quaternion.identity) as GameObject;
		Destroy (plat, platformLifetime);

		float random = Random.Range(0f, 1f);
		if (random < collectableProbability) {
			GameObject collectable = Instantiate (collectablePrefab, at.position + Vector3.up * collectableHeight, Quaternion.identity) as GameObject;
			Destroy (collectable, collectableLifetime);
		}
	}

}
