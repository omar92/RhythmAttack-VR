using System;
using VRTK;
using UnityEngine;

public class VRController : MonoBehaviour
{


    //public
    public VRControllersTypes vrControllerType;
    public TransformVariable AssociatedTransform;

    //Private
    VRTK_ControllerEvents trackObject = null;



   // public static int currentDeviceIndex;
    Color currentColler;
    int ChildCount = 0;
    IControllable child;
    void Awake()
    {
        trackObject = GetComponent<VRTK_ControllerEvents>();
        //register events
        trackObject.TriggerPressed += GripPressed;
        trackObject.TriggerReleased += GripReleased;

    }
    private void GripPressed(object sender, ControllerInteractionEventArgs e)
    {
       // Viprate();
        ExcuteInChildren((child) =>
        {
            child.OnTrigger(true);
        });
    }
    private void GripReleased(object sender, ControllerInteractionEventArgs e)
    {
        ExcuteInChildren((child) =>
        {
            child.OnTrigger(false);
        });
    }

    void ExcuteInChildren(Action<IControllable> action)
    {
        ChildCount = AssociatedTransform.value.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
            child = AssociatedTransform.value.GetChild(i).GetComponent<IControllable>();
            if (child != null)
            {
                action(child);
            }
        }
    }

    public void Viprate()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(gameObject), 1f,1F,.01F);
    }

    void FixedUpdate()
    {
        AssociatedTransform.value.position = transform.position;
        AssociatedTransform.value.rotation = transform.rotation;
    }
    void Update()
    {
        AssociatedTransform.value.position = transform.position;
        AssociatedTransform.value.rotation = transform.rotation;
    }
    private void LateUpdate()
    {
        AssociatedTransform.value.position = transform.position;
        AssociatedTransform.value.rotation = transform.rotation;
    }
}
