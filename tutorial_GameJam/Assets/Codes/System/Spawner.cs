using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawn_Points;

    float timer;

    private void Awake()
    {
        spawn_Points = GetComponentsInChildren<Transform>(); // ���� ���� �ڽ� ������Ʈ�� <T>�� ������.
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject monster = GameManager.Instance.object_Pool.Get(0);
        monster.transform.position = spawn_Points[Random.Range(1, spawn_Points.Length)].position;
    }
}
