using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFinalScore : MonoBehaviour {

    public FloatVariable Finalscore;
    public Text scoreTXT;
	
	void Start ()
    {
        scoreTXT.text = Finalscore.value.ToString();
	}
	
}
