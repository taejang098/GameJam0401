using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ������Ʈ ��Ȱ��ȭ 
            gameObject.SetActive(false);
            UseItem();
        }
    }
    public virtual void UseItem() { }
}
