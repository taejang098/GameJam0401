using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HpBtn : UI_UpgradeBtn
{
    public override void Init()
    {
        InfoStr = $"���Ⱑ �ö� ��Ʈ����ũ �Դϴ�.{System.Environment.NewLine}ü���� ȸ���ǰ� �ִ� ü���� <color=red>{GameManager.Instance.upgradeValues[0]}</color>��ŭ ���� �մϴ�.";
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
