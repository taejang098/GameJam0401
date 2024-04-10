using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScene : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI playTimerText;
    private void Start()
    {
        SoundManager.Instance.Play(Define.AudioType.Bgm, "Bgm_Game");


        InputManager.Instance.AddKeyDownEvent(KeyCode.Escape,() => 
        {
            if (PopupManager.Instance.GetLength() == 0)
            {
                PopupManager.Instance.Open(Define.PopupType.Setting_Popup, true);
            }
            else
            {
                if (PopupManager.Instance.GetPeek()?.name == "Upgrade_Popup")
                {
                    return;
                }


                PopupManager.Instance.Close();
            }
            
        });

        GameManager.Instance.player.LevelUpEvent += ShowLevel;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isGameClear)
        {
            return;
        }

        if (GameManager.Instance.player.isDie())
        {
            PopupManager.Instance.Open(Define.PopupType.FinishGame_Popup, false);
            GameManager.Instance.isGameOver = true;
            return;
        }

        if (GameManager.Instance.gameTime > 0)
        {
            GameManager.Instance.gameTime -= Time.deltaTime; // deltaTime을 이용하여 시간 감소
            string minutes = Mathf.Floor(GameManager.Instance.gameTime / 60).ToString("00");
            string seconds = Mathf.Floor(GameManager.Instance.gameTime % 60).ToString("00");
            playTimerText.text = minutes + ":" + seconds;
        }
        else
        {
            playTimerText.text = "00:00";

            PopupManager.Instance.Open(Define.PopupType.FinishGame_Popup, false);
            GameManager.Instance.isGameClear = true;
        }
    }

    private void ShowLevel(int level)
    {
        levelText.text = "Lv." + level.ToString();
    }
}
