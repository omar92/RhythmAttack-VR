using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
  //  public GameObject gun;

    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
     //   gun.SetActive(true);
        stateMachine.ChangeState(new DefenceState());
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
       
    }
}
