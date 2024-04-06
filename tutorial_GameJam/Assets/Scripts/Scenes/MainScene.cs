using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [Header("UI")]
    public Button startBtn;

    void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        }); 
    }


    void Update()
    {
        
    }
}
