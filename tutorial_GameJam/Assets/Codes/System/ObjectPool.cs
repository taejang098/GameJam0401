using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public GameObject[] monsterPrefabs;

    List<GameObject>[] itemPools;
    List<GameObject>[] monsterPools;

    private void Awake()
    {
        itemPools = new List<GameObject>[itemPrefabs.Length];

        for (int i = 0;  i < itemPools.Length; i++)
        {
            itemPools[i] = new List<GameObject>();
        }

        monsterPools = new List<GameObject>[monsterPrefabs.Length];

        for (int i = 0; i < monsterPools.Length; i++)
        {
            monsterPools[i] = new List<GameObject>();
        }

      
    }

    private void Start()
    {
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                GameObject temp = Instantiate(itemPrefabs[i], transform);
                itemPools[i].Add(temp);
                temp.SetActive(false);
            }
        }

        for (int i = 0; i < monsterPrefabs.Length; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                GameObject temp = Instantiate(monsterPrefabs[i], transform);
                monsterPools[i].Add(temp);
                temp.SetActive(false);
            }
        }

    }

    public GameObject GetItem(int index)
    {
        GameObject select = null;

        foreach (GameObject item in itemPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                item.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(itemPrefabs[index], transform);
            itemPools[index].Add(select);
        }

        return select;
    }

    public GameObject GetMonster(int index)
    {
        GameObject select = null;

        foreach (GameObject item in monsterPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                item.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(monsterPrefabs[index], transform);
            monsterPools[index].Add(select);
        }

        return select;
    }
}
