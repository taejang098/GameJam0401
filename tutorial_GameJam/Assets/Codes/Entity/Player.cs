using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public Vector2 inputVec;

    Rigidbody2D rb;

    // �÷��̾� ����
    public float attack_Damage; // ���ݷ�
    public float bullet_Speed;  // ����ü �ӵ�
    public int level;           // ����
    public float exp_Amount;    // ����ġ ȹ�淮

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    // �̵� �Է� �Լ�
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // ���� �Է� �Լ�
    void OnFire(InputValue value)
    {
        Attack();
    }

    // ���� �Լ�
    public void Attack()
    {
        GameObject bullet = GameManager.Instance.object_Pool.Get(2);
        bullet.transform.position = rb.position;

        // ���콺 ��ġ �޾ƿ�
        Vector3 mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_Pos.z = 0f;

        // ���� ���
        Vector3 dir = (mouse_Pos - rb.transform.position).normalized;

        // ���� �߻� 
        Rigidbody2D bullet_RB = bullet.GetComponent<Rigidbody2D>();
        bullet_RB.velocity = dir * GameManager.Instance.player.bullet_Speed;
    }

    // �ǰ� �Լ�
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    // ��� �Լ�
    public override void Die()
    {
        base.Die();
    }

    // ����ġ ȹ�� �Լ�
    public void Get_Exp(float exp)
    {
        // ����ġ ȹ�� �� ������ �ý��� �ڵ� �ۼ��ϱ�.
    }

    // ���� �� �Լ�
    public void Level_Up()
    {
        Debug.Log("���� ��!");
        level++;
    }
}
