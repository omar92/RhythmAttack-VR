using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceState : MonoBehaviour, IState
{

    StateMachine stateMachine; GameManager gameManager;
    GameObject sword;
    MidiFilePlayer midiPLayer;
    private void Start()
    {
        sword = GameObject.FindGameObjectWithTag("Gun");
    }
    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;

        //midiPLayer = gameManager.GetComponentInChildren<MidiFilePlayer>();
        //if (midiPLayer)
        //{
        //    midiPLayer.MPTK_Play();
        //}
        Emitter.inistance.StartEmitiing();
        sword.SetActive(true);

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
        sword.SetActive(false);
    }
}
