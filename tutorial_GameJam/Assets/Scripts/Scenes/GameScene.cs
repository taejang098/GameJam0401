using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup optionPanel;
  
    private void Start()
    {
        InputManager.Instance.AddKeyDownEvent(KeyCode.Escape,() => 
        {
            if (optionPanel.GetBool())
            {
                optionPanel.ChangePopup(false);
                GameManager.Instance.PauseGame(false);
            }
            else
            {
                optionPanel.ChangePopup(true);
                GameManager.Instance.PauseGame(true);
            }
            
            
        });
    }

    private void Update()
    {
        
    }
}
