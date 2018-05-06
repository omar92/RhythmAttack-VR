using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/FloatVariable", order = 1)]
public class FloatVariable : ScriptableObject
{
    public float value;

    public void SetValue (float val)
    {
        value = val;
    }
}
