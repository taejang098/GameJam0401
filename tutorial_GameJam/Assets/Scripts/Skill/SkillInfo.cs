using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillInfo", menuName = "Scriptable Object/SkillInfo", order = int.MaxValue)]
public class SkillInfo : ScriptableObject
{
    public Sprite icon;
    public string nameStr;
    [TextArea(5,7)]
    public string infoStr;
 

}
