using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : IState
{

    StateMachine stateMachine; GameManager gameManager;


    public void Enter(StateMachine stateMachine, GameManager gameManager)
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
