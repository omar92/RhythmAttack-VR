using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceState :  IState
{

    StateMachine stateMachine; GameManager gameManager;
    HandStateController[] hands = null;
    MidiFilePlayer midiPLayer;

    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
        if (hands == null)
        {
            hands = GameObject.FindObjectsOfType<HandStateController>();
        }


        Emitter.inistance.StartEmitiing();
        foreach (var hand in hands)
        {
            hand.SetHandState(HandStates.Melee);
        }
    }

    public void Excute()
    {
        if (Emitter.inistance.IsDone )
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public void Exit()
    {
        foreach (var hand in hands)
        {
            hand.SetHandState(HandStates.Empty);
        }
    }
}
