using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting.Antlr3.Runtime;

public class DialogueInteract : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas;

    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] GameObject dialogueOptionsContainer;

    [SerializeField] Transform dialogueOptionsParent;

    [SerializeField] GameObject dialogueOptionsButtonPrefab;

    [SerializeField] DialogueObject startDialogueObject;

    bool optionSelected = false;

    bool interaction = false;

    public bool DialogueRunning()
    {
        return interaction;
    }

    public void StartDialogue()
    {
        StartCoroutine(DisplayDialogue(startDialogueObject));
    }

    public void StartDialogue(DialogueObject _dialogueobject)
    {
        StartCoroutine(DisplayDialogue(_dialogueobject));
    }

    public void OptionSelected(DialogueObject selectedOption)
    {
        optionSelected = true;
        StartDialogue(selectedOption);
    }

    IEnumerator DisplayDialogue(DialogueObject _dialogueobject)
    {
        yield return null;

        List<GameObject> spawnedButtons = new List<GameObject>();

        dialogueCanvas.enabled = true;
        while (true)
        {
            foreach (var dialogue in _dialogueobject.dialogueSegments)
            {
                interaction = true;
                dialogueText.text = "";

                foreach (char letter in dialogue.dialogueText)
                {
                    dialogueText.text += letter;
                    yield return new WaitForSeconds(dialogue.dialogueDisplayTime);
                }

                //dialogueText.text = dialogue.dialogueText;

                if (dialogue.dialogueChoices.Count == 0)
                {
                    while (!Input.GetKeyDown(KeyCode.E))
                    {
                        yield return null;
                        //yield return new WaitForSeconds(dialogue.dialogueDisplayTime);
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        yield return new WaitForEndOfFrame();
                        Debug.Log("skipped dialogue");
                    }
                }
                else
                {
                    dialogueOptionsContainer.SetActive(true);
                    // Open options panel
                    foreach (var option in dialogue.dialogueChoices)
                    {
                        GameObject newButton = Instantiate(dialogueOptionsButtonPrefab, dialogueOptionsParent);
                        spawnedButtons.Add(newButton);
                        newButton.GetComponent<UIDialogueOption>().Setup(this, option.followOnDialogue, option.dialogueChoice);
                    }

                    while (!optionSelected)
                    {
                        yield return null;
                    }
                    break;
                }
            }

            dialogueOptionsContainer.SetActive(false);
            dialogueCanvas.enabled = false;
            optionSelected = false;
            interaction = false;

            spawnedButtons.ForEach(x => Destroy(x));

            break;
        }
    }
}
