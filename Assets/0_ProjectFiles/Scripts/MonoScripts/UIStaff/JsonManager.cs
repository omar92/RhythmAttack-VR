using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager : MonoBehaviour
{
    public FloatVariable finalScore;
    public int maxNumOfBoard = 10;
    float newScore = -1f;
    public GameEvent LeaderboardChanged;
    string json;
    Player player;
    public ListVariable playerList;
    public StringVariable playerName;

    public void SaveJson()
    {

        if (playerList.list.Count != 0 || playerList.list.Count < maxNumOfBoard)
        {
            if (playerName.value.Length <= 0)
                playerName.value = "Player";
            player = new Player(finalScore.value, playerName.value);
            playerList.list.Add(player);
        }
        else
        {
            if (playerName.value.Length <= 0)
                playerName.value = "Player";
            if (finalScore.value > playerList.list[playerList.list.Count - 1].playerScore)
            {
                playerList.list[playerList.list.Count - 1].playerScore = finalScore.value;
                playerList.list[playerList.list.Count - 1].playerName = playerName.value;
            }
        }
        playerList.list.Sort();
        playerList.list.Reverse();
        json = JsonUtility.ToJson(playerList);
        LeaderboardChanged.Raise();
        playerName.value = "Player";
    }
}

