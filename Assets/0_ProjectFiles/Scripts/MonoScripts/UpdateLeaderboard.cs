using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateLeaderboard : MonoBehaviour {

    public FloatVariable finalScore;
    public int maxNumOfBoard = 10;
    public SortedDictionaryVariable boardList;
    public GameEvent LeaderboardChanged;

    public void LeaderboadrUpdate()
    {
        if (boardList.Value.Count == 0 || boardList.Value.Count < maxNumOfBoard)
        {
            boardList.Value.Add(finalScore.value, "blablabla");
        }
        else
        {
            if (finalScore.value > boardList.Value.Keys.ElementAt(boardList.Value.Count - 1))
            {
                boardList.Value.Remove(boardList.Value.Keys.ElementAt(boardList.Value.Count - 1));
                boardList.Value.Add(finalScore.value, "blablabla");
            }
        }
        LeaderboardChanged.Raise();
    }
}
