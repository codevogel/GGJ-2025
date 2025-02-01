using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public bool fadeInOnStart = true;
    public float fadeDuration = 2f;
   public Material fadeMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if (fadeInOnStart)
        {
            FadeIn();
        }
        
    }

    public void FadeIn(){
        Fade(1, 0);
    }

    public void FadeOut(){
        Fade(0, 1);
    }

    public void Fade(float fadeIn, float fadeOut)
    {
        StartCoroutine(FadeRoutine(fadeIn, fadeOut));

    }

    private IEnumerator FadeRoutine(float fadeIn, float fadeOut)
    {
        Color startColor = new Color(0, 0, 0, fadeIn);
        Color endColor = new Color(0, 0, 0, fadeOut);
        float time = 0;
        while (time < fadeDuration)
        {
            fadeMaterial.color = Color.Lerp(startColor, endColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        // Ensure the final color is set
        fadeMaterial.color = endColor;
    }
}
