using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : Item
{
    public float get_Exp_Amount; // ����ġ ȹ�淮
    public override void UseItem()
    {
        GameManager.Instance.player.Get_Exp(get_Exp_Amount);
    }
}
