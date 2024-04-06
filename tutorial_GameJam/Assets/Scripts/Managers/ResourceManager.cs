using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        var resource = Resources.Load<T>($"{path}");

        if (resource != null)
        {
            return resource;
        }

        Debug.Log($"Failed to load prefab : {path}");
        return null;

    }

    //�������� ���ִ� �Լ�
    public static T CreatePrefab<T>(string path, Vector3 pos = new Vector3(), Quaternion quaternion = new Quaternion(), Transform parent = null) where T : UnityEngine.Object
    {
        var resource = Resources.Load<T>($"Prefab/{path}");

        if (resource == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }


        var obj = UnityEngine.Object.Instantiate(resource, Vector3.zero, quaternion, parent);

        //������Ʈ ������ Clone�� �����ִ� �ڵ�
        var index = obj.name.IndexOf("(Clone)", StringComparison.Ordinal);
        if (index > 0)
            obj.name = obj.name[..index];

        return obj;
    }

    public static List<T> LoadAll<T>(string path) where T : UnityEngine.Object
    {
        var resource = Resources.LoadAll<T>($"{path}");

        if (resource.Length > 0)
        {
            return resource.ToList();
        }
        Debug.Log($"Failed to load prefab : {path}");
        return null;

    }
}
