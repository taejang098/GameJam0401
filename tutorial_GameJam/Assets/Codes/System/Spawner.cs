using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawn_Points;

    float timer;

    private void Awake()
    {
        spawn_Points = GetComponentsInChildren<Transform>(); // 본인 포함 자식 오브젝트의 <T>를 가져옴.
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
