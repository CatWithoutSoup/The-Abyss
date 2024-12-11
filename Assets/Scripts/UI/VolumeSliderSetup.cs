using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSliderSetup : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    void Start()
    {
        MusicController musicController = MusicController.instance;
        if (musicController != null && volumeSlider != null)
        {
            volumeSlider.value = musicController.audioSource.volume;
            volumeSlider.onValueChanged.AddListener(musicController.SetVolume);
        }
        else
        {
            Debug.Log("Всё плохо...");
        }
    }
    private void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
        }
    }

}
