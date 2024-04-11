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

    // �÷��̾� ����
    [Header("���ݷ�")]
    public float attack_Damage;                  // ���ݷ�
    [Header("���� �ӵ�(�ʴ� ���� Ƚ��)")]
    [SerializeField] private float _attackCount = 1;
    private float attackDelay = 1.0f;             // ���� �ӵ�(1��)
    private float attackTimer = 0f;
    private bool isMousePressed = false;
    [Header("����ü �ӵ�")]
    public float bullet_Speed;                   // ����ü �ӵ�
    private int _level = 1;                           // ����
    [SerializeField]
    private float current_Exp_Amount;            // ����ġ ȹ�淮
    private float exp_Amount_To_Level_Up;        // �������� �ʿ��� ����ġ��
    [Header("�ʱ� �������� �ʿ��� ����ġ�� (1�̻�)")]
    public float init_Exp_Amount_To_Level_Up;    // �ʱ� �������� �ʿ��� ����ġ��
    [Header("�������� �ʿ��� ����ġ�� ���� ���")]
    public float exp_Amount_increase_Value;      // ����ġ �ʿ䷮ ���� ���

 
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
            Debug.LogError("�÷��̾��� �ʱ� �������� �ʿ��� ����ġ���� 1�̻����� �������ּ���.");
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

    // �̵� �Է� �Լ�
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    // ���� �Է� �Լ�
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

    // ���� �Լ�
    public void Attack()
    {
        GameObject bullet = GameManager.Instance.object_Pool.GetItem(1);
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

        Vector2 direction = (transform.position - obj.transform.position).normalized; // �浹 ������ ����
        // �˹� ȿ�� ����
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

    // ��� �Լ�
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

    // ����ġ ȹ�� �Լ�
    public void Get_Exp(float exp)
    {
        // ����ġ ȹ��
        current_Exp_Amount += exp;

        bool levelUp = false;

        // ���� ��
        if (current_Exp_Amount >= exp_Amount_To_Level_Up)
        {
            current_Exp_Amount -= exp_Amount_To_Level_Up;
            exp_Amount_To_Level_Up *= exp_Amount_increase_Value; // �������� �ʿ��� ����ġ�� ����
            init_Exp_Amount_To_Level_Up = exp_Amount_To_Level_Up;
            levelUp = true;

        }

        // UI ����
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

