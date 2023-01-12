using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkbox : MonoBehaviour
{
    public int checkboxIndex;
    public CheckboxData checkboxData;

    public void CheckboxChanged(bool value)
    {
        // Debug.Log("Checkbox " + checkboxIndex + ": " + value);
        // Debug.Log("checkboxData: " + checkboxData.name);
        checkboxData.checkboxStates[checkboxIndex] = value;
    }
}
