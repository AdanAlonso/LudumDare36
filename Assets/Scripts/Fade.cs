using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour {

    public Image fadeTexture;
    public float fadeTime;

    void Awake() {
        FadeIn();
    }

    public void FadeIn() {
        StartCoroutine(fade(true));
    }

    public void FadeOut() {
        StartCoroutine(fade(false));
    }

    IEnumerator fade(bool fadeIn) {
        float alphaFrom = fadeIn ? 1 : 0;
        float alphaTo = fadeIn ? 0 : 1;

        float timer = 0;
        while (timer < fadeTime) {
            float alpha = Mathf.Lerp(alphaFrom, alphaTo, timer / fadeTime);
            fadeTexture.color = new Color(fadeTexture.color.r, fadeTexture.color.g, fadeTexture.color.b, alpha);

            yield return 0;
            timer += Time.fixedDeltaTime;
        }
        fadeTexture.color = new Color(fadeTexture.color.r, fadeTexture.color.g, fadeTexture.color.b, alphaTo);
    }
}