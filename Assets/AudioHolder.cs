using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{

    public void Awake() 
    {
        StartCoroutine(EndCurrentPlayingSounds());
    }
    IEnumerator EndCurrentPlayingSounds() 
    {
        int i;

      for (i = 0; i < gameObject.transform.childCount; i++) 
      { 
        Destroy(gameObject.transform.GetChild(i));
        yield return null;
      }
    }



    public void CallDestroyAudio(GameObject soundGameObject) 
    {
        StartCoroutine(DestroyAudio(soundGameObject, 300f));
    }
    IEnumerator DestroyAudio(GameObject soundGameObject, float timeoutDuration)
    {
        float startTime = Time.time;

        while (soundGameObject.GetComponent<AudioSource>().isPlaying && Time.time - startTime < timeoutDuration)
        {
            yield return null;
        }

        soundGameObject.GetComponent<AudioSource>().Stop();

        GameObject.Destroy(soundGameObject);
    }


}



    