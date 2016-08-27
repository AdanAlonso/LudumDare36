using UnityEngine;
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

	void Awake() {
		StartCoroutine (FSM());
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
	}

}
