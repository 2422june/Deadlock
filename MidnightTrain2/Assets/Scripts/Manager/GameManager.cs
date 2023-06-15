using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : ManagerBase
{
    private List<GameObject> _upCars, _rightCars, _leftCars, _downCars = new List<GameObject>();
    private GameObject _car;

    private Define.CarInfo[] _carInfos = new Define.CarInfo[12];

    private enum PathType
    {
        UP2Down = 0, UP2Right = 1, UP2Left = 2,
        Right2Up = 3, Right2Down = 4, Right2Left = 5,
        Down2Up = 6, Down2Right = 7, Down2Left = 8,
        Left2Up = 9, Left2Right = 10, Left2Down = 11,
        None = 12
    }

    private PathType _pathType = PathType.None;

    public bool _isAccident;

    public override void Init()
    {
        _upCars.Clear();
        _rightCars.Clear();
        _leftCars.Clear();
        _downCars.Clear();

        _isAccident = false;

        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0, 3));
            _pathType = SetPathType(Random.Range(0, 3), Random.Range(0, 3));
            CreateCar(_pathType);
        }
    }

    public void SetPath(Define.CarInfo[] arr)
    {
        _carInfos = arr;
    }


    private PathType SetPathType(int _startDir, int _endDir)
    {
        PathType dir = 0;
        int startDir = _startDir;
        int endDir = _endDir;
        switch (startDir)
        {
            case 0:
                if (endDir == 1)
                {
                    dir = PathType.UP2Right;
                }
                if (endDir == 2)
                {
                    dir = PathType.UP2Down;
                }
                if (endDir == 3)
                {
                    dir = PathType.UP2Left;
                }
                break;
            case 1:
                if (endDir == 0)
                {
                    dir = PathType.Right2Up;
                }
                if (endDir == 2)
                {
                    dir = PathType.Right2Down;
                }
                if (endDir == 3)
                {
                    dir = PathType.Right2Left;
                }
                break;
            case 2:
                if (endDir == 0)
                {
                    dir = PathType.Down2Up;
                }
                if (endDir == 1)
                {
                    dir = PathType.Down2Right;
                }
                if (endDir == 3)
                {
                    dir = PathType.Down2Left;
                }
                break;
            case 3:
                if (endDir == 0)
                {
                    dir = PathType.Left2Up;
                }
                if (endDir == 1)
                {
                    dir = PathType.Left2Right;
                }
                if (endDir == 2)
                {
                    dir = PathType.Left2Down;
                }
                break;
        }
        return dir;
    }

    private void CreateCar(PathType type)
    {
        if (_isAccident)
            return;

        int arrowDir = 0;
        switch(type)
        {
            case PathType.UP2Left:
                arrowDir = 1;
                break;
            case PathType.UP2Right:
                arrowDir = -1;
                break;
            case PathType.Left2Up:
                arrowDir = -1;
                break;
            case PathType.Left2Down:
                arrowDir = 1;
                break;
            case PathType.Right2Up:
                arrowDir = 1;
                break;
            case PathType.Right2Down:
                arrowDir = -1;
                break;
            case PathType.Down2Left:
                arrowDir = -1;
                break;
            case PathType.Down2Right:
                arrowDir = 1;
                break;
        }

        _car = ResourcesManager.Load(Define.CarSourcePath);
        _car.GetComponent<CarController>().Init(_carInfos[(int)type], arrowDir);
    }

    public void GameOver()
    {

    }

    public void GameClear()
    {

    }

    public void GameStart()
    {

    }

}
