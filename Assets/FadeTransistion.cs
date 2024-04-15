using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransistion : MonoBehaviour
{
    [SerializeField] float fadeAmount;
    [SerializeField] float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade() 
    { 
      Color color = gameObject.GetComponent<Color>();

        color.a = 1f;

        while(color.a > 0f) 
        { 
          color.a -= fadeAmount;
          new WaitForSeconds(fadeDuration);
        }

        gameObject.SetActive(false);
        yield return null;



    }


}
