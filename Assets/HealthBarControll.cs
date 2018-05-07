using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControll : MonoBehaviour
{


    public FloatVariable BossHealth;
    public LevelSettings level;

    Image bar;

    void Awake()
    {
        bar = gameObject.GetComponentsInChildren<Image>()[1];
    }


    public void Update()
    {
        bar.fillAmount = BossHealth.value / level.BossHealth;
    }
}
