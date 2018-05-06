using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControll : MonoBehaviour {

    Image bar;

	void Awake () {
        bar = gameObject.GetComponentsInChildren<Image>()[1];
	}
	 
	
	public void ChangeBar(float amount)
    {
        bar.fillAmount = amount;
    }
}
