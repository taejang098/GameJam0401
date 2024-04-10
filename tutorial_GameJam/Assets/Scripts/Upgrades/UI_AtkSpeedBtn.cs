using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AtkSpeedBtn : UI_UpgradeBtn
{
    public override void Init()
    {
        InfoStr = $"이탈리아 기원의 쿠키입니다.{System.Environment.NewLine}공격속도가 초당 <color=red>{GameManager.Instance.player.AttackCount} + {GameManager.Instance.upgradeValues[2]}</color>만큼 공격합니다.";
        Level = GameManager.Instance.upgradeLevels[2];
        AddEvent(() => { Execute(); });
    }

    public override void Execute()
    {
        base.Execute();
        GameManager.Instance.upgradeLevels[2]++;
        GameManager.Instance.player.AttackCount += GameManager.Instance.upgradeValues[2];
    }
}
