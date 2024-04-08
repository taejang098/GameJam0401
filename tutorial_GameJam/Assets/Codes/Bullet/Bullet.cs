using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    float timer = 0;
    public float destroy_Time;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // destroy_Time만큼 지났다면 투사체 풀에 반환하기
        if (timer > destroy_Time)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamage(GameManager.Instance.player.attack_Damage);
        }
    }

    private void OnEnable()
    {
        timer = 0; // 타이머 초기화
    }
}
