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
                player = new Player(finalScore.value , playerName.value);
                playerList.list.Add(player);
            Debug.Log("D5al fl ola ahooo");
            //}
        }
        else
        {
            Debug.Log("D5al fl else ahooo");
            if (finalScore.value > playerList.list[playerList.list.Count-1].playerScore)
            {
                playerList.list[playerList.list.Count - 1].playerScore = finalScore.value;
                playerList.list[playerList.list.Count - 1].playerName = playerName.value;
            }            
        }
        //newScore = finalScore.value;
        playerList.list.Sort();
        playerList.list.Reverse();
        foreach (var item in playerList.list)
        {
            Debug.Log("Listaaa Name"+ item.playerName+ "Listaaa Score" + item.playerScore);
        }
        json = JsonUtility.ToJson(playerList);
        //PlayerPrefs.SetString("scoreJson", json);
        LeaderboardChanged.Raise();
        playerName.value = "";
    }

}

