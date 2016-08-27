using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {

	double tiempo;
    public int tiempoSeg;
    public int tiempoMin;

	public string label;

	Text text;

	void Start () {
		text = GetComponent<Text> ();
        tiempo = 0;
	}

	void Update () {
        tiempo += Time.deltaTime;
        tiempoMin = (int) (tiempo / 60);
        tiempoSeg = (int) tiempo - (tiempoMin * 60);

		text.text = label + tiempoMin.ToString("D2") + ":" + tiempoSeg.ToString("D2");
    }
}
