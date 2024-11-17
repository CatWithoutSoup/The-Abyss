using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderMainMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogWarning("������� ��������� �� ��������!");
            return;
        }

        if (MusicController.instance != null)
        {
            volumeSlider.value = MusicController.instance.audioSource.volume;

            volumeSlider.onValueChanged.AddListener(MusicController.instance.SetVolume);
        }
        else
        {
            Debug.LogWarning("MusicController �� ������!");
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
