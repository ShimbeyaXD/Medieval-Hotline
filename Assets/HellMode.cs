using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellMode : MonoBehaviour
{
    [SerializeField] GameObject redMist;
    [SerializeField] float alphaAmount = 0.5f;

    Extraction extraction;

    // Start is called before the first frame update
    void OnEnable()    
    {
        extraction = FindObjectOfType<Extraction>();
        redMist.SetActive(true);
    }

    public void FillOpacity()
    {
        StartCoroutine(RedTransition());
    }

    IEnumerator RedTransition()
    {
        while (true)
        {
            Debug.Log("Tranistiion color");
            Color red = redMist.GetComponent<Image>().color;

            red += red * alphaAmount * Time.deltaTime;

            redMist.GetComponent<Image>().color = red;

            yield return new WaitForEndOfFrame();
        }

    }
}
