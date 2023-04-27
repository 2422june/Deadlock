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
        _uiRoot = _dynamicRoot.Find("UIs");
    }

    public T FindUI<T>(string name, List<string> parents = null) where T : UIBehaviour
    {
        Transform target = _uiRoot;

        if (parents != null)
        {
            for (int i = 0; i < parents.Count; i++)
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

    public Transform FindObject(string name, List<string> parents = null)
    {
        Transform target = _dynamicRoot;

        if (parents != null)
        {
            for (int i = 0; i < parents.Count; i++)
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
