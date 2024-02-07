using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> clips;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
       audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(string soundName) 
    { 
        
        for (int i = 0; i < clips.Count; i++) 
        {
            Debug.Log("SFXPlay");
            if (soundName == clips[i].name){
                audioSource.clip = clips[i];
                audioSource.Play();
            }

           
        }
    }

    public void RunningSFX(bool running) 
    { 
        if (running)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else 
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        

    }

    public void EnemyDeathSound()
    {
        StartCoroutine(PlayEnemyDeathSound());
    }

    IEnumerator PlayEnemyDeathSound()
    {
        GameObject enemySoundObject = transform.GetChild(1).gameObject;

        enemySoundObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        enemySoundObject.SetActive(false); 
    }
}
