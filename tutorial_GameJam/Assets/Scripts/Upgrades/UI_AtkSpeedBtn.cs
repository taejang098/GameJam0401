using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AtkSpeedBtn : UI_UpgradeBtn
{
    public override void Init()
    {
        InfoStr = $"��Ż���� ����� ��Ű�Դϴ�.{System.Environment.NewLine}���ݼӵ��� �ʴ� <color=red>{GameManager.Instance.player.AttackCount} + {GameManager.Instance.upgradeValues[2]}</color>��ŭ �����մϴ�.";
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
