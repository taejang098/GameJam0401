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

    // �ǰ� �Լ�
    public virtual void TakeDamage(float damage)
    {
        // �����ð�
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

    // ��� �Լ�
    public virtual void Die()
    {
        Debug.Log("���!");
        gameObject.SetActive(false);
    }
}
