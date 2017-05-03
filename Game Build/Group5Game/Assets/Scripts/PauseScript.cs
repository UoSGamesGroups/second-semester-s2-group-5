using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    bool paused = false;

    public void onPause()
    {
        if (paused)
        {
            Time.timeScale = 1.0f;
            paused = false;
        } else
        {
            Time.timeScale = 0.0f;
            paused = true;
        }
    }
}
