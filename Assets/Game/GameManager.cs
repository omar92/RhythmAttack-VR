using MidiPlayerTK;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    MidiFilePlayer midiPLayer;
    StateMachine stateMachine;//= new StateMachine(gameObject.GetComponent<GameManager>()); 

    void Awake()
    {
        stateMachine = new StateMachine(this);
        stateMachine.ChangeState(new InitialState());

    }

    private void Update()
    {
        stateMachine.StateUpdate();
    }


}
