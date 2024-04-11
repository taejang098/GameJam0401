using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SpeedBtn : UI_UpgradeBtn
{

    public override void Init()
    {
        InfoStr = $"시원한 아이스 아메리카노입니다.{System.Environment.NewLine}이동속도가 <color=red>{GameManager.Instance.upgradeValues[3]}</color>만큼 증가합니다.";
        Level = GameManager.Instance.upgradeLevels[3];
        AddEvent(() => { Execute(); });
    }

    public override void Execute()
    {
        base.Execute();
        GameManager.Instance.upgradeLevels[3]++;
        GameManager.Instance.player.speed += GameManager.Instance.upgradeValues[3];
    }
}
