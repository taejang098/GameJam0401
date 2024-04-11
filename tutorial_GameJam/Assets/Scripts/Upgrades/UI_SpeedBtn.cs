using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SpeedBtn : UI_UpgradeBtn
{

    public override void Init()
    {
        InfoStr = $"�ÿ��� ���̽� �Ƹ޸�ī���Դϴ�.{System.Environment.NewLine}�̵��ӵ��� <color=red>{GameManager.Instance.upgradeValues[3]}</color>��ŭ �����մϴ�.";
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
