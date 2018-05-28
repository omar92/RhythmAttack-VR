using System;
using UnityEngine;

[Serializable]
public class Player
{
    public string playerName;
    public float playerScore;

    public Player(float playerScore , string playerName = "Player")
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}
