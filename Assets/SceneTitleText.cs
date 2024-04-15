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

    private void Awake()
    {
        stageNumberText = GetComponent<TextMeshProUGUI>();
        stageNameText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

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
            stageNumberText.text = "STAGE THERE";
            stageNameText.text = "-The Holy Comandments-";
        }
        else 
        {
            Debug.LogWarning("No Title Text Asignd");
        }
    }

}
