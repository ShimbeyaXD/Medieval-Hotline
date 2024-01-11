using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    Artifact artifact;

    int timeInt;

    void OnEnable()
    {
        StartCoroutine(AddTime());

        artifact = FindObjectOfType<Artifact>();
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (artifact.LevelCleared)
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
