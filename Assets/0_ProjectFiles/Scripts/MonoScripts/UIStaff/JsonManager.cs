using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour {

    public FloatVariable finalScore;
    public int maxNumOfBoard = 10;
    float newScore = -1f;
    public GameEvent LeaderboardChanged;
    string json;
    Player player;
    public List<Player> playerList;

    void Awake()
    {
        playerList = new List<Player>();
    }

    public void SaveJson()
    {
        if (playerList.Count == 0 || playerList.Count < maxNumOfBoard)
        {
            //if (finalScore.value != newScore)
            //{
                player = new Player("BlaBlaBla", finalScore.value);
                playerList.Add(player);
            //}
        }
        else
        {
            if (finalScore.value > playerList[playerList.Count-1].playerScore)
            {
                playerList[playerList.Count - 1].playerScore = finalScore.value;
            }
        }
        //newScore = finalScore.value;
        json = JsonUtility.ToJson(playerList);
        PlayerPrefs.SetString("scoreJson", json);
        LeaderboardChanged.Raise();        
    }

}

