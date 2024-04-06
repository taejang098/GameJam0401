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
        //리스트 딕셔너리 초기화
        foreach (Define.SKILLTYPE st in System.Enum.GetValues(typeof(Define.SKILLTYPE)))
        {
            _skillTypeList.Add(st);
            _getSkill.Add(st, 0);
        }

        GameManager.Instance.player.LevelUpEvent += ShowSkill; //레벨업 이벤트 

        //GameManager.Instance.player.LevelUpEvent(); 스킬 팝업 실행
    }


    private void ShowSkill() //스킬 버튼 생성 메서드
    {

        GameManager.Instance.PauseGame(true);
        skillPanel.ChangePopup(true);

        List<Define.SKILLTYPE> tempSkillTypes = _skillTypeList.ToList(); //스킬 타입 리스트 복사
        List<GameObject> curBtn = new List<GameObject>(); //현재 생성된 버튼

        for (int i = 0; i < 3; i++)
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
                Skill skill = _factory.GetSkillType(tempType);
                skill.Execute();//스킬 실행

                CountUpSkill(tempType); //스킬 강화 카운트 증가
                foreach (GameObject b in curBtn)//생성된 버튼 제거
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
