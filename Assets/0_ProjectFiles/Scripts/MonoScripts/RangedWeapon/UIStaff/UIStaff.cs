using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class UIStaff : MonoBehaviour , IControllable
{
    public float rayLength = 1000f;
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
        line.SetPosition(0, hand.position);
        ray = new Ray(hand.position, -hand.up);
        Debug.DrawRay(hand.position, -hand.up, Color.blue);
        if (Physics.Raycast(ray, out hit, rayLength))
        {
         //   print("hit(" + hit.transform.name + ")");
            line.SetPosition(1, hit.transform.position);
            if (currentButton==null || hit.transform != currentButton.transform)
            {
                if (currentButton != null)
                {
                  //  print("UnHovering(" + currentButton.name + ")");
                    UnHovering(currentButton);
                }
                currentButton = hit.transform.GetComponent<Button>();
                if (currentButton != null)
                {
                   // print("Hovering("+ currentButton .name+ ")");
                    Hovering(currentButton);
                }
            }
        }
        else
        {
            line.SetPosition(1, -hand.up * rayLength);
            currentButton = null;
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
             //   print("OnClick(" + currentButton.name + ")");
                currentButton.onClick.Invoke();
            }
        }
    }


}
