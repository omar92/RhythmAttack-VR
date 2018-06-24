using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeNoteScript : ANote
{
    public GameEvent EvadeHit;
    public GameEvent EvadePass;
    public void Spawn(Vector3 source, Vector3 dist, Direction dir)
    {
        Spawn(source, dist);
        transform.LookAt(dist);
        switch (dir)
        {
            case Direction.UP:
            case Direction.DOWN:
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.NONE:
            case Direction.RIGHT:
            case Direction.LEFT:
            default:

                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Miss")
        {
            EvadePass.Raise();
            Hide();       
        }
        else if (other.tag == "Player")
        {
            EvadeHit.Raise();
            Destroy(gameObject);
        }
    }

    private new void Hide()
    {
        Destroy(gameObject, 1f);
    }
}