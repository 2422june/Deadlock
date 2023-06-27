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
    [SerializeField]
    private List<Define.PathType> _canDrive = new List<Define.PathType>();
    [SerializeField]
    private TrafficLightController[] trafficLights = new TrafficLightController[12];

    private bool _isSetInfos;
    private float _timer;

    public bool _isAccident;

    private Define.PathType _pathType = Define.PathType.None;

    public override void Init()
    {
        _upCars.Clear();
        _rightCars.Clear();
        _leftCars.Clear();
        _downCars.Clear();
        _canDrive.Clear();

        _timer = 0;

        _isAccident = false;

        SetTrafficLights();
    }

    private void Update()
    {
        if(_isSetInfos)
        {
            if(_timer <= 0)
            {
                _timer = Random.Range(1, 1.5f);
                _pathType = SetPathType(Random.Range(0, 4), Random.Range(0, 4));
                CreateCar(_pathType);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    private void SetTrafficLights()
    {
        string[] canvas = { "Up", "Right", "Down", "Left" };
        string[] light = { "Up", "Down", "Right", "Left" };
        string[] parents = { "", "Button" };
        int index = 0;

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (canvas[i] == light[j])
                {
                    continue;
                }

                parents[0] = canvas[i] + "Canvas";
                trafficLights[index] = Manager._find.FindUI(light[j], parents).GetComponent<TrafficLightController>();
                index++;
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
        _car.GetComponent<CarController>().Init(_carInfos[(int)type], type, arrowDir);
    }

    public void AddPath(Define.PathType type)
    {
        if (!_canDrive.Contains(type))
        {
            _canDrive.Add(type);
            trafficLights[(int)type].OnLight();
        }
    }

    public void RemovePath(Define.PathType type)
    {
        if (_canDrive.Contains(type))
        {
            _canDrive.Remove(type);
            trafficLights[(int)type].OffLight();
        }
    }

    public bool IsCanDrive(Define.PathType type)
    {
        return (_canDrive.Contains(type));
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
