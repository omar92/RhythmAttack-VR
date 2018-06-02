using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeNoteScript : ANote
{
    public void Spawn(Vector3 source, Vector3 dist, Direction dir)
    {
        Spawn(source, dist);
        switch (dir)
        {
            case Direction.UP:
            case Direction.DOWN:

                break;
            case Direction.NONE:
            case Direction.RIGHT:
            case Direction.LEFT:
            default:

                break;
        }
    }
}