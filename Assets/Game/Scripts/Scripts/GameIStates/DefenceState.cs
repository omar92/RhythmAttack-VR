using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceState :  IState
{

    StateMachineOld stateMachine; GameManager gameManager;
    HandStateController[] hands = new HandStateController[0];
    MidiFilePlayer midiPLayer;
    MusicBar musicBar;
    public void Enter(StateMachineOld stateMachine, GameManager gameManager)
    {
        if (!musicBar)
        {
          
            musicBar = GameObject.FindObjectOfType<MusicBar>();
            musicBar.transform.GetChild(0).gameObject.SetActive(true);
        }
       

        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
        if (hands.Length==0)
        {
            hands = GameObject.FindObjectsOfType<HandStateController>();
        }
        //Debug.LogError(hands.Length);

        Emitter.inistance.StartEmitiing();
        for (int i = 0; i < hands.Length; i++)
        {    
            hands[i].SetHandState(HandStates.Melee);
        }
    }

    public void Excute()
    {
        //if (HealthIndicator.playerHealth<=0)
        //{
        //    stateMachine.ChangeState(new LoseState());
        //}
        
      //  if (Emitter.inistance.IsDone )
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public void Exit()
    {
        musicBar.transform.GetChild(0).gameObject.SetActive(false);

        foreach (var hand in hands)
        {
            hand.SetHandState(HandStates.Empty);
        }
    }
}
