using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    private Dictionary<KeyCode, Action> _keyDownEvent = new Dictionary<KeyCode, Action>();

    private void Update()
    {
        foreach (KeyValuePair<KeyCode, Action> code in _keyDownEvent)
        {
            if (Input.GetKeyDown(code.Key))
            {
                code.Value?.Invoke();
            }
        }
    }


    public void AddKeyDownEvent(KeyCode keyCode, Action inputEvent)
    {
        if(_keyDownEvent.ContainsKey(keyCode))
        {
            _keyDownEvent[keyCode] = inputEvent;
            return;
        }
        _keyDownEvent.Add(keyCode, inputEvent);
    }
}
