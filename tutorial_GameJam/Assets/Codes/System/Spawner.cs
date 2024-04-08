using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    public Sprite sprite;
    public float health;
    public float damage;
    public float speed;
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

        if (timer > monster_Datas[GameManager.Instance.Stage].spawn_Time)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject monster = GameManager.Instance.object_Pool.Get(0);
        monster.transform.position = spawn_Points[UnityEngine.Random.Range(1, spawn_Points.Length)].position;
        monster.GetComponent<Monster>().Init(monster_Datas[GameManager.Instance.Stage]);
    }
}
