using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTitleText : MonoBehaviour
{
    [SerializeField] List<bool> sceneTitleNumber = new List<bool>();

    TextMeshProUGUI stageNumberText;
    TextMeshProUGUI stageNameText;
    Animator textFadeAnimator;
    Animator textFadeAnimatorParent;
    Keeper keeper;

    private void Awake()
    {
        stageNumberText = GetComponent<TextMeshProUGUI>();
        stageNameText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textFadeAnimator = stageNameText.GetComponent<Animator>();
        textFadeAnimatorParent = GetComponent<Animator>();
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();

        if (!keeper.GrantCheckpoint)
        {
            textFadeAnimator.SetBool("ShowText", true);
            textFadeAnimatorParent.SetBool("ShowText", true);
        }
        else { return; }

        if (sceneTitleNumber[0]) 
        {
            stageNumberText.text = "STAGE ONE";
            stageNameText.text = "-The Holy Bible-";
        }
        if (sceneTitleNumber[1])
        {
            stageNumberText.text = "STAGE TWO";
            stageNameText.text = "-The Holy Grail-";
        }
        if (sceneTitleNumber[2])
        {
            stageNumberText.text = "STAGE THREE";
            stageNameText.text = "-The Holy Commandments-";
        }
        else 
        {
            Debug.LogWarning("No Title Text Assigned or player has passed the checkpoint");
        }
    }

}
