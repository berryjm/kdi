using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
public void OnMouseDown()
{
// Load the scene with the name "SampleScene"
SceneManager.LoadScene("SampleScene");
}
}
