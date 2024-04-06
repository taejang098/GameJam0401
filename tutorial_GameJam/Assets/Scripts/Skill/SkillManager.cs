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
        //리스트 딕셔너리 초기화
        foreach (Define.SKILLTYPE st in System.Enum.GetValues(typeof(Define.SKILLTYPE)))
        {
            _skillTypeList.Add(st);
            _getSkill.Add(st, 0);
        }

        GameManager.Instance.player.LevelUpEvent += ShowSkill; //레벨업 이벤트 

        //[테스트용] 엔터키 누르면 스킬 선택 패널 활성화
        InputManager.Instance.AddKeyDownEvent(KeyCode.Return, () => { GameManager.Instance.player.LevelUpEvent(); });
        
    }


    private void ShowSkill() //스킬 버튼 생성 메서드
    {

        if(_skillTypeList.Count == 0)
        {
            return;
        }

        GameManager.Instance.PauseGame(true);
        skillPanel.ChangePopup(true);

        List<Define.SKILLTYPE> tempSkillTypes = _skillTypeList.ToList(); //스킬 타입 리스트 복사
        List<GameObject> curBtn = new List<GameObject>(); //현재 생성된 버튼

        for (int i = 0; i < Mathf.Min(3, _skillTypeList.Count); i++)
        {
            Define.SKILLTYPE tempType = tempSkillTypes[Random.Range(0, tempSkillTypes.Count)];
            tempSkillTypes.Remove(tempType); //중복 생성 막기

            //버튼생성
            GameObject temp = _factory.CreateSkillBtn();

            curBtn.Add(temp);

            //부모 오브젝트
            temp.transform.SetParent(point[i]);
            temp.transform.localPosition = Vector3.zero;

            SkillBtn btn = temp.GetComponent<SkillBtn>();
            btn.Info = _factory.GetSkillInfo(tempType); //스킬 정보 넣어주기
            btn.Init(() => 
            {
                SoundManager.Instance.Play(Define.AudioType.Sfx, "Sfx_SkillBtn");

                Skill skill = _factory.GetSkillType(tempType);
                skill.Execute();//스킬 실행

                CountUpSkill(tempType); //스킬 강화 카운트 증가

                foreach (GameObject b in curBtn)//생성된 버튼 제거
                {
                    Destroy(b);
                }

                //아이콘 존재 유무
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

    private GameObject FindCurIcon(Define.SKILLTYPE type)//현재 아이콘이 생성 되어 있는지 확인 후 리턴하는 함수
    {
    /*    if (skillInfoPanel.transform.childCount == 0)
        {
            return null;
        }*/

        return skillInfoPanel.transform.Find(type + "_Icon")?.gameObject;
    }

    private void CountUpSkill(Define.SKILLTYPE type) //강화 횟수 올리는 메서드
    {
        _getSkill[type]++;

        if (_getSkill[type] == 5)
        {
            RemoveSkill(type);
        }
    }

    private void RemoveSkill(Define.SKILLTYPE type) //최대 강화 시 딕셔너리에서 제거하는 함수
    {
        _skillTypeList.Remove(type);
    }
    
}
