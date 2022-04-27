using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Toggle master;
    [SerializeField] private Toggle music;

    private void Start()
    {
        master.onValueChanged.AddListener(SetMasterVolume);
        music.onValueChanged.AddListener(SetMusicVolume);

        master.isOn = PlayerPrefs.GetInt("MasterVolume") == 1 ? true : false;
        music.isOn = PlayerPrefs.GetInt("MusicVolume") == 1 ? true : false;
        SetMasterVolume(master.isOn);
        SetMusicVolume(music.isOn);
    }

    private void SetMasterVolume(bool flag)
    {
        mixer.audioMixer.SetFloat("MasterVolume",flag? 0:-80);
        PlayerPrefs.SetInt("MasterVolume",flag? 1:0);
    }
    
    private void SetMusicVolume(bool flag)
    {
        mixer.audioMixer.SetFloat("MusicVolume",flag? -15:-80);
        PlayerPrefs.SetInt("MusicVolume",flag? 1:0);
    }
}
