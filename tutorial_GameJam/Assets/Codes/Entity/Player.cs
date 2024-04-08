using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    private Vector2 inputVec;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // �÷��̾� ����
    [Header("���ݷ�")]
    public float attack_Damage;                  // ���ݷ�
    [Header("����ü �ӵ�")]
    public float bullet_Speed;                   // ����ü �ӵ�
    private int level;                           // ����
    [SerializeField]
    private float current_Exp_Amount;            // ����ġ ȹ�淮
    private float exp_Amount_To_Level_Up;        // �������� �ʿ��� ����ġ��
    [Header("�ʱ� �������� �ʿ��� ����ġ�� (1�̻�)")]
    public float init_Exp_Amount_To_Level_Up;    // �ʱ� �������� �ʿ��� ����ġ��
    [Header("�������� �ʿ��� ����ġ�� ���� ���")]
    public float exp_Amount_increase_Value;      // ����ġ �ʿ䷮ ���� ���

    public Action LevelUpEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        LevelUpEvent += Level_Up;

        if (init_Exp_Amount_To_Level_Up <= 0)
        {
            Debug.LogError("�÷��̾��� �ʱ� �������� �ʿ��� ����ġ���� 1�̻����� �������ּ���.");
        }
        else
        {
            exp_Amount_To_Level_Up = init_Exp_Amount_To_Level_Up;
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    private void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            sr.flipX = inputVec.x > 0;
        }

        if (Mathf.Abs(inputVec.x) > 0 || Mathf.Abs(inputVec.y) > 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }

    // �̵� �Է� �Լ�
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // ���� �Է� �Լ�
    void OnFire(InputValue value)
    {
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Shot");
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
        GameManager.Instance.PauseGame(true);
    }

    // ����ġ ȹ�� �Լ�
    public void Get_Exp(float exp)
    {
        // ����ġ ȹ��
        current_Exp_Amount += exp;

        // ���� ��
        while (current_Exp_Amount >= exp_Amount_To_Level_Up)
        {
            current_Exp_Amount -= exp_Amount_To_Level_Up;
            exp_Amount_To_Level_Up *= exp_Amount_increase_Value; // �������� �ʿ��� ����ġ�� ����
            Level_Up();
        }

        // UI ����
        GameManager.Instance.ui_Manager.Set_Exp_Slider(current_Exp_Amount);
    }

    // ���� �� �Լ�
    private void Level_Up()
    {
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Open");
        GameManager.Instance.skill_Manager.ShowSkill();
        level++;
        
    }
}
