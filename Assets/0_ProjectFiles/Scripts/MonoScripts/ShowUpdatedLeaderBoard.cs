using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowUpdatedLeaderBoard : MonoBehaviour
{
    public ListVariable playerList;

    public void ShowLeaderboard()
    {
     //   List<Player> playerList = JsonUtility.FromJson<List<Player>>(PlayerPrefs.GetString("scoreJson"));
        for (int i = 0; i < Mathf.Min(playerList.list.Count, transform.childCount); i++)
        {
            transform.GetChild(i+1).gameObject.SetActive(true);
            transform.GetChild(i + 1).GetComponentsInChildren<Text>()[1].text = playerList.list[i].playerScore.ToString();
            transform.GetChild(i + 1).GetComponentsInChildren<Text>()[0].text = playerList.list[i].playerName;
        } 
    }
}
