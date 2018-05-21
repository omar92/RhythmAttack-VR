using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateLeaderboard : MonoBehaviour {

    public FloatVariable finalScore;
    public int maxNumOfBoard = 10;
    public SortedDictionaryVariable boardList;
    public GameEvent LeaderboardChanged;
    int i = 0;
    float newScore = -1f;

    public void LeaderboadrUpdate()
    {     
        if (boardList.Value.Count == 0 || boardList.Value.Count < maxNumOfBoard)
        {
            if (finalScore.value != newScore)
            {
                boardList.Value.Add(finalScore.value, i.ToString());
            }

        }
        else
        {
            if (finalScore.value > boardList.Value.Keys.ElementAt(boardList.Value.Count - 1))
            {
                boardList.Value.Remove(boardList.Value.Keys.ElementAt(boardList.Value.Count - 1));
                boardList.Value.Add(finalScore.value, i.ToString());
            }
        }
        i++;
        newScore = finalScore.value;
        LeaderboardChanged.Raise();
    }
}
