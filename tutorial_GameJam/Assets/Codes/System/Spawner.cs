using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    public Define.MonsterType type;
    public float spawn_Time;
}

public class Spawner : MonoBehaviour
{
    public Transform[] spawn_Points;
    [Header("���� ������")]
    public MonsterData[] monster_Datas;

    float timer;


    private void Awake()
    {
        spawn_Points = GetComponentsInChildren<Transform>(); // ���� ���� �ڽ� ������Ʈ�� <Transform>�� ������.
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (monster_Datas.Length <= GameManager.Instance.Stage)
        {
            return;
        }

        if (timer > monster_Datas[GameManager.Instance.Stage].spawn_Time)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
       
        GameObject monster = GameManager.Instance.GetMonster(monster_Datas[GameManager.Instance.Stage].type);
        monster.transform.position = spawn_Points[UnityEngine.Random.Range(1, spawn_Points.Length)].position;
        //monster.GetComponent<Monster>().Init(monster_Datas[GameManager.Instance.Stage]);
    }
}
