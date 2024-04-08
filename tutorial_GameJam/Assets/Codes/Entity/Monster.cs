using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    [Header("공격력")]
    public float attack_Damage;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // 피격 함수
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    // 사망 함수
    public override void Die()
    {
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_MonsterDie");

        base.Die();
        
        DropItem();
        
    }

    // 플레이어와 닿을 시 공격
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(attack_Damage);
        }
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void LateUpdate()
    {
        sr.flipX = GameManager.Instance.player.transform.position.x > rb.position.x;
    }

    // 플레이어를 쫓아가는 함수
    public void FollowPlayer()
    {
        Vector2 dir = GameManager.Instance.player.transform.position - rb.transform.position;
        Vector2 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    // 아이템 드롭 함수
    public void DropItem()
    {
        GameObject exp = GameManager.Instance.object_Pool.Get(1);
        exp.transform.position = rb.transform.position;
    }

    private void OnEnable()
    {
        health = max_Health;
        timer = 0f;
    }

    // 데이터 초기화 설정
    public void Init(MonsterData data)
    {
        sr.sprite = data.sprite;
        attack_Damage = data.damage;
        speed = data.speed;
        max_Health = data.health;
        health = data.health;
    }
}
