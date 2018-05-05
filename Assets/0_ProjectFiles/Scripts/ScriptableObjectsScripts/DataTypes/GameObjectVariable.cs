using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/GameObjectVariable", order = 1)]
public class GameObjectVariable : ScriptableObject
{
    public GameObject Value;
}
