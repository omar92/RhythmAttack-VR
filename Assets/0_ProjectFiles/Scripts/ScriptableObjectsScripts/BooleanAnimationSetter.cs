using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BooleanAnimationSetter : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
    }
	
    public void SetTrue(string key)
    {
        anim.SetBool(key,true);
    }

    public void SetFalse(string key)
    {
        anim.SetBool(key, false);
    }

    public void SetTrue(int index)
    {
        anim.SetBool(index, true);
    }

    public void SetFalse(int index)
    {
        anim.SetBool(index, false);
    }
}
