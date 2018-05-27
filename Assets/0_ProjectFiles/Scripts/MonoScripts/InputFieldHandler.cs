using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
    InputField field;

    private void Start()
    {
        field = transform.GetComponent<InputField>();
    }

    public void ValueChanged(string value)
    {
        field.text = value;
    }
}
