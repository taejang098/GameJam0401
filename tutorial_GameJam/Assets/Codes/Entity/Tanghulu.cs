using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanghulu : Monster
{
    public List<Sprite> sprites = new List<Sprite>();

    private int _hitCount;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (_hitCount < sprites.Count - 1)
        {
            _hitCount++;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[_hitCount];
        }

        
    }

}
