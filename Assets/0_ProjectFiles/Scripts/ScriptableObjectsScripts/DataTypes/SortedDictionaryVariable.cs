using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SortedDictionaryVariable", menuName = "Variables/SortedDictionaryVariable", order = 4)]
public class SortedDictionaryVariable : ScriptableObject
{

    private SortedDictionary<float, string> value=null;

    public SortedDictionary<float, string> Value
    {
        get
        {
            if (Value == null)
            {
               return Value = new SortedDictionary<float, string>();
            }
            return value;
        }

        set
        {
            this.value = value;
        }
    }
}
