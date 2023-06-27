using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FindManager : ManagerBase
{
    [SerializeField]
    private Transform _dynamicRoot;
    [SerializeField]
    private Transform _uiRoot;

    public override void Init()
    {
        _dynamicRoot = GameObject.Find("@DynamicObjects").transform;
        _uiRoot = GameObject.Find("@UIs").transform;
    }

    public T FindUI<T>(string name, string[] parents = null, Transform root = null) where T : UIBehaviour
    {
        Transform target = root;
        if (target == null)
        {
            target = _uiRoot;
        }

        if (parents != null)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                target = target.Find(parents[i]);
            }
        }
        target = target.Find(name);

        if (target == null)
        {
            Debug.Log("No Object");
            return default(T);
        }

        T result = target.GetComponent<T>();
        return result;
    }

    public Transform FindUI(string name, string[] parents = null, Transform root = null)
    {
        Transform target = root;
        if (target == null)
        {
            target = _uiRoot;
        }

        if (parents != null)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                target = target.Find(parents[i]);
            }
        }
        target = target.Find(name);

        if (target == null)
        {
            Debug.Log("No Object");
            return null;
        }

        return target;
    }


    public Transform FindUI(string name, string parent = null, Transform root = null)
    {
        Transform target = root;
        if (target == null)
        {
            target = _uiRoot;
        }

        if (parent != null)
        {
            target = target.Find(parent);
        }
        target = target.Find(name);

        if (target == null)
        {
            Debug.Log("No Object");
            return null;
        }

        return target;
    }

    public Transform FindObject(string name, string[] parents = null)
    {
        Transform target = _dynamicRoot;

        if (parents != null)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                target = target.Find(parents[i]);
            }
        }
        target = target.Find(name);

        if (target == null)
        {
            Debug.Log("No Object");
            return null;
        }

        return target;
    }
}
