using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : ManagerBase
{

    private Define.PathType _pathType = Define.PathType.None;

    public int[] _carPath = new int[2];

    private RoadButtonController[] _roadButtons = new RoadButtonController[4];

    public override void Init()
    {
        _carPath[0] = -1;
        _carPath[1] = -1;
        FindRoadButtom();
    }

    public void FindRoadButtom()
    {
        string[] parents = { "UpCanvas", "RightCanvas", "DownCanvas", "LeftCanvas" };
        for (int i = 0; i < parents.Length; i++)
        {
            _roadButtons[i] = Manager._find.FindUI("Button", parents[i]).GetComponent<RoadButtonController>();
        }
    }

    public void AddPath(int point)
    {
        if (_carPath[0] == -1)
        {
            _carPath[0] = point;
        }
        else if (_carPath[1] == -1)
        {
            _carPath[1] = point;

            if (_carPath[0] == _carPath[1])
            {
                if (_roadButtons[point].IsInteractBothHand())
                {
                    LineStop(point);
                }
                else
                {
                    LineCancle(point);
                }
            }
            else
            {
                LineOpen();
            }
            ClearPath();
        }
        Debug.Log("Add Path");
    }

    private Define.PathType SetPathType(int _startDir, int _endDir)
    {
        Define.PathType dir = 0;
        int startDir = _startDir;
        int endDir = _endDir;
        switch (startDir)
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


    public void ClearPath()
    {
        _carPath[0] = -1;
        _carPath[1] = -1;
    }

    public void LineStop(int point)
    {
        _roadButtons[point].Cancle();
        if (point == 0)
        {
            //Up
            Manager._game.RemovePath(Define.PathType.UP2Right);
            Manager._game.RemovePath(Define.PathType.UP2Down);
            Manager._game.RemovePath(Define.PathType.UP2Left);
        }
        else if (point == 1)
        {
            //Right
            Manager._game.RemovePath(Define.PathType.Right2Up);
            Manager._game.RemovePath(Define.PathType.Right2Down);
            Manager._game.RemovePath(Define.PathType.Right2Left);
        }
        else if (point == 2)
        {
            //Down
            Manager._game.RemovePath(Define.PathType.Down2Up);
            Manager._game.RemovePath(Define.PathType.Down2Right);
            Manager._game.RemovePath(Define.PathType.Down2Left);
        }
        else
        {
            //Left
            Manager._game.RemovePath(Define.PathType.Left2Up);
            Manager._game.RemovePath(Define.PathType.Left2Right);
            Manager._game.RemovePath(Define.PathType.Left2Down);
        }
    }

    public void LineCancle(int point)
    {
        _roadButtons[point].Cancle();
    }

    public void LineOpen()
    {
        _pathType = SetPathType(_carPath[0], _carPath[1]);
        Manager._game.AddPath(_pathType);
    }
}
