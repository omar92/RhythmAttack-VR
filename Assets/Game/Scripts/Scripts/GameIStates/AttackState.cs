using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    StateMachine stateMachine; GameManager gameManager;
    HandStateController[] hands =null;


    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        if (hands == null)
        {
            hands = GameObject.FindObjectsOfType<HandStateController>();
        }
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
        Emitter.inistance.enabled = true;
        foreach (var hand in hands)
        {
            hand.SetHandState(HandStates.Ranged);
        }
        //stateMachine.ChangeState(new DefenceState());
    }

    public void Excute()
    {
        new WaitForSeconds(60f);
        stateMachine.ChangeState(new DefenceState());
    }

    public void Exit()
    {
        foreach (var hand in hands)
        {
            hand.SetHandState(HandStates.Empty);
        }
    }
}
