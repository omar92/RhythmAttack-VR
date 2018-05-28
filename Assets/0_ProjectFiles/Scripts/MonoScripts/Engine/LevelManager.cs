using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public FloatVariable GameLevel;
    public GameEvent LevelChangedE;

    public void IncreaseLevel()
    {
        GameLevel.value += 1;
        LevelChangedE.Raise();
    }
}
