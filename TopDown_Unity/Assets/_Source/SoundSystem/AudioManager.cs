using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace SoundSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        private const string MASTER_NAME = "Master";
        private const string MUSIC_NAME = "Music";
        private const string SOUND_NAME = "Sound";

        private void Awake()
        {
            mixer.SetFloat(MASTER_NAME,  PlayerPrefs.HasKey(MASTER_NAME) ? PlayerPrefs.GetFloat(MASTER_NAME) : 1f);
            mixer.SetFloat(MUSIC_NAME, PlayerPrefs.HasKey(MUSIC_NAME) ? PlayerPrefs.GetFloat(MUSIC_NAME) : 1f);
            mixer.SetFloat(SOUND_NAME, PlayerPrefs.HasKey(SOUND_NAME) ? PlayerPrefs.GetFloat(SOUND_NAME) : 1f);
        }

        public void ChangeMaster(float value)
        {
            float resultValue = Mathf.Log10(value == 0 ? 0.001f : value) * 20;
            mixer.SetFloat(MASTER_NAME, resultValue);
            PlayerPrefs.SetFloat(MASTER_NAME, resultValue);
        }

        public void ChangeMusic(float value)
        {
            float resultValue = Mathf.Log10(value == 0 ? 0.001f : value) * 20;
            mixer.SetFloat(MUSIC_NAME, resultValue);
            PlayerPrefs.SetFloat(MUSIC_NAME, resultValue);
        }

        public void ChangeSound(float value)
        {
            float resultValue = Mathf.Log10(value == 0 ? 0.001f : value) * 20;
            mixer.SetFloat(SOUND_NAME, resultValue);
            PlayerPrefs.SetFloat(SOUND_NAME, resultValue);
        }
    }
}
