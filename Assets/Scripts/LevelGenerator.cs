using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	[System.Serializable]
	public enum States {
		stopped,
		s1,
		s2,
		s4
	}
	public States state = States.s4;

	public Transform[] heights;

	public GameObject platformPrefab;
	public float timeBetweenPlatforms;

	public GameObject[] collectablePrefabs;
	public float collectableProbability;
	public float collectableHeight;

	public GameObject rockContainerPrefab;
	public float rockProbability;
	public Transform rockHeight;

	public Rigidbody2D playerRb;

	void Awake() {
		StartCoroutine (FSM());
	}

	void OnEnable() {
		ActiveZone.onPlayerDeath += PlayerKiller_onPlayerDeath;
	}

	void OnDisable() {
		ActiveZone.onPlayerDeath -= PlayerKiller_onPlayerDeath;
	}

	void PlayerKiller_onPlayerDeath ()
	{
		ChangeState (States.stopped);
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

	IEnumerator stopped() {
		while (state == States.stopped) {
			yield return 0;
		}
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
		Instantiate (platformPrefab, at.position, Quaternion.identity);

		float random = Random.Range(0f, 1f);
		if (random < collectableProbability) {
			Instantiate (collectablePrefabs[Random.Range(0, collectablePrefabs.Length)], at.position + Vector3.up * collectableHeight, Quaternion.identity);
		}

		random = Random.Range(0f, 1f);
		if (random < rockProbability) {
			Instantiate (rockContainerPrefab, rockHeight.position, Quaternion.identity);
		}
	}

}
