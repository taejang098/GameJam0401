using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : Item
{
    public float get_Exp_Amount; // °æÇèÄ¡ È¹µæ·®
    public override void UseItem()
    {
        GameManager.Instance.player.Get_Exp(get_Exp_Amount);
    }
}
