using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        //GameManager.Instance.player.LevelUpEvent(); ��ų �˾� ����
    }


    private void ShowSkill() //��ų ��ư ���� �޼���
    {

        GameManager.Instance.PauseGame(true);
        skillPanel.ChangePopup(true);

        List<Define.SKILLTYPE> tempSkillTypes = _skillTypeList.ToList(); //��ų Ÿ�� ����Ʈ ����
        List<GameObject> curBtn = new List<GameObject>(); //���� ������ ��ư

        for (int i = 0; i < 3; i++)
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
                Skill skill = _factory.GetSkillType(tempType);
                skill.Execute();//��ų ����

                CountUpSkill(tempType); //��ų ��ȭ ī��Ʈ ����
                foreach (GameObject b in curBtn)//������ ��ư ����
                {
                    Destroy(b);
                }

                GameObject icon = _factory.CreateSkillIcon();
                icon.GetComponent<Image>().sprite = btn.Info.icon;
                icon.transform.SetParent(skillInfoPanel);

                GameManager.Instance.PauseGame(false);
                skillPanel.ChangePopup(false);
            }); 
        }
        

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
