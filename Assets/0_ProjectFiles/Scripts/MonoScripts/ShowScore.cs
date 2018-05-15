using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    public FloatVariable score;
    public Text scoreTXT;
	
	void Start ()
    {
        scoreTXT.text = score.value.ToString();
	}
	
}
