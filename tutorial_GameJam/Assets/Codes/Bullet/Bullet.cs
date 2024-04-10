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
            if (!GetComponent<Collider2D>().enabled)
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
                gameObject.transform.SetParent(FindObjectOfType<ObjectPool>().transform);
            }
            gameObject.SetActive(false);
        }
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Transform child = collision.transform.GetChild(0);
           
            rb.velocity = Vector2.zero;  
            GetComponent<Collider2D>().enabled = false;
            transform.position = (Vector2)collision.transform.position + Random.insideUnitCircle * 0.1f;
            transform.SetParent(child);

            if (0 >= collision.GetComponent<Monster>().Health - GameManager.Instance.player.attack_Damage)
            {
                
                for(int i = child.childCount - 1; i>=0; i--)
                {
                    child.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    child.GetChild(i).gameObject.SetActive(false);
                    child.GetChild(i).SetParent(FindObjectOfType<ObjectPool>().transform);
                    
                }
           
            }
            collision.GetComponent<Monster>().TakeDamage(GameManager.Instance.player.attack_Damage);
          
        }
    }

    private void OnEnable()
    {
        timer = 0; // Ÿ�̸� �ʱ�ȭ
    }
}
