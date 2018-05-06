using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControll : MonoBehaviour
{


    public FloatVariable BossHealth;
    public FloatVariable OriginalBossHealth;

    Image bar;

    void Awake()
    {
        bar = gameObject.GetComponentsInChildren<Image>()[1];
    }


    public void ChangeHealth()
    {
        bar.fillAmount = BossHealth.value / OriginalBossHealth.value;
    }
}
