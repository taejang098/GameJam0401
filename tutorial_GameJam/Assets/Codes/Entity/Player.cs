using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public Vector2 inputVec;

    Rigidbody2D rb;

    // 플레이어 스탯
    public float attack_Damage; // 공격력
    public float bullet_Speed;  // 투사체 속도
    public int level;           // 레벨
    public float exp_Amount;    // 경험치 획득량

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    // 이동 입력 함수
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // 공격 입력 함수
    void OnFire(InputValue value)
    {
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
    }

    // 경험치 획득 함수
    public void Get_Exp(float exp)
    {
        // 경험치 획득 및 레벨업 시스템 코드 작성하기.
    }

    // 레벨 업 함수
    public void Level_Up()
    {
        Debug.Log("레벨 업!");
        level++;
    }
}
