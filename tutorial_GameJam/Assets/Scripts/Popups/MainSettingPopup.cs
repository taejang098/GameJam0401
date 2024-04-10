using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSettingPopup : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;


    public Button closeBtn;

    void Start()
    {
        bgmSlider.value = SoundManager.Instance.GetVolume(Define.AudioType.Bgm);
        sfxSlider.value = SoundManager.Instance.GetVolume(Define.AudioType.Sfx);


        //ScrollBar
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetVolume(Define.AudioType.Bgm, value);

        });
        sfxSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetVolume(Define.AudioType.Sfx, value);
        });


        closeBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Touch");
            PopupManager.Instance.Close();
        });
    }

  
}
