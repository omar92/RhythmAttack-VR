using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ObjectVariable", menuName = "Variables/ObjectVariable", order = 2)]
public class ObjectVariable : ScriptableObject
{
    public object value;
}