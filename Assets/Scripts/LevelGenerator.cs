using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	[System.Serializable]
	public enum States {
		stopped,
		s1,
		s2,
		s4,
		s1short,
		s2short,
		s4short
	}
	public States state = States.s4;

	public Transform[] heights;

	public GameObject platformPrefab;
	public float timeBetweenPlatforms;

	public GameObject shortPlatformPrefab;
	public float timeBetweenShortPlatforms;
	public int minShortInterval;
	public int maxShortInterval;
	public int currentShortInterval;

	public GameObject[] collectablePrefabs;
	public float collectableProbability;
	public float collectableHeight;

	public GameObject rockContainerPrefab;
	public float rockProbability;
	public Transform rockHeight;

	public Rigidbody2D playerRb;

	void Awake() {
		currentShortInterval = 0;
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
		currentShortInterval = 0;
		yield return new WaitForSeconds(timeBetweenPlatforms);

		spawnPlatform(platformPrefab, heights[2]);

		float random = Random.Range(0f, 1f);

		if (random < 1f / 3)
			ChangeState (States.s1);
		else if (random < 1f / 3 * 2)
			ChangeState (States.s2);
		else
			ChangeState (States.s4);
	}

	IEnumerator s2() {
		currentShortInterval = 0;
		yield return new WaitForSeconds(timeBetweenPlatforms);

		spawnPlatform(platformPrefab, heights[1]);

		float random = Random.Range(0f, 1f);

		if (random < 1f / 3)
			ChangeState (States.s1);
		else if (random < 1f / 3 * 2)
			ChangeState (States.s2);
		else
			ChangeState (States.s4);
	}

	IEnumerator s4() {
		currentShortInterval = 0;
		yield return new WaitForSeconds(timeBetweenPlatforms);

		spawnPlatform(platformPrefab, heights[0]);

		float random = Random.Range(0f, 1f);

		if (random < 1f / 3)
			ChangeState (States.s2);
		else if (random < 1f / 3 * 2) {
			yield return new WaitForSeconds(timeBetweenShortPlatforms);
			ChangeState (States.s2short);
		}
		else
			ChangeState (States.s4);
	}

	IEnumerator s1short() {
		++currentShortInterval;
		yield return new WaitForSeconds(timeBetweenShortPlatforms);

		spawnPlatform(shortPlatformPrefab, heights[2]);

		float random = Random.Range(0f, 1f);

		if (currentShortInterval < minShortInterval) {
			ChangeState (States.s2short);
		}

		if (currentShortInterval >= maxShortInterval)
			ChangeState (States.s4);

		if (random < 1f / 2)
			ChangeState (States.s2short);
		else
			ChangeState (States.s4);
	}

	IEnumerator s2short() {
		++currentShortInterval;
		yield return new WaitForSeconds(timeBetweenShortPlatforms);

		spawnPlatform(shortPlatformPrefab, heights[1]);

		float random = Random.Range(0f, 1f);

		if (currentShortInterval < minShortInterval) {
			if (random < 1f / 2)
				ChangeState (States.s1short);
			else
				ChangeState (States.s4short);
		}

		if (currentShortInterval >= maxShortInterval)
			ChangeState (States.s4);

		if (random < 1f / 3)
			ChangeState (States.s1short);
		else if (random < 1f / 3 * 2)
			ChangeState (States.s4short);
		else 
			ChangeState (States.s4);
	}

	IEnumerator s4short() {
		++currentShortInterval;
		yield return new WaitForSeconds(timeBetweenShortPlatforms);

		spawnPlatform(shortPlatformPrefab, heights[0]);

		float random = Random.Range(0f, 1f);

		if (currentShortInterval < minShortInterval) {
			ChangeState (States.s2short);
		}

		if (currentShortInterval >= maxShortInterval)
			ChangeState (States.s4);

		if (random < 1f / 2)
			ChangeState (States.s2short);
		else
			ChangeState (States.s4);
	}

	void spawnPlatform(GameObject platform, Transform at) {
		Instantiate (platform, at.position, Quaternion.identity);

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
