using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{

    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    private float sfxVolumeFactor;
    private float musicVolumeFactor;

    private void Awake()
    {
        sfxVolumeFactor = sfxVolumeSlider.value;
        musicVolumeFactor = musicVolumeSlider.value;
    }

    public void ChangeVolumeFactors() 
    { 
      sfxVolumeFactor = sfxVolumeSlider.value;
      musicVolumeFactor = musicVolumeSlider.value;
    }

    public float GetSFXVolumeFactor() 
    {
        return sfxVolumeFactor;
    }
    public float GetMusicVolumeFactor()
    {
        return musicVolumeFactor;
    }
}
