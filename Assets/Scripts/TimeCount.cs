using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {

	double tiempo;
    int tiempoSeg;
    int tiempoMin;

	public Text text;

	bool counting;

	void Start () {
        tiempo = 0;
		counting = true;
	}

	void OnEnable() {
		ActiveZone.onPlayerDeath += ActiveZone_onPlayerDeath;
	}

	void OnDisable() {
		ActiveZone.onPlayerDeath -= ActiveZone_onPlayerDeath;
	}

	void ActiveZone_onPlayerDeath ()
	{
		counting = false;
	}

	void Update () {
		if (counting)
        	tiempo += Time.deltaTime;
        tiempoMin = (int) (tiempo / 60);
        tiempoSeg = (int) tiempo - (tiempoMin * 60);

		text.text = tiempoMin.ToString("D2") + ":" + tiempoSeg.ToString("D2");
    }
}
