using UnityEngine;
using System.Collections;

public class BackgroundGenerator : MonoBehaviour {

	public float waitBetweenObjects;

	[System.Serializable]
	public struct BackgroundObject
	{
		public GameObject prefab;
		public Transform spawnAt;
		public float probability;
	};

	public BackgroundObject cactus;
	public BackgroundObject column;
	public BackgroundObject bird;

	void Start() {
		StartCoroutine (spawn(cactus));
		StartCoroutine (spawn(column));
		StartCoroutine (spawn(bird));
	}

	IEnumerator spawn(BackgroundObject obj) {
		while (true) {
			yield return new WaitForSeconds (waitBetweenObjects);
			float random = Random.Range (0f, 1f);
			if (random < obj.probability) {
				Instantiate (obj.prefab, obj.spawnAt.position, Quaternion.identity);
			}
			yield return 0;
		}
	}
}
