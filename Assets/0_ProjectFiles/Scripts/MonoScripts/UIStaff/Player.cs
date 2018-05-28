using System;
using UnityEngine;

[Serializable]
public class Player :IComparable
{
    public string playerName;
    public float playerScore;

    public Player(float playerScore , string playerName = "Player")
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }

    public int CompareTo(object obj)
    {
        var to = (Player)obj;

        if (this.playerScore > to.playerScore) return 1;
        else if (this.playerScore < to.playerScore) return -1;
        else return 0;
    }
}
