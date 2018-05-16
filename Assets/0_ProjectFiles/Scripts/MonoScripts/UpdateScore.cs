using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour {

    public FloatVariable finalScore;
    public LevelSettings levelSettings;
    public FloatVariable combo;
    public GameEvent scoreChanged;

    public void ScoreIncUpdate()
    {
        scoreChanged.Raise();
        finalScore.value += combo.value;
        combo.value++;
        finalScore.value += levelSettings.scoreFactor;
    }
    public void ScoreDecUpdate()
    {
        scoreChanged.Raise();
        combo.value = 0f;
       // upScore.value -= scoreFactor.value;
       // finalScore.value = upScore.value;
    }
}
