using System.Collections;
using UnityEngine;
using TMPro;
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
        CheckpointDialogueUpdate();

        textComponent.text = string.Empty;

        i = 0;
        line = dialog.Lines[i];

        StartCoroutine(TypeLine());
        isTalkingToPope = true;

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
        Keeper keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
        keeper.NextDialogueObject();
        //d = 1;


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

    private void CheckpointDialogueUpdate() // Calls when player has touched the checkpoint
    {
        if(gameScene) {

            d = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>().dialogueLine;
        }

        if (d >= dialogs.Length - 1)
        {
            d = dialogs.Length - 1;
        }

        dialog = dialogs[d];
    }

}
