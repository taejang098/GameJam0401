using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AtkBtn : UI_UpgradeBtn
{
    public override void Init()
    {
        InfoStr = $"�Ļ���� �ƴ� �ε巯�� ���Ŀ� �����Դϴ�.{System.Environment.NewLine}���ݷ��� <color=red>{GameManager.Instance.player.attack_Damage} + {GameManager.Instance.upgradeValues[1]}</color>��ŭ �����մϴ�.";
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
