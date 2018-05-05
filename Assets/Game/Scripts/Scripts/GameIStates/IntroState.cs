using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : IState
{
    
    public void Enter(StateMachineOld stateMachine, GameManager gameManager)
    {
        stateMachine.ChangeState(new DefenceState());

      //  throw new System.NotImplementedException();
    }

    public void Excute()
    {
      //  throw new System.NotImplementedException();
    }

    public void Exit()
    {
        //throw new System.NotImplementedException();
    }
}
