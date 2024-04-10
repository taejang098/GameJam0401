using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;
public class SettingsPopup : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI infoText;

    private void Start()
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

        ShowInfo();

       
    }

    private void ShowInfo()
    {
        levelText.text = "Lv."+GameManager.Instance.player.Level;
        infoText.text = "ü�� : Lv." + GameManager.Instance.upgradeLevels[0] + System.Environment.NewLine + System.Environment.NewLine +
                        "���ݷ� : Lv." + GameManager.Instance.upgradeLevels[1] + System.Environment.NewLine + System.Environment.NewLine +
                        "���� �ӵ� : Lv." + GameManager.Instance.upgradeLevels[2] + System.Environment.NewLine + System.Environment.NewLine +
                        "�̵� �ӵ� : Lv." + GameManager.Instance.upgradeLevels[3];

    }
}
