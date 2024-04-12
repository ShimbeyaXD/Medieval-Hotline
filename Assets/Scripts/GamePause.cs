using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] DialogManager dialogManger;

 

    // Update is called once per frame
    void Update()
    {
        if (paused) 
        { 
          Time.timeScale = 0f;
        }
        else    
        {
            Time.timeScale = 1f;
        }

        if(dialogManger.GetIsTalkingToPope() == true) { paused = true; } else { paused = false; }

        Debug.Log(paused);
    }
}
