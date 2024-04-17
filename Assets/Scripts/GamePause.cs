using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] private Animator menuAnimator;

    bool menuActive = false;



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

        if(dialogueManager.GetIsTalkingToPope() == true) { paused = true; } else { paused = false; }
        //PLayer Dies will also pause fix later
        Menu();
    }

    void Menu()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuActive)
            {
                menuAnimator.SetTrigger("PauseGame");
                menuActive = true;
            }
            else
            {
                menuAnimator.SetTrigger("UnPauseGame");
                menuActive = false;
            }
        }
    }
}
