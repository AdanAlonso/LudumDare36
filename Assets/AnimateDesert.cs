using UnityEngine;
using System.Collections;

public class AnimateDesert : MonoBehaviour {

    public float scrollSpeed;

    bool playing;

    void OnEnable() {
        ActiveZone.onPlayerDeath += ActiveZone_onPlayerDeath;
    }

    void OnDisable() {
        ActiveZone.onPlayerDeath += ActiveZone_onPlayerDeath;
    }

    void Start() {
        playing = true;
        StartCoroutine(TileTexture());
    }

    private void ActiveZone_onPlayerDeath() {
        playing = false;
    }

    IEnumerator TileTexture() {
        while (playing) {
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * scrollSpeed % 1, 0);

            yield return 0;
        }
    }
}
