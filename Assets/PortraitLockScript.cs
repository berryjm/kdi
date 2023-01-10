using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitLockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight){
        Screen.orientation = ScreenOrientation.Portrait;
    }
     Screen.autorotateToPortrait = true;
     Screen.autorotateToPortraitUpsideDown = true;
     Screen.autorotateToLandscapeLeft = false;
     Screen.autorotateToLandscapeRight = false;
    }

     void Update () {
     if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown){
        Screen.orientation = ScreenOrientation.AutoRotation;
     }
    }
}
