using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceState : IState
{

    StateMachine stateMachine; GameManager gameManager;
    public GameObject sword;
    MidiFilePlayer midiPLayer;
    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;

        midiPLayer = gameManager.GetComponentInChildren<MidiFilePlayer>();
        if (midiPLayer)
        {
            midiPLayer.MPTK_Play();
        }
    }

    public void Excute()
    {
        if (!midiPLayer.MPTK_IsPlaying)
        {
            sword.SetActive(false);
            stateMachine.ChangeState(new AttackState());
        }
    }

    public void Exit()
    {
      //  throw new System.NotImplementedException();
    }
}
