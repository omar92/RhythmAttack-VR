using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IState
{
    MidiFilePlayer midiPLayer;
    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        midiPLayer = gameManager.GetComponentInChildren<MidiFilePlayer>();
        if (midiPLayer)
        {
            midiPLayer.enabled = false;
        }
        MidiPlayerInitialiser.Init("Assets/Game/Resources/Tracks/");

        stateMachine.ChangeState(new IntroState());
    }

    public void Excute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        if (midiPLayer)
        {
            midiPLayer.enabled = true;
        }
    }

}
