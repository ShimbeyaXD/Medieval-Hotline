using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float textSpeed;

    [SerializeField] private Dialog justATest;
    [SerializeField] GameObject dialogBox;

    Dialog dialog;
    int i = 0;
    string line;
   [SerializeField] bool isTalkingToPope = false;

    [SerializeField] Animator talkingAnimator;

    [SerializeField] private bool gameScene = false;
    [SerializeField] GameObject hellModeManager;

    //DemonSpawning
    DemonSpawner demonSpawner;

    //Objective Text UI
    ObjectiveUI objectiveUI;

    //Follow Target for 'camerashake'
    FollowTarget followTarget;

    void Start()
    {
        textComponent.text = string.Empty;
        dialog = justATest;
        i = 0;
        line = dialog.Lines[i];
        StartCoroutine(TypeLine());
        isTalkingToPope = true;

        demonSpawner = FindObjectOfType<DemonSpawner>();
        objectiveUI = FindObjectOfType<ObjectiveUI>();
        followTarget = FindObjectOfType<FollowTarget>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (textComponent.text == justATest.Lines[i]) 
            { 
              NextLine();
            }
            else 
            { 
                StopAllCoroutines();
                talkingAnimator.SetBool("isTalk", false);
                textComponent.text = justATest.Lines[i];
            }
        }


    }

    IEnumerator TypeLine() 
    {
        if (gameScene) 
        {
            talkingAnimator.SetBool("isTalk", false);
            talkingAnimator.SetBool("isTalkInGame", true);
        }
        else 
        {
            talkingAnimator.SetBool("isTalk", true);
            talkingAnimator.SetBool("isTalkInGame", false);
        }

        
      foreach (char c in line) 
      {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
      }
       
        talkingAnimator.SetBool("isTalk", false);
        talkingAnimator.SetBool("isTalkInGame", false);
    }

    private void NextLine()
    {
        if (!gameScene) 
        {
            if (i < justATest.Lines.Count - 1)
            {
                i++;
                Debug.Log("preseed button");
                textComponent.text = string.Empty;
                line = dialog.Lines[i];
                StartCoroutine(TypeLine());

            }
            else
            {
                SceneManager.LoadScene("Level1");
            }
        }

        if (gameScene) 
        {
            if (i < justATest.Lines.Count - 1)
            {
                i++;
                Debug.Log("preseed button");
                textComponent.text = string.Empty;
                line = dialog.Lines[i];
                StartCoroutine(TypeLine());

            }
            else 
            {
                StoppedDialogue();
            }
                
        }

    }

    private void StoppedDialogue()
    {
        isTalkingToPope = false;
        AktivateHellMode();
        dialogBox.gameObject.SetActive(false);
    }

    private void AktivateHellMode()
    {
        followTarget.StartShake(1.3f, 0.2f);

        demonSpawner.DemonSpawnInitiate();
        objectiveUI.GetOutText();
        hellModeManager.gameObject.SetActive(true);
    }

    public bool GetIsTalkingToPope() 
    {
        return isTalkingToPope;
    }
}
