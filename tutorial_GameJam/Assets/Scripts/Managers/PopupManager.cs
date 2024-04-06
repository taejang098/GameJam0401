using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopupManager 
{ 
    public static void ChangePopup(this CanvasGroup canvasGroup, bool value)
    {
        if (value)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public static bool GetBool(this CanvasGroup canvasGroup)
    {
        return canvasGroup.interactable;
    }

 
}
