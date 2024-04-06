using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public List<Transform> point = new List<Transform>();

    private Dictionary<Define.SKILLTYPE, int> _getSkill = new Dictionary<Define.SKILLTYPE, int>();

    private SkillFactory _factory = new SkillFactory();
    private List<Define.SKILLTYPE> _skillTypeList = new List<Define.SKILLTYPE>();

    [Header("UI")]
    public CanvasGroup skillPanel;
    public Transform skillInfoPanel;

    void Start()
    {
        //����Ʈ ��ųʸ� �ʱ�ȭ
        foreach (Define.SKILLTYPE st in System.Enum.GetValues(typeof(Define.SKILLTYPE)))
        {
            _skillTypeList.Add(st);
            _getSkill.Add(st, 0);
        }

        GameManager.Instance.player.LevelUpEvent += ShowSkill; //������ �̺�Ʈ 

        //[�׽�Ʈ��] ����Ű ������ ��ų ���� �г� Ȱ��ȭ
        InputManager.Instance.AddKeyDownEvent(KeyCode.Return, () => { GameManager.Instance.player.LevelUpEvent(); });
        
    }


    private void ShowSkill() //��ų ��ư ���� �޼���
    {

        if(_skillTypeList.Count == 0)
        {
            return;
        }

        GameManager.Instance.PauseGame(true);
        skillPanel.ChangePopup(true);

        List<Define.SKILLTYPE> tempSkillTypes = _skillTypeList.ToList(); //��ų Ÿ�� ����Ʈ ����
        List<GameObject> curBtn = new List<GameObject>(); //���� ������ ��ư

        for (int i = 0; i < Mathf.Min(3, _skillTypeList.Count); i++)
        {
            Define.SKILLTYPE tempType = tempSkillTypes[Random.Range(0, tempSkillTypes.Count)];
            tempSkillTypes.Remove(tempType); //�ߺ� ���� ����

            //��ư����
            GameObject temp = _factory.CreateSkillBtn();

            curBtn.Add(temp);

            //�θ� ������Ʈ
            temp.transform.SetParent(point[i]);
            temp.transform.localPosition = Vector3.zero;

            SkillBtn btn = temp.GetComponent<SkillBtn>();
            btn.Info = _factory.GetSkillInfo(tempType); //��ų ���� �־��ֱ�
            btn.Init(() => 
            {
                SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_SkillBtn");

                Skill skill = _factory.GetSkillType(tempType);
                skill.Execute();//��ų ����

                CountUpSkill(tempType); //��ų ��ȭ ī��Ʈ ����

                foreach (GameObject b in curBtn)//������ ��ư ����
                {
                    Destroy(b);
                }

                //������ ���� ����
                if (FindCurIcon(tempType) != null)
                {
                    FindCurIcon(tempType).GetComponent<SkillIcon>().UpgradeCount = _getSkill[tempType];
                }
                else
                {
                    GameObject iconObj = _factory.CreateSkillIcon();
                    SkillIcon icon = iconObj.GetComponent<SkillIcon>();

                    icon.UpgradeCount = _getSkill[tempType];
                    icon.iconImage.sprite = btn.Info.icon;

                    iconObj.name = tempType + "_Icon";
                    iconObj.transform.SetParent(skillInfoPanel);
                }
   

                GameManager.Instance.PauseGame(false);
                skillPanel.ChangePopup(false);

            }); 
        }
        

    }

    private GameObject FindCurIcon(Define.SKILLTYPE type)//���� �������� ���� �Ǿ� �ִ��� Ȯ�� �� �����ϴ� �Լ�
    {
    /*    if (skillInfoPanel.transform.childCount == 0)
        {
            return null;
        }*/

        return skillInfoPanel.transform.Find(type + "_Icon")?.gameObject;
    }

    private void CountUpSkill(Define.SKILLTYPE type) //��ȭ Ƚ�� �ø��� �޼���
    {
        _getSkill[type]++;

        if (_getSkill[type] == 5)
        {
            RemoveSkill(type);
        }
    }

    private void RemoveSkill(Define.SKILLTYPE type) //�ִ� ��ȭ �� ��ųʸ����� �����ϴ� �Լ�
    {
        _skillTypeList.Remove(type);
    }
    
}
