using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatVariableToAnim : MonoBehaviour
{


    public FloatVariable var;


    Animator anim;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void SetAnim(string key)
    {
        anim.SetInteger(key, (int)var.value);
    }

    public void SetFalse(int index)
    {
        anim.SetInteger(index, (int)var.value);
    }
}
