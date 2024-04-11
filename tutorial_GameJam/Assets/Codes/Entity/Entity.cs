using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("�ִ� ü��")]
    [SerializeField] protected float max_Health;
    protected float health;
    [Header("�̵� �ӵ�")]
    public float speed;
    protected float timer;
    [Header("���� �ð�")]
    public float hitable_Time;
   

    public float Max_Health
    {
        get
        {
            return max_Health;
        }
        set
        {           
            max_Health = value;
            health = max_Health;
        }
    }

    private void Start()
    {
        health = max_Health;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    // �ǰ� �Լ�
    public virtual void TakeDamage(float damage)
    {
         // �����ð��� �ƴ� ��
         if (timer >= hitable_Time)
         {
            //Debug.Log($"�ǰ�!{damage} ��ŭ ��Ҵ�.");
            health -= damage;
            timer = 0f;
         }

        if (health <= 0)
        {
            Die();

        }
    }
  

    // ��� �Լ�
    public virtual void Die()
    {
        //Debug.Log("���!");
        gameObject.SetActive(false);
    }
}
