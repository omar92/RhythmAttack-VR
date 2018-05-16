using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUpdatedScore : MonoBehaviour
{

    public FloatVariable combo;
    public FloatVariable finalScore;

    public Text upScoreTxt;
    public Text comboTxt;

    public void ScoreUpdate()
    {
        upScoreTxt.text = finalScore.value.ToString();
        comboTxt.text = combo.value.ToString();
    }
}
