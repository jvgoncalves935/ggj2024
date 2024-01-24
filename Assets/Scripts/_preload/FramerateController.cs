using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateController : MonoBehaviour
{
    private int MAX_FPS = 60;

    void Start(){
        LimitarFPS();
    }

    private void LimitarFPS() {

        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);

        QualitySettings.vSyncCount = 1;
        //Time.captureFramerate = MAX_FPS;
        Application.targetFrameRate = MAX_FPS;
        //yield return new WaitForSeconds(1);
        //Debug.Log(Application.targetFrameRate);
    }
}
