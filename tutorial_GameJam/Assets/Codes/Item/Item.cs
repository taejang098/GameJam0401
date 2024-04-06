using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_GetItem");

            // 오브젝트 비활성화 
            gameObject.SetActive(false);
            UseItem();
        }
    }
    public virtual void UseItem() { }
}
