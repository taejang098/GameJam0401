using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AtkBtn : UI_UpgradeBtn
{
    public override void Init()
    {
        InfoStr = $"식사용이 아닌 부드러운 간식용 머핀입니다.{System.Environment.NewLine}공격력이 <color=red>{GameManager.Instance.player.attack_Damage} + {GameManager.Instance.upgradeValues[1]}</color>만큼 증가합니다.";
        Level = GameManager.Instance.upgradeLevels[1];

        AddEvent(() => { Execute(); });
    }

    public override void Execute()
    {
        base.Execute();
        GameManager.Instance.upgradeLevels[1]++;
        GameManager.Instance.player.attack_Damage += GameManager.Instance.upgradeValues[1];
    }


}
