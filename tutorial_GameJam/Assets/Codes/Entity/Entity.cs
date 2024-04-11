using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("최대 체력")]
    [SerializeField] protected float max_Health;
    protected float health;
    [Header("이동 속도")]
    public float speed;
    protected float timer;
    [Header("무적 시간")]
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

    // 피격 함수
    public virtual void TakeDamage(float damage)
    {
         // 무적시간이 아닐 때
         if (timer >= hitable_Time)
         {
            //Debug.Log($"피격!{damage} 만큼 닳았다.");
            health -= damage;
            timer = 0f;
         }

        if (health <= 0)
        {
            Die();

        }
    }
  

    // 사망 함수
    public virtual void Die()
    {
        //Debug.Log("사망!");
        gameObject.SetActive(false);
    }
}
