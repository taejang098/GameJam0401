using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPopup : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Button closeBtn;

    public TextMeshProUGUI bgmValueText;
    public TextMeshProUGUI sfxValueText;

    private void Start()
    {
        closeBtn.onClick.AddListener(() => 
        {
            transform.parent.GetComponent<CanvasGroup>().ChangePopup(false);
            GameManager.Instance.PauseGame(false);
        });

        bgmSlider.value = 0.5f;
        sfxSlider.value = 0.5f;

        bgmValueText.text = "50";
        sfxValueText.text = "50";

        //ScrollBar
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetVolume(Define.AudioType.Bgm, value);
            bgmValueText.text = (bgmSlider.value * 100).ToString("N0");

        });
        sfxSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetVolume(Define.AudioType.Sfx, value);
            sfxValueText.text = (sfxSlider.value * 100).ToString("N0");
        });
    }
}
