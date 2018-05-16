using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour
{

    Vector2 hitDirection;

    Vector2 precviousPosition;
    Vector2 currentPosition;
    Vector2 myPosition;
    Vector2 pointDirection;

    private void Update()
    {
        myPosition = new Vector2(transform.position.x, transform.position.y);
    }
    private void OnTriggerEnter(Collider other)
    {


        //if (other.tag == "Sword")
        //{
        //    if (other.GetComponent<Sword>().xyPositions.Count > 0)
        //    {
        //        currentPosition = new Vector2(other.transform.position.x, other.transform.position.y);
        //        precviousPosition = (Vector2)other.GetComponent<Sword>().xyPositions[0];
        //        hitDirection = (currentPosition - precviousPosition).normalized;
        //        pointDirection = (myPosition - currentPosition).normalized;
                
        //        if (Vector2.Dot(hitDirection, transform.up) < 0 && Vector2.Dot(pointDirection,transform.up)<0)
        //        {
        //            Debug.Log(" you are hitting from above");
        //        }else if (Vector2.Dot(hitDirection, transform.right) < 0 && Vector2.Dot(pointDirection, transform.right) < 0)
        //        {
        //            Debug.Log(" you are hitting from right");
        //        }
        //        else if (Vector2.Dot(hitDirection, -transform.right) < 0 && Vector2.Dot(pointDirection, -transform.right) < 0)
        //        {
        //            Debug.Log(" you are hitting from left");
        //        }
        //        else if (Vector2.Dot(hitDirection, -transform.up) < 0 && Vector2.Dot(pointDirection, -transform.up) < 0)
        //        {
        //            Debug.Log(" you are hitting from down");
        //        }
        //    }
          


        
    }





}
