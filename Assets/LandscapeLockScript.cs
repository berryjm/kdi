using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeLockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown){
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
     Screen.autorotateToLandscapeLeft = true;
     Screen.autorotateToLandscapeRight = true;
     Screen.autorotateToPortrait = false;
     Screen.autorotateToPortraitUpsideDown = false;
    }

   void Update () {
     if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight){
        Screen.orientation = ScreenOrientation.AutoRotation;
     }
    }

}
