using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteVariableOnText : MonoBehaviour {

    public Text textField;
    public FloatVariable variable;

	public void Write()
    {
        textField.text = variable.value + "";
    }
}
