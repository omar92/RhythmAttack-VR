using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
    Color targetColor;
    float targetDensity;

    Color OriginalColor;
    float OriginalDensity;


    public float LerpSpeed = .1f;
    // Use this for initialization
    void Start()
    {
        OriginalColor = RenderSettings.fogColor;
        OriginalDensity = RenderSettings.fogDensity;

        targetColor = OriginalColor;
        targetDensity = OriginalDensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (RenderSettings.fogColor != targetColor)
        {
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, targetColor, LerpSpeed);
        }
        if (RenderSettings.fogDensity != targetDensity)
        {
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, targetDensity, LerpSpeed);
        }
    }

    public void SetFogColor(string color)
    {
        if (!ColorUtility.TryParseHtmlString(color, out targetColor))
        {
            targetColor = Color.black;
        }
    }
    public void SetFogColor(Color color)
    {

        targetColor = color;
    }
    public void SetFogDensity(float Density)
    {
        targetDensity = Density;
    }

    public void ResetFog()
    {
        SetFogColor(OriginalColor);
        SetFogDensity(OriginalDensity);
    }

    Coroutine co;
    public void FogAlert(string color)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
        co = StartCoroutine(FogAlertCO(color));
    }

    IEnumerator FogAlertCO(string color)
    {
        SetFogColor(color);
        SetFogDensity(.2f);
        while (RenderSettings.fogColor != targetColor)
        {
            yield return new WaitForEndOfFrame();
        }
        ResetFog();
    }

    public void ResetFog(float delay)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
        co = StartCoroutine(FogResetDelayCO(delay));
    }
    IEnumerator FogResetDelayCO(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetFog();
    }
}
