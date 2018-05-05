using UnityEngine;

[CreateAssetMenu(fileName = "GameStateVariable", menuName = "Variables/GameStateVariable", order = 2)]
public class GameStateVariable : ScriptableObject
{
    public GameState value;
}