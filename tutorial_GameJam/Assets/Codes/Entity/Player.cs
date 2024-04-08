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

    // 플레이어 스탯
    [Header("공격력")]
    public float attack_Damage;                  // 공격력
    [Header("투사체 속도")]
    public float bullet_Speed;                   // 투사체 속도
    private int level;                           // 레벨
    [SerializeField]
    private float current_Exp_Amount;            // 경험치 획득량
    private float exp_Amount_To_Level_Up;        // 레벨업에 필요한 경험치량
    [Header("초기 레벨업에 필요한 경험치량 (1이상)")]
    public float init_Exp_Amount_To_Level_Up;    // 초기 레벨업에 필요한 경험치량
    [Header("레벨업에 필요한 경험치량 증가 계수")]
    public float exp_Amount_increase_Value;      // 경험치 필요량 증가 계수

    public Action LevelUpEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        LevelUpEvent += Level_Up;

        if (init_Exp_Amount_To_Level_Up <= 0)
        {
            Debug.LogError("플레이어의 초기 레벨업에 필요한 경험치량을 1이상으로 설정해주세요.");
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

    // 이동 입력 함수
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // 공격 입력 함수
    void OnFire(InputValue value)
    {
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Shot");
        Attack();
    }

    // 공격 함수
    public void Attack()
    {
        GameObject bullet = GameManager.Instance.object_Pool.Get(2);
        bullet.transform.position = rb.position;

        // 마우스 위치 받아옴
        Vector3 mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_Pos.z = 0f;

        // 방향 계산
        Vector3 dir = (mouse_Pos - rb.transform.position).normalized;

        // 물리 발사 
        Rigidbody2D bullet_RB = bullet.GetComponent<Rigidbody2D>();
        bullet_RB.velocity = dir * GameManager.Instance.player.bullet_Speed;
    }

    // 피격 함수
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    // 사망 함수
    public override void Die()
    {
        base.Die();
        GameManager.Instance.PauseGame(true);
    }

    // 경험치 획득 함수
    public void Get_Exp(float exp)
    {
        // 경험치 획득
        current_Exp_Amount += exp;

        // 레벨 업
        while (current_Exp_Amount >= exp_Amount_To_Level_Up)
        {
            current_Exp_Amount -= exp_Amount_To_Level_Up;
            exp_Amount_To_Level_Up *= exp_Amount_increase_Value; // 레벨업에 필요한 경험치량 증가
            Level_Up();
        }

        // UI 적용
        GameManager.Instance.ui_Manager.Set_Exp_Slider(current_Exp_Amount);
    }

    // 레벨 업 함수
    private void Level_Up()
    {
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Open");
        GameManager.Instance.skill_Manager.ShowSkill();
        level++;
        
    }
}
