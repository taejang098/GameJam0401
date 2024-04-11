using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HpBtn : UI_UpgradeBtn
{
    public override void Init()
    {
        InfoStr = $"딸기가 올라간 쇼트케이크 입니다.{System.Environment.NewLine}체력이 회복되고 최대 체력이 <color=red>{GameManager.Instance.upgradeValues[0]}</color>만큼 증가 합니다.";
        Level = GameManager.Instance.upgradeLevels[0];
        AddEvent(() => { Execute(); });
    }

    public override void Execute()
    {
        base.Execute();
        GameManager.Instance.upgradeLevels[0]++;

        GameManager.Instance.player.Max_Health += GameManager.Instance.upgradeValues[0];
        GameManager.Instance.player.ShowHpbar();
    }
}
