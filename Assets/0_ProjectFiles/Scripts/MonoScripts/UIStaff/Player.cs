using System;
using UnityEngine;

[Serializable]
public class Player
{
    public string playerName;
    public float playerScore;

    public Player(string playerName, float playerScore)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}
