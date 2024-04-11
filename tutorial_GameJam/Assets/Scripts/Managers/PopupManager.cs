using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PopupManager : Singleton<PopupManager>
{
    private Stack<GameObject> _popups = new Stack<GameObject>();

    private bool _isPause;
    public void Open(Define.PopupType type, bool isPause)
    {

        GameObject popup = ResourceManager.CreatePrefab<GameObject>($"Popup/{type}");
        popup.transform.SetParent(GameObject.Find("MainCanvas").transform);
        popup.transform.localPosition = Vector2.zero;
        popup.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        popup.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        _popups.Push(popup);

        popup.GetComponent<CanvasGroup>().alpha = 0;

        popup.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(() => 
        {
            if (isPause) 
            {
                GameManager.Instance.PauseGame(true);
                _isPause = true;
            }
        });
    }

    public void Close()
    {
        GameObject temp = _popups.Pop();

        if (temp == null)
        {
            return;
        }

        if (_isPause)
        {
            GameManager.Instance.PauseGame(false);
            _isPause = false;
        }

        temp.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(()=> { Destroy(temp); });
    }

    public int GetLength()
    {
        return _popups.Count;
    }

    public GameObject GetPeek()
    {
        return _popups.Peek();
    }


}
