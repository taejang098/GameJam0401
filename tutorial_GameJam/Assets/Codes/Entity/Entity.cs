using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float max_Health;
    float health;
    public float speed;
    float timer;

    private void Awake()
    {
        health = max_Health;
    }

    // 피격 함수
    public virtual void TakeDamage(float damage)
    {
        // 무적시간
         //if (timer > )
         //{
            health -= damage;
         //   timer = 0;
         //}

        if (health < 0)
        {
            Die();
        }
    }

    // 사망 함수
    public virtual void Die()
    {
        Debug.Log("사망!");
        gameObject.SetActive(false);
    }
}
