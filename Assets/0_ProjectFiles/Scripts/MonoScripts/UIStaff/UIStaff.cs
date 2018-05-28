using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class UIStaff : MonoBehaviour , IControllable
{

    public GameEvent clickUIEvent ;

    public void OnSqueez(bool isDown)
    {
        
    }

    public void OnTrigger(bool isDown)
    {
        if (isDown)
        {
            clickUIEvent.Raise();
        }        
    }

    private void OnTriggerEnter(Collider collision)
    {
        var uiButton = collision.gameObject.GetComponent<Button>();
        if (uiButton)
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(transform.parent.gameObject), 1f);
            uiButton.onClick.Invoke();
        }
    }
}
