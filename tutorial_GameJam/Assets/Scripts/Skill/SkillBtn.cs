using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBtn : MonoBehaviour
{

    [Header("UI")]
    public Image iconImage;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI nameText;

    public SkillInfo Info { get; set; }
    public virtual void Init(Action btnEvent = null)
    {

        iconImage.sprite = Info.icon;
        infoText.text = Info.infoStr;
        nameText.text = Info.nameStr;

        GetComponent<Button>().onClick.AddListener(() => 
        {
            btnEvent?.Invoke();
        });
    }
}
