using System;
using VRTK;
using UnityEngine;

public class VRController : MonoBehaviour
{


    //public
    public VRControllersTypes vrControllerType;
    //public TransformVariable AssociatedTransform;
    public FloatVariable slashSpeed;
    public LevelSettings settings;
    public float strength = 1f;
    public float duration = 0.2f;
    public float interval = 0.01f;

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
        trackObject.TriggerPressed += TriggerClick;
        trackObject.TriggerReleased += GripReleased;

    }
    private void TriggerClick(object sender, ControllerInteractionEventArgs e)
    {
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
        ChildCount = transform.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
            child = transform.GetChild(i).GetComponent<IControllable>();
            if (child != null)
            {
                action(child);
            }
        }
    }

    public void ViprateNote()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(gameObject), strength, 0.009f, interval-.009f);   
    }
    public void ViprateGun()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(gameObject), strength, duration, interval);
    }

}
