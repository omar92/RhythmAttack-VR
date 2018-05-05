using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : IState
{

    StateMachineOld stateMachine; GameManager gameManager;


    public void Enter(StateMachineOld stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
    }

    public void Excute()
    {
        Debug.Log(" i'm in lose scene ");
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    
}
