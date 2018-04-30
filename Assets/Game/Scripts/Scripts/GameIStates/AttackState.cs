using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    StateMachine stateMachine; GameManager gameManager;
    GameObject gun;

    private void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun");
    }
    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
        Emitter.inistance.enabled = true;
        gun.SetActive(true);
        //stateMachine.ChangeState(new DefenceState());
    }

    public void Excute()
    {
        new WaitForSeconds(60f);
        stateMachine.ChangeState(new DefenceState());
    }

    public void Exit()
    {
        gun.SetActive(false);
    }
}
