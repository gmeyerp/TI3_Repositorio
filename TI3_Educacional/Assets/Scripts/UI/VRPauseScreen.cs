using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPauseScreen : MonoBehaviour
{
    public void Unpause()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
