using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("���� ������Ʈ")]
    public Player player;
    public ObjectPool object_Pool;
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
    [Header("���׷��̵� ����(Hp, Atk, AtkSpeed, speed)")]
    public List<int> upgradeLevels = new List<int>() {1,1,1,1};
    [Header("���׷��̵� ������(Hp, Atk, AtkSpeed, speed)")]
    public List<float> upgradeValues = new List<float>() {1,1,1,1};


    public float gameTime = 15 * 60;
    [HideInInspector]
    public int killCount = 0;

    [HideInInspector]
    public bool isGameOver = false;
    [HideInInspector]
    public bool isGameClear = false;
    private void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // ��������(���̵�) ���� �ý���
        int index = Mathf.Min(Stage, next_Stage_Time.Length);

        if (next_Stage_Time.Length <= index)
        {
            return;
        }

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

    public GameObject GetMonster(Define.MonsterType type)
    {
        return object_Pool.GetMonster((int)type);
    }
}
