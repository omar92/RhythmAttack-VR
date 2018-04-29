using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        stateMachine.ChangeState(new DefenceState());
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
       
    }
}
