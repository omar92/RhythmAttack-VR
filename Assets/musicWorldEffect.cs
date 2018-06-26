using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicWorldEffect : MonoBehaviour
{
    public float[] musicData;
    public TransformVariable audioST;
    public AudioSource audioS;
    public FFTWindow filter;
    public Color BaseColor;
    public Color Mask;
    public float scale;
    public Material[] ImissionMats;
    public Material[] ColorMats;
    public Transform[] TransformsScale;
    public float scale2;
    public bool ApplyOnSkyBox = false;
   
    // Use this for initialization
    void Start()
    {

    }

    Color color;
    // Update is called once per frame
    void Update()
    {
        if (audioST.value != null)
        {
            audioS = audioST.value.GetComponent<AudioSource>();
        }
        if (audioS)
        {
            musicData = audioS.GetSpectrumData(64, 0, filter);
        }
        color = BaseColor * musicData[0]* scale;
        for (int i = 0; i < ImissionMats.Length; i++)
        {
            ImissionMats[i].SetColor("_EmissionColor", color);
        }
        for (int i = 0; i < ColorMats.Length; i++)
        {
            ColorMats[i].SetColor("_Color", color);
        }
        if (ApplyOnSkyBox)
        {
            if (RenderSettings.skybox.HasProperty("_Tint"))
                RenderSettings.skybox.SetColor("_Tint", color);
            if (RenderSettings.skybox.HasProperty("_SkyTint"))
                RenderSettings.skybox.SetColor("_SkyTint", color);
        }

        for (int i = 0; i < TransformsScale.Length; i++)
        {
            TransformsScale[i].localScale = new Vector3(musicData[0]* scale2, musicData[0] * scale2, musicData[0] * scale2);
        }
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[ExecuteInEditMode]
//public class BuildingColors : MonoBehaviour
//{
//    public Color color, emissionColor;
//    public float intensity = 1f;
//    public List<Material> myMats;
//    private Color tempEmissionColor, tempColor;
//    private float tempIntensity;

//    void Update()
//    {
//        if (emissionColor != tempEmissionColor || intensity != tempIntensity || color != tempColor)
//        {
//            tempColor = color;
//            tempEmissionColor = emissionColor;
//            tempIntensity = intensity;
//            for (int i = 0; i < myMats.Count; i++)
//            {
//                myMats[i].SetColor("_Color", color);
//                myMats[i].SetColor("_EmissionColor", emissionColor * intensity);
//            }
//        }
//    }
//}