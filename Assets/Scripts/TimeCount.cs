using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {

	double tiempo;
    int tiempoSeg;
    int tiempoMin;

	public Text text;

	void Start () {
        tiempo = 0;
	}

	void Update () {
        tiempo += Time.deltaTime;
        tiempoMin = (int) (tiempo / 60);
        tiempoSeg = (int) tiempo - (tiempoMin * 60);

		text.text = tiempoMin.ToString("D2") + ":" + tiempoSeg.ToString("D2");
    }
}
