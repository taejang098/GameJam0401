using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{

    [Header("���ݷ�")]
    public float attack_Damage;
    private SpriteRenderer sr;
    public float Health { get { return health; }}

    private void Awake()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

        GameManager.Instance.killCount++;
    }

    // �÷��̾�� ���� �� ����

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(attack_Damage);
            collision.gameObject.GetComponent<Player>().Knockback(gameObject, speed*2);
        }
    }
 
    private void FixedUpdate()
    {
       

            FollowPlayer();
    }

    private void LateUpdate()
    {
        sr.flipX = GameManager.Instance.player.transform.position.x > transform.position.x;
    }

    // �÷��̾ �Ѿư��� �Լ�
    public void FollowPlayer()
    {
         Vector2 dir = GameManager.Instance.player.transform.position - transform.position;
         Vector2 nextVec = dir.normalized * speed * Time.deltaTime;
         transform.Translate(nextVec);

        /*_agent.SetDestination(GameManager.Instance.player.transform.position);*/
    }

    // ������ ��� �Լ�
    public void DropItem()
    {
        

        GameObject exp = GameManager.Instance.object_Pool.GetItem(0);
        exp.transform.position = transform.position;

    }

    private void OnEnable()
    {
        health = max_Health;
        timer = 0f;
    }


 
    // ������ �ʱ�ȭ ����
  /*  public void Init(MonsterData data)
    {
        sr.sprite = data.sprite;
        attack_Damage = data.damage;
        speed = data.speed;
        max_Health = data.health;
        health = data.health;
    }*/
}
