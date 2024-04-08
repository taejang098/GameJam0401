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

        // destroy_Time��ŭ �����ٸ� ����ü Ǯ�� ��ȯ�ϱ�
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
        timer = 0; // Ÿ�̸� �ʱ�ȭ
    }
}
