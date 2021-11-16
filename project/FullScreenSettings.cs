using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSettings : MonoBehaviour
{
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("We set full screen!");
    }
}
