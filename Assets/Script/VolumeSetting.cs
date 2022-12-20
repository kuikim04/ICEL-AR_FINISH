using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Script
{ 
  [System.Serializable] 
    public class Sound
    {
        [HideInInspector] public AudioSource source;
        public string clipname;
        public AudioClip audioClip;

        [Range(0, 1)]
        public float volume = 0.5f;
    }


    public class VolumeSetting : MonoBehaviour
    {
        public Slider soundSlider;
        public Slider musicSlider;
        public AudioMixer audioMixer;
        public AudioMixer musicAudioMixer;
        private void Awake()
        {
            soundSlider.value = Singleton.Instance.soundVolume;
            musicSlider.value = Singleton.Instance.musicVolume;
        }
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
            Singleton.Instance.soundVolume = volume;
        }

        public void SetMusicVolume(float volume)
        {
            musicAudioMixer.SetFloat("musicvolume", volume);
            Singleton.Instance.musicVolume = volume;
        }
    } 
}

