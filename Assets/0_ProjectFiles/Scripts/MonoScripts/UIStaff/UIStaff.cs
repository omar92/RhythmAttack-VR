using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStaff : MonoBehaviour, IControllable
{
    public void OnTrigger(bool isDown)
    {
        // throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider collision)
    {
        var uiButton = collision.gameObject.GetComponent<Button>();
        if (uiButton)
        {
            uiButton.onClick.Invoke();
        }
    }
}
