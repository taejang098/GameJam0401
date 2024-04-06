using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public Player player;
    public ObjectPool object_Pool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }


    public void PauseGame(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            player.GetComponent<PlayerInput>().enabled = true;
        }
       
    }

}
