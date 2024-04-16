using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{

    [SerializeField] bool[] scene;
    // Start is called before the first frame update

    void Start()
    {
        if (scene[0]) 
        {
            SoundManager.PlaySound("MainMenuTheme");
        }
        if (scene[1]) 
        {
            SoundManager.PlaySound("Stage1Theme");
        }
        if (scene[2])
        {
            SoundManager.PlaySound("Stage2Theme");
        }
        if (scene[3])
        {
            SoundManager.PlaySound("Stage3Theme");
        }
        else 
        {
            Debug.LogWarning("no Theme playing");
        }

    }

    public void Sound1() 
    {
        SoundManager.PlaySound("Slash");
    }


}
