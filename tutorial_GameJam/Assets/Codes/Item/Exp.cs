using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Exp : Item
{
    public float get_Exp_Amount; // °æÇèÄ¡ È¹µæ·®

    public Sprite[] sprites;
    private void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public override void UseItem()
    {
        GameManager.Instance.player.Get_Exp(get_Exp_Amount);
    }

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(60);

        gameObject.SetActive(false);
    }
}
