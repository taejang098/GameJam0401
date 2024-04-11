using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [Header("UI")]
    public Button startBtn;
    public Button settingBtn;

    void Start()
    {
        InputManager.Instance.AddKeyDownEvent(KeyCode.Escape, () =>
        {
            if (PopupManager.Instance.GetLength() == 0)
            {
                Application.Quit();
            }
           
        });


        SoundManager.Instance.Play(Define.AudioType.Bgm, "Bgm_Main");

        startBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Touch");
            SceneManager.LoadScene("GameScene");
        });
        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Touch");
            PopupManager.Instance.Open(Define.PopupType.MainSetting_Popup,false);
        }); 


    }


    void Update()
    {
        
    }
}
