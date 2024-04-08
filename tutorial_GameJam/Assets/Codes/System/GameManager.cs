using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [Header("게임 오브젝트")]
    public Player player;
    public ObjectPool object_Pool;
    public SkillManager skill_Manager;
    public UIManager ui_Manager;

    [Header("게임 시스템")]
    private int stage;
    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
        }
    }

    private float timer;
    [Header("스테이지 바뀌는 시간(s)")]
    public float[] next_Stage_Time;

    private void Update()
    {
        // 스테이지(난이도) 변경 시스템
        timer += Time.deltaTime;
        if (timer > next_Stage_Time[Mathf.Min(Stage, next_Stage_Time.Length)])
        {
            stage++;
            timer = 0;
        }
    }

    public void PauseGame(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            player.GetComponent<PlayerInput>().enabled = true;
        }
       
    }

}
