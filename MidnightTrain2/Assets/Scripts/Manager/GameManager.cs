using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : ManagerBase
{
    private List<GameObject> _upCars = new List<GameObject>();
    private List<GameObject> _rightCars = new List<GameObject>();
    private List<GameObject> _leftCars = new List<GameObject>();
    private List<GameObject> _downCars = new List<GameObject>();
    private GameObject _car;

    private Define.CarInfo[] _carInfos = new Define.CarInfo[12];
    private bool _isSetInfos;
    private float _timer;

    private Define.PathType _pathType = Define.PathType.None;

    public bool _isAccident;

    public int _start, _end;

    public override void Init()
    {
        _upCars.Clear();
        _rightCars.Clear();
        _leftCars.Clear();
        _downCars.Clear();

        _timer = 0;

        _isAccident = false;
    }

    private void Update()
    {
        if(_isSetInfos)
        {
            if(_timer <= 0)
            {
                _timer = Random.Range(0.5f, 1.5f);
                _pathType = SetPathType(Random.Range(0, 4), Random.Range(0, 4));
                CreateCar(_pathType);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    public void SetPath(Define.CarInfo[] arr)
    {
        _carInfos = arr;
        _isSetInfos = true;
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

    private void CreateCar(Define.PathType type)
    {
        if (_isAccident)
            return;

        int arrowDir = 0;
        switch(type)
        {
            case Define.PathType.UP2Left:
                arrowDir = 1;
                break;
            case Define.PathType.UP2Right:
                arrowDir = -1;
                break;
            case Define.PathType.Left2Up:
                arrowDir = -1;
                break;
            case Define.PathType.Left2Down:
                arrowDir = 1;
                break;
            case Define.PathType.Right2Up:
                arrowDir = 1;
                break;
            case Define.PathType.Right2Down:
                arrowDir = -1;
                break;
            case Define.PathType.Down2Left:
                arrowDir = -1;
                break;
            case Define.PathType.Down2Right:
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
