using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceState :  IState
{

    StateMachine stateMachine; GameManager gameManager;
    HandStateController[] hands = new HandStateController[0];
    MidiFilePlayer midiPLayer;

    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
        if (hands.Length==0)
        {
            hands = GameObject.FindObjectsOfType<HandStateController>();
        }
        //Debug.LogError(hands.Length);

        Emitter.inistance.StartEmitiing();
        for (int i = 0; i < hands.Length; i++)
        {    
            hands[i].SetHandState(HandStates.Melee);
        }
    }

    public void Excute()
    {
        if (HealthIndicator.playerHealth<=0)
        {
            stateMachine.ChangeState(new LoseState());
        }
        
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
