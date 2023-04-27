using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static GameObject Load(string path, Transform parent = null)
    {
        GameObject loaded = Load(path);

        GameObject result = Load(loaded, Vector3.zero, Vector3.zero, parent);

        return result;
    }

    public static GameObject Load(string path, Vector3 pos, Vector3 rot, Transform parent = null)
    {
        GameObject loaded = Load(path);

        GameObject result = Load(loaded, pos, rot, parent);

        return result;
    }

    private static GameObject Load(string path)
    {
        GameObject loaded = Resources.Load<GameObject>(path);
        if (loaded == null)
        {
            Debug.Log("Unsuitable path");
            return null;
        }
        return loaded;
    }

    private static GameObject Load(GameObject source, Vector3 pos, Vector3 rot, Transform parent = null)
    {
        GameObject result = Instantiate(source, pos, Quaternion.Euler(rot), parent);

        if(parent != null)
        {
            result.transform.parent = parent;

        }

        if (result == null)
        {
            Debug.Log("can't Instantiate Target");
            return null;
        }

        return result;
    }
}
