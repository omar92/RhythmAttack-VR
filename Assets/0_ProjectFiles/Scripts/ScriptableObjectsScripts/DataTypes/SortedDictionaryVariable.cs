using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SortedDictionaryVariable", menuName = "Variables/SortedDictionaryVariable", order = 4)]
public class SortedDictionaryVariable : ScriptableObject
{

    private SortedList<float, string> value=null;

    public SortedList<float, string> Value
    {
        get
        {
            if (this.value == null)
            {
                this.value = new SortedList<float, string>();
            }
            return this.value;
        }
        set
        {
            this.value = value;
        }
    }
}
