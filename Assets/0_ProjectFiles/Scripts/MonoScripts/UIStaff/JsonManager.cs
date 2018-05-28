using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager : MonoBehaviour {

    public FloatVariable finalScore;
    public int maxNumOfBoard = 10;
    float newScore = -1f;
    public GameEvent LeaderboardChanged;
    string json;
    Player player;
    public ListVariable playerList;
    public StringVariable playerName;


    //void Awake()
    //{
    //    //if (PlayerPrefs.GetString("scoreJson") == null)
    //    //{
    //    //    playerList = new List<Player>();
    //    //}else
    //    //{
    //     //   playerList = JsonUtility.FromJson<ListVariable>(PlayerPrefs.GetString("scoreJson"));
    //    //}

    //} 

    public void SaveJson()
    {
        if (playerList.list.Count == 0 || playerList.list.Count < maxNumOfBoard)
        {
            //if (finalScore.value != newScore)
            //{
                player = new Player(playerName.value, finalScore.value);
                playerList.list.Add(player);
            //}
        }
        else
        {
            for (int i = 0; i < playerList.list.Count; i++)
            {
                if (finalScore.value > playerList.list[i].playerScore)
                {
                    playerList.list[i].playerScore = finalScore.value;
                    playerList.list[i].playerName = playerName.value;
                }
            }
            
        }
        //newScore = finalScore.value;
        playerList.list.Sort();
        playerList.list.Reverse();
        json = JsonUtility.ToJson(playerList);
        //PlayerPrefs.SetString("scoreJson", json);
        LeaderboardChanged.Raise();        
    }

}

