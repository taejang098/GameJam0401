using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FinishPopup : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI resultText;

    public Button homeBtn;

    public Sprite clearSprite;
    public Sprite gameOverSprite;
    private void Start()
    {
        
        if (GameManager.Instance.isGameOver)
        {
            GetComponent<Image>().sprite = gameOverSprite;
        }
        else if(GameManager.Instance.isGameClear)
        {
            GetComponent<Image>().sprite = clearSprite;
        }

        ShowInfo();

        homeBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Touch");
            SceneManager.LoadScene("MainScene");
            PopupManager.Instance.Close();
            Time.timeScale = 1;
        });

        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetDelay(1f).OnComplete(()=> { Time.timeScale = 0; });
    }



    private void ShowInfo()
    {
        levelText.text = "Lv." + GameManager.Instance.player.Level;
        infoText.text = "체력 : Lv." + GameManager.Instance.upgradeLevels[0] + System.Environment.NewLine + System.Environment.NewLine +
                        "공격력 : Lv." + GameManager.Instance.upgradeLevels[1] + System.Environment.NewLine + System.Environment.NewLine +
                        "공격 속도 : Lv." + GameManager.Instance.upgradeLevels[2] + System.Environment.NewLine + System.Environment.NewLine +
                        "이동 속도 : Lv." + GameManager.Instance.upgradeLevels[3];

        float finishTime = 15 * 60 - GameManager.Instance.gameTime;

        string minutes = Mathf.Floor(finishTime / 60).ToString("00");
        string seconds = Mathf.Floor(finishTime % 60).ToString("00");

        resultText.text = minutes+"분 "+seconds + "초 동안 " + GameManager.Instance.killCount + "마리 쓰러뜨렸어!";


    }



}
