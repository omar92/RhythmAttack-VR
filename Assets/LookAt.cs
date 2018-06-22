using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {


    public TransformVariable target;

    private void FixedUpdate()
    {
        transform.LookAt(target.value);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
