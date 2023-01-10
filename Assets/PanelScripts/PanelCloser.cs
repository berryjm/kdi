using UnityEngine;
using UnityEngine.UI;

public class PanelCloser : MonoBehaviour
{
    public GameObject panel; // drag the panel object here in the inspector

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
