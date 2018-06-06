using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeadController : MonoBehaviour {

    private void Awake()
    {
        VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
    }

    protected virtual void OnEnable()
    {
       var head = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        transform.parent = head;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    protected virtual void OnDestroy()
    {
        VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
    }
}
