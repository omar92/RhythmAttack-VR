using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    StateMachineOld stateMachine; GameManager gameManager;
    HandStateController[] hands = new HandStateController[0];
    RangedTargetScript targetsScript;

    public void Enter(StateMachineOld stateMachine, GameManager gameManager)
    {
    

        if (hands.Length == 0)
        {
            hands = GameObject.FindObjectsOfType<HandStateController>();
        }
        if (!targetsScript) {
            targetsScript= GameObject.FindObjectOfType<RangedTargetScript>();
        }

        
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
        //Emitter.inistance.enabled = true;


        foreach (var hand in hands)
        {
            hand.SetHandState(HandStates.Ranged);
        }
        targetsScript.SpawnTargets();
        //stateMachine.ChangeState(new DefenceState()); 
    }

    public void Excute()
    {
        //if (targetsScript.isDone)
        {
            //if (targetsScript.health > 0)
            //{
            //    stateMachine.ChangeState(new DefenceState());
            //}
            //else
            //{
            //    stateMachine.ChangeState(new WinState());

            //}
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
