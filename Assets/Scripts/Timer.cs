using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    Artifact artifact;
    Keeper keeper;

    int timeInt;

    void OnEnable()
    {
        StartCoroutine(AddTime());

        artifact = FindObjectOfType<Artifact>();
        timerText = GetComponent<TextMeshProUGUI>();

        keeper = GameObject.FindObjectOfType<Keeper>();
    }

    void Update()
    {
        if (keeper.IsLevelCleared)
        {
            StopCoroutine(AddTime());
        }
    }

    IEnumerator AddTime()
    {
        timeInt =+ 1;
        timerText.text = timeInt.ToString();

        yield return new WaitForSeconds(1);
    }

}
