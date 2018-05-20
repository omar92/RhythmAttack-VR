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
        finalScore.value += combo.value;
        combo.value++;
        finalScore.value += levelSettings.scoreFactor;
        scoreChanged.Raise();
    }
    public void ScoreDecUpdate()
    {
        combo.value = 0f;
        scoreChanged.Raise();
        // upScore.value -= scoreFactor.value;
        // finalScore.value = upScore.value;
    }
}
