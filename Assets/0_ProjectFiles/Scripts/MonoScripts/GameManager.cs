using MidiPlayerTK;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public GameStateVariable CurrentGameState;
    public GameEvent StateChangedEvent;
   // MidiFilePlayer midiPLayer;
   // StateMachine stateMachine;//= new StateMachine(gameObject.GetComponent<GameManager>()); 

    void Awake()
    {
        StateChangedEvent.Raise();
      //  stateMachine = new StateMachine(this);
      //  stateMachine.ChangeState(new InitialState());
    }

    private void Update()
    {
     //   stateMachine.StateUpdate();
    }


}
