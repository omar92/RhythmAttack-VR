using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class UIStaff : MonoBehaviour , IControllable
{
    public float rayLength = 10f;
    public Ray ray;
    public RaycastHit hit;
    public GameEvent clickUIEvent ;

    Transform hand;
    Button currentButton = null;
    LineRenderer line;

    private void Start()
    {
        hand = transform.GetChild(0);
        line = transform.GetComponent<LineRenderer>();
       
        
    }
    public void OnSqueez(bool isDown)
    {
        
    }
    private void Update()
    {
        RayCastUI();
    }
    private void RayCastUI()
    {
        line.SetPosition(0, hand.localPosition);
        ray = new Ray(hand.position, hand.forward);
        Debug.DrawRay(hand.position, hand.forward, Color.blue);
        if (Physics.Raycast(ray, out hit, rayLength))
        { 
            line.SetPosition(1, hit.transform.localPosition);
            if (hit.transform != currentButton.transform)
            {
                if (currentButton != null)
                {
                    UnHovering(currentButton);
                }
                currentButton = hit.transform.GetComponent<Button>();
                if (currentButton != null)
                {
                    Hovering(currentButton);
                }
            }
        }
        else
        {
            line.SetPosition(1, hand.forward * -rayLength);
        }
    }

    void Hovering(Button button)
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(transform.parent.gameObject), 1f);
    }
    void UnHovering(Button button)
    {

    }
    public void OnTrigger(bool isDown)
    {
        if (isDown)
        {
            clickUIEvent.Raise();
            if (currentButton != null)
            {
                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(transform.parent.gameObject), 1f);
                currentButton.onClick.Invoke();
            }
        }
    }

    //private void OnTriggerEnter(Collider collision)
    //{
        //var uiButton = collision.gameObject.GetComponent<Button>();
        //if (uiButton)
        //{

        //    uiButton.onClick.Invoke();
        //}
    //}
}
