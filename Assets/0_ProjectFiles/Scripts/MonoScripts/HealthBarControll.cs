using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControll : MonoBehaviour
{


    public FloatVariable Health;
    public LevelSettings level;

    public bool isPLayer = false;

    Image bar;

    void Awake()
    {
        bar = gameObject.GetComponentsInChildren<Image>()[1];
    }


    public void ChangeHealth()
    {
        if (isPLayer)
        {
            bar.fillAmount = Health.value / level.PlayerHealth;
        }
        else
        {
            bar.fillAmount = Health.value / level.BossHealth;
        }
    }
}
