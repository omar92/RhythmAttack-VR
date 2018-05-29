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
    public BoolVariable triggerClicked;
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
        trackObject.TriggerClicked += TriggerSqueezed;

    }

    // L triggerClicked boolVariable msh 48al test why instead the Event working well;

    private void TriggerClick(object sender, ControllerInteractionEventArgs e)
    {
        triggerClicked.value = true;
       // NormalViprate();
        ExcuteInChildren((child) =>
        {
            child.OnTrigger(true);
        });
    }
    private void GripReleased(object sender, ControllerInteractionEventArgs e)
    {
        triggerClicked.value = false;
        ExcuteInChildren((child) =>
        {
            child.OnTrigger(false);
        });
    }
    private void TriggerSqueezed(object sender, ControllerInteractionEventArgs e)
    {
        triggerClicked.value = true;
        ExcuteInChildren((child) =>
        {
            child.OnSqueez(true);
        });
    }
    void ExcuteInChildren(Action<IControllable> action)
    {
        ChildCount = transform.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
           var childGO = transform.GetChild(i);
            child = childGO.GetComponent<IControllable>();
            if (child != null&& childGO.gameObject.activeInHierarchy)
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
    public void NormalViprate()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(gameObject), strength, duration, interval);
    }

}
