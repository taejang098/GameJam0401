using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory 
{

/*    public List<GameObject> InitSkillBtn()
    {
        return ResourceManager.LoadAll<GameObject>("Prefab/Skill");
    }
*/
    public GameObject CreateSkillBtn()
    {
        return ResourceManager.CreatePrefab<GameObject>($"Skill/SkillBtn");
    }
    public GameObject CreateSkillIcon()
    {
        return ResourceManager.CreatePrefab<GameObject>($"Skill/SkillIcon");
    }


    public SkillInfo GetSkillInfo(Define.SKILLTYPE type)
    {
        return ResourceManager.Load<SkillInfo>($"ScriptableObject/Info_{type}");
    }

    public Skill GetSkillType(Define.SKILLTYPE type)
    {
        switch (type)
        {
            case Define.SKILLTYPE.Hp_Upgrade:
                return new Hp_Upgrade();
            case Define.SKILLTYPE.Speed_Upgrade:
                return new Speed_Upgrade();
            case Define.SKILLTYPE.Atk_Upgrade:
                return new Atk_Upgrade();
            case Define.SKILLTYPE.AtkSpeed_Upgrade:
                return new AtkSpeed_Upgrade();
        }

        return null;
    }
}
