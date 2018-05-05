using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectVariable", menuName = "Variables/GameObjectVariable", order = 1)]
public class GameObjectVariable : ScriptableObject
{
    public GameObject value;
    public void SetValue(GameObject value)
    {
        this.value = value;
    }
}
