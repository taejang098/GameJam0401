using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [Header("���� ������Ʈ")]
    public Player player;
    public ObjectPool object_Pool;
    public SkillManager skill_Manager;
    public UIManager ui_Manager;

    [Header("���� �ý���")]
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
    [Header("�������� �ٲ�� �ð�(s)")]
    public float[] next_Stage_Time;

    private void Update()
    {
        // ��������(���̵�) ���� �ý���
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
