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

    [SerializeField] private Dialog[] dialogs;
    [SerializeField] GameObject dialogBox;
    Dialog dialog;

    int i = 0;
    int d = 0;
    string line;
   
    [SerializeField] bool isTalkingToPope = false;

    [SerializeField] Animator talkingAnimator;

    [SerializeField] private bool gameScene = false;
    [SerializeField] GameObject hellModeManager;



    void OnEnable()
    {
        Debug.Log("Conversation Start");
        textComponent.text = string.Empty;

        dialog = dialogs[d];

        i = 0;
        line = dialog.Lines[i];


        StartCoroutine(TypeLine());
        isTalkingToPope = true;


        SceneManager.sceneLoaded += OnSceneLoad;



    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) 
    { 
      d = 0;
    }

    void Update()
    {
        if(dialog == null) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            if (textComponent.text == dialog.Lines[i]) 
            { 
              NextLine();
            }
            else 
            { 
                StopAllCoroutines();
                talkingAnimator.SetBool("isTalk", false);
                textComponent.text = dialog.Lines[i];
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
            if (i < dialogs[d].Lines.Count - 1)
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
            if (i < dialogs[d].Lines.Count - 1)
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
        d++;
        Debug.Log(d);

        if (dialog.lastChatBeforeHell == true) 
        {
            AktivateHellMode();
        }

        dialogBox.gameObject.SetActive(false);
    }

    private void AktivateHellMode()
    {

        hellModeManager.gameObject.SetActive(true);
    }

    public bool GetIsTalkingToPope() 
    {
        return isTalkingToPope;
    }

    public void RespawnCheckpoint()
    {
        d = 1;
        dialog = dialogs[d];
    }

}
