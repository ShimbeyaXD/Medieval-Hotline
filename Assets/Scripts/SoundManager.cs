using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class SoundManager
{
        

    public static void PlaySound(string audioName) 
    {
        GameObject soundGameObject = new GameObject("sound : " + audioName);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        if(GameObject.FindObjectOfType<AudioHolder>() != null) 
        {
            soundGameObject.transform.parent = GameObject.FindObjectOfType<AudioHolder>().transform;

        }

        audioSource.clip = Clip(audioName);
        audioSource.volume = Volume(audioName);
        audioSource.Play();
        GameObject.FindObjectOfType<AudioHolder>().CallDestroyAudio(soundGameObject);
    }

    private static AudioClip Clip(string clipName)
    {
        var audioDataArrey = GameObject.FindObjectOfType<Keeper>().audioDataArrey;

        foreach (Keeper.AudioFileData audioFile in audioDataArrey)
        {
            if (audioFile.audioName == clipName)
            {
                if(audioFile.audioClip != null) 
                {
                    Debug.Log("Audio Clip " +  " was found");
                    return audioFile.audioClip;
                }
                else 
                {
                    Debug.LogWarning("AudioFile has no Adioclip attached to the name " + clipName);
                }

            }
        }
        Debug.LogWarning("Audio Clip was not found");
        return null;
    }

    private static float Volume(string clipName) 
    {
        var audioDataArrey = GameObject.FindObjectOfType<Keeper>().audioDataArrey;

        foreach (Keeper.AudioFileData audioFile in audioDataArrey)
        {
            if (audioFile.audioName == clipName)
            {

                 return audioFile.volume;
                
            }

        }

        return 0f;
    }



}

