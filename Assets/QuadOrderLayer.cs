using UnityEngine;
using System.Collections;

public class QuadOrderLayer : MonoBehaviour {

	public int sortingOrder;

	void Start () {
		GetComponent<Renderer> ().sortingOrder = sortingOrder;
	}

}
