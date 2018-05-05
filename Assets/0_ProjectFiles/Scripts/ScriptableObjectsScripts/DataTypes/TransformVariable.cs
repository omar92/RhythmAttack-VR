using UnityEngine;


[CreateAssetMenu(fileName = "TransformVariable", menuName = "Variables/TransformVariable", order = 1)]
public class TransformVariable : ScriptableObject
{

    public Transform value;
    public void SetValue(Transform value)
    {
        this.value = value;
    }
}