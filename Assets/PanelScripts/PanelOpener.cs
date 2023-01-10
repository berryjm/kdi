using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel; // drag the panel object here in the inspector

    public void OpenPanel()
    {
        panel.SetActive(true);
    }
}

