using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Yakgwa : Monster
{
    public List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        float value = GameManager.Instance.player.transform.position.x - transform.position.x;

        

        transform.GetChild(0).Rotate(new Vector3(0,0, -Mathf.Sign(value)/2));
    }
}
