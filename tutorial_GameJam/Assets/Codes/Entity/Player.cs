using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : Entity
{
   

    private Vector2 inputVec;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // 플레이어 스탯
    [Header("공격력")]
    public float attack_Damage;                  // 공격력
    [Header("공격 속도(초당 공격 횟수)")]
    [SerializeField] private float _attackCount = 1;
    private float attackDelay = 1.0f;             // 공격 속도(1초)
    private float attackTimer = 0f;
    private bool isMousePressed = false;
    [Header("투사체 속도")]
    public float bullet_Speed;                   // 투사체 속도
    private int _level = 1;                           // 레벨
    [SerializeField]
    private float current_Exp_Amount;            // 경험치 획득량
    private float exp_Amount_To_Level_Up;        // 레벨업에 필요한 경험치량
    [Header("초기 레벨업에 필요한 경험치량 (1이상)")]
    public float init_Exp_Amount_To_Level_Up;    // 초기 레벨업에 필요한 경험치량
    [Header("레벨업에 필요한 경험치량 증가 계수")]
    public float exp_Amount_increase_Value;      // 경험치 필요량 증가 계수

 
    public Action<int> LevelUpEvent;
    [Header("Others")]
    public Image hpbar;
    public ParticleSystem particleR;
    public ParticleSystem particleL;
    private bool moveR = true;

    public float AttackCount
    {
        get
        {
            return _attackCount;
        }
        set
        {
            _attackCount = value;

            attackDelay = 1 / _attackCount;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (init_Exp_Amount_To_Level_Up <= 0)
        {
            Debug.LogError("플레이어의 초기 레벨업에 필요한 경험치량을 1이상으로 설정해주세요.");
        }
        else
        {
            exp_Amount_To_Level_Up = init_Exp_Amount_To_Level_Up;
        }

        attackDelay = attackDelay / _attackCount;
    }

    private void FixedUpdate()
    {
        
    }

    private  void Update()
    {
        //base.Update();

        Vector2 nextVec = inputVec.normalized * speed;
        //rb.MovePosition(rb.position + nextVec);
        rb.velocity = nextVec;
        //transform.Translate(nextVec);

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            if (isMousePressed)
            {
                SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Shot");
                Attack();

                attackTimer = 0f;
            }
        }
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0)
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            direction = Vector3.Cross(Vector3.forward, direction);

            float value = Vector3.Dot(Vector3.up, direction);

            if (value > 0)
            {
                sr.flipX = true;
            }
            else if (value < 0)
            {
                sr.flipX = false;
            }
        }


        if (inputVec.x != 0)
        {

            if (inputVec.x > 0)
            {
                if (particleL.isStopped)
                {
                    particleL.Play();
                }
                if (!particleR.isStopped)
                {
                    particleR.Stop();
                }

                moveR = true;
            }
            else if(inputVec.x < 0)
            {
                if (particleR.isStopped)
                {
                    particleR.Play();
                }
                if (!particleL.isStopped)
                {
                    particleL.Stop();
                }

                moveR = false;
            }
           
        }

        if (Mathf.Abs(inputVec.x) > 0 || Mathf.Abs(inputVec.y) > 0)
        {
            anim.SetBool("isMove", true);
            if (moveR)
            {
                if (particleL.isStopped)
                {
                    particleL.Play();
                }
            }
            else
            {
                if (particleR.isStopped)
                {
                    particleR.Play();
                }
               
            }
        }
        else
        {
            anim.SetBool("isMove", false);
            if (!particleR.isStopped)
            {
                particleR.Stop();
            }
            if (!particleL.isStopped)
            {
                particleL.Stop();
            }

        }
    }

    // 이동 입력 함수
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    // 공격 입력 함수
    public void OnFirePress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isMousePressed = true;
        }
        else if (context.canceled)
        {
            isMousePressed = false;
        }

    }

    // 공격 함수
    public void Attack()
    {
        GameObject bullet = GameManager.Instance.object_Pool.GetItem(1);
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
        SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_GetDamage");

        base.TakeDamage(damage);

        ShowHpbar();

        if (health > 0)
        {
            StartCoroutine("NotTakeDamage");
        }
    }

    public void ShowHpbar()
    {
        hpbar.fillAmount = health / max_Health;
    }

    public void Knockback(GameObject obj, float knockbackForce)
    {

        Vector2 direction = (transform.position - obj.transform.position).normalized; // 충돌 대상과의 방향
        // 넉백 효과 적용
        transform.DOMove((Vector2)transform.position + direction, 0.2f);

    }

    private IEnumerator NotTakeDamage()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);

        yield return new WaitForSeconds(1.5f);

        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    // 사망 함수
    public override void Die()
    {
        base.Die();
        
    }

    public bool isDie()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    // 경험치 획득 함수
    public void Get_Exp(float exp)
    {
        // 경험치 획득
        current_Exp_Amount += exp;

        bool levelUp = false;

        // 레벨 업
        if (current_Exp_Amount >= exp_Amount_To_Level_Up)
        {
            current_Exp_Amount -= exp_Amount_To_Level_Up;
            exp_Amount_To_Level_Up *= exp_Amount_increase_Value; // 레벨업에 필요한 경험치량 증가
            init_Exp_Amount_To_Level_Up = exp_Amount_To_Level_Up;
            levelUp = true;

        }

        // UI 적용
        GameManager.Instance.ui_Manager.Set_Exp_Slider(current_Exp_Amount);

        if (levelUp)
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Open");

            _level++;

            PopupManager.Instance.Open(Define.PopupType.Upgrade_Popup,true);

            LevelUpEvent.Invoke(_level);
        }
    }
}

