using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillIcon : MonoBehaviour
{

    public TextMeshProUGUI countText;
    public Image iconImage;

    private int _upgradeCount = 0;

    public int UpgradeCount 
    {
     
        set 
        {
            _upgradeCount = value;
            ShowUpgradeCount();
        } 
    }

    private void ShowUpgradeCount()
    {
        countText.text = _upgradeCount.ToString();
    }
}
