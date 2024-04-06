using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup settingsPanel;
  
    private void Start()
    {

        //SoundManager.Instance.Play(Define.AudioType.Bgm, "Bgm_GameBgm");

        InputManager.Instance.AddKeyDownEvent(KeyCode.Escape,() => 
        {
            CanvasGroup skillInfoPanel = GameObject.Find("SkillInfoPanel").GetComponent<CanvasGroup>();

            if (Time.timeScale != 0) {
                if (settingsPanel.GetBool())
                {
                    settingsPanel.ChangePopup(false);
                    GameManager.Instance.PauseGame(false);
                }
                else
                {
                    settingsPanel.ChangePopup(true);
                    GameManager.Instance.PauseGame(true);
                }

            }
            
        });
    }

    private void Update()
    {
        
    }
}
