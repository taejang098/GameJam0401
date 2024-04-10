using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoppang : Monster
{
    public List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

}
