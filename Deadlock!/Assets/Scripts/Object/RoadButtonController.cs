using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadButtonController : ObjectBase
{
    [SerializeField]
    private int Up = 0, Right = 1, Down = 2, Left = 3;

    [Header("----------------------")]
    [SerializeField]
    private int _point;

    private GameObject RC, LC;

    private void Start()
    {
        _type = Define.CatingType.RoadButton;
        isInteractRightHand = false;
        isInteractLeftHand = false;

        //RC = transform.Find("RC").gameObject;
        //LC = transform.Find("LC").gameObject;

        //RC.SetActive(false);
        //LC.SetActive(false);
    }

    public void Cancle()
    {
        isInteractRightHand = false;
        isInteractLeftHand = false;
        //RC.SetActive(false);
        //LC.SetActive(false);
    }

    public bool IsInteractBothHand()
    {
        return (isInteractRightHand  && isInteractLeftHand);
    }

    public override void Interact(VRController interactedHand)
    {
        if(interactedHand.IsRightController())
        {
            isInteractRightHand = true;
            //RC.SetActive(true);
        }
        else
        {
            isInteractLeftHand = true;
            //LC.SetActive(true);
        }

        Manager._traffic.AddPath(_point);
    }
}
