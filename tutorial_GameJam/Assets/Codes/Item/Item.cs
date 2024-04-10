using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_GetItem");

            // ������Ʈ ��Ȱ��ȭ 
            Vector2 playerPos = collision.transform.position;
            DOTween.Sequence().Append(transform.DOMove(playerPos, 0.1f)).OnUpdate(() =>
            {
                playerPos = collision.transform.position;
            }).OnComplete(() =>
            {
                gameObject.SetActive(false);
                UseItem();
            });

        }
    }
    public virtual void UseItem() { }
}
