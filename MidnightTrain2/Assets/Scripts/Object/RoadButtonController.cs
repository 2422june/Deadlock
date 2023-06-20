using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadButtonController : ObjectBase
{
    [SerializeField]
    private int Up = 0, Right = 1, Down = 2, Left = 3;

    [Header("----------------------")]
    [SerializeField]
    private int _startPos;

    private void Start()
    {
        _type = Define.CatingType.RoadButton;
        isInteractRightHand = false;
        isInteractleftHand = false;
    }

    private Define.PathType SetPathType(int _endDir)
    {
        Define.PathType dir = 0;
        int endDir = _endDir;
        switch (_startPos)
        {
            case 0:
                if (endDir == 1)
                {
                    dir = Define.PathType.UP2Right;
                }
                if (endDir == 2)
                {
                    dir = Define.PathType.UP2Down;
                }
                if (endDir == 3)
                {
                    dir = Define.PathType.UP2Left;
                }
                break;
            case 1:
                if (endDir == 0)
                {
                    dir = Define.PathType.Right2Up;
                }
                if (endDir == 2)
                {
                    dir = Define.PathType.Right2Down;
                }
                if (endDir == 3)
                {
                    dir = Define.PathType.Right2Left;
                }
                break;
            case 2:
                if (endDir == 0)
                {
                    dir = Define.PathType.Down2Up;
                }
                if (endDir == 1)
                {
                    dir = Define.PathType.Down2Right;
                }
                if (endDir == 3)
                {
                    dir = Define.PathType.Down2Left;
                }
                break;
            case 3:
                if (endDir == 0)
                {
                    dir = Define.PathType.Left2Up;
                }
                if (endDir == 1)
                {
                    dir = Define.PathType.Left2Right;
                }
                if (endDir == 2)
                {
                    dir = Define.PathType.Left2Down;
                }
                break;
        }
        return dir;
    }

}
