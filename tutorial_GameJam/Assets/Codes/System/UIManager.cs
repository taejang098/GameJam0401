using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image exp_Slider;   // ����ġ �����̴�

    public void Set_Exp_Slider(float exp)
    {
        exp_Slider.fillAmount = exp / GameManager.Instance.player.init_Exp_Amount_To_Level_Up;
    }
}
