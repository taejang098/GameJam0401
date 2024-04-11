using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanghulu : Monster
{
    public List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

    
    }

}
