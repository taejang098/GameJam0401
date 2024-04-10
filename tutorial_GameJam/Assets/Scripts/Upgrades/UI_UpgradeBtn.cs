using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public abstract class UI_UpgradeBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    public TextMeshProUGUI _levelText;
    public TextMeshProUGUI _infoText;
    private Button _upgradeBtn;

    private int _level = 0;
    private string _infoStr;

    public int Level 
    {
        get 
        {
            return _level;
        }
        set 
        {
            _level = value;
            if (_level > 5)
            {
                _levelText.text = "Lv.Max";
                _upgradeBtn.enabled = false;
            }
            else
            {
                _levelText.text = "Lv." + _level;
            }
        }
    }

    public string InfoStr
    {
        get
        {
            return _infoStr;
        }
        set
        {
            _infoStr = value;
            UpdateStr();
        }
    }

    private void Start()
    {
        _upgradeBtn = GetComponent<Button>();
        Init();

    }

    public abstract void Init();
    public virtual void Execute()
    {
        Level++;
        UpdateStr();

        PopupManager.Instance.Close();
    }


    protected void AddEvent(Action action)
    {
        _upgradeBtn.onClick.AddListener(()=> 
        {
            SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_Touch");
            action?.Invoke(); 
        });
    }

    private void UpdateStr()
    {
        _infoText.text = $"{_infoStr}";
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f, 1.1f,1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
