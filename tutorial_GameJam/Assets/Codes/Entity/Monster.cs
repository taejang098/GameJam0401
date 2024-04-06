using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public float attack_Damage;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // �ǰ� �Լ�
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    // ��� �Լ�
    public override void Die()
    {
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_MonsterDie");

        base.Die();
        
        DropItem();
        
    }

    // �÷��̾�� ���� �� ����
    private void OnCollisionEnter2D(Collision2D collision)
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

    // �÷��̾ �Ѿư��� �Լ�
    public void FollowPlayer()
    {
        Vector2 dir = GameManager.Instance.player.transform.position - rb.transform.position;
        Vector2 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    // ������ ��� �Լ�
    public void DropItem()
    {
        GameObject exp = GameManager.Instance.object_Pool.Get(1);
        exp.transform.position = rb.transform.position;
    }
}
