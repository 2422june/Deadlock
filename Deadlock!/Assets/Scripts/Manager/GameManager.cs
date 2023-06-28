using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : ManagerBase
{
    private List<GameObject> _cars = new List<GameObject>();
    private GameObject _car;

    private Define.CarInfo[] _carInfos = new Define.CarInfo[12];
    [SerializeField]
    private List<Define.PathType> _canDrive = new List<Define.PathType>();
    [SerializeField]
    private TrafficLightController[] trafficLights = new TrafficLightController[12];

    private bool _isSetInfos;
    private float _timer;


    private Define.PathType _pathType = Define.PathType.None;

    private bool _isStartGame, _isEndGame;
    private GameObject _mainCanvas, _resultCanvas;
    private GameObject _rightCanvas, _leftCanvas, _upCanvas, _downCanvas;

    public override void Init()
    {
        _cars.Clear();
        _canDrive.Clear();

        _timer = 0;

        _isStartGame = false;
        _isEndGame = false;

        SetTrafficLights();

        SetUI();
    }

    private void Update()
    {
        if(_isStartGame && _isSetInfos)
        {
            if(_timer <= 0)
            {
                _timer = Random.Range(2, 4);
                _pathType = SetPathType(Random.Range(0, 4), Random.Range(0, 4));
                CreateCar(_pathType);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    private void SetUI()
    {
        _rightCanvas = Manager._find.FindUITransform("RightCanvas").gameObject;
        _leftCanvas = Manager._find.FindUITransform("LeftCanvas").gameObject;
        _upCanvas = Manager._find.FindUITransform("UpCanvas").gameObject;
        _downCanvas = Manager._find.FindUITransform("DownCanvas").gameObject;


        _mainCanvas = Manager._find.FindUITransform("MainCanvas").gameObject;
        _resultCanvas = Manager._find.FindUITransform("ResultCanvas").gameObject;

        GameObject _startGame = Manager._find.FindUITransform("Button", "MainCanvas").gameObject;
        GameObject _goToMain = Manager._find.FindUITransform("Button", "ResultCanvas").gameObject;
        GameObject _gameExit = Manager._find.FindUITransform("Exit", "MainCanvas").gameObject;

        _startGame.GetComponent<Button>().onClick.AddListener(OnClickStart);
        _goToMain.GetComponent<Button>().onClick.AddListener(OnClickGoToMain);
        _gameExit.GetComponent<Button>().onClick.AddListener(OnClickExit);

        _upCanvas.SetActive(false);
        _downCanvas.SetActive(false);
        _rightCanvas.SetActive(false);
        _leftCanvas.SetActive(false);
        _resultCanvas.SetActive(false);
    }

    public void OnClickStart()
    {
        _upCanvas.SetActive(true);
        _downCanvas.SetActive(true);
        _rightCanvas.SetActive(true);
        _leftCanvas.SetActive(true);
        _resultCanvas.SetActive(false);
        _mainCanvas.SetActive(false);

        _isStartGame = true;
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnClickGoToMain()
    {
        _mainCanvas.SetActive(true);
        _upCanvas.SetActive(false);
        _downCanvas.SetActive(false);
        _rightCanvas.SetActive(false);
        _leftCanvas.SetActive(false);
        _resultCanvas.SetActive(false);

        foreach(GameObject car in _cars)
        {
            Destroy(car);
        }
        _cars.Clear();
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
                trafficLights[index] = Manager._find.FindUITransform(light[j], parents).GetComponent<TrafficLightController>();
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
        if (_isEndGame)
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
        _cars.Add(_car);
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
        _mainCanvas.SetActive(false);
        _upCanvas.SetActive(false);
        _downCanvas.SetActive(false);
        _rightCanvas.SetActive(false);
        _leftCanvas.SetActive(false);
        _resultCanvas.SetActive(true);
        _isStartGame = false;
    }

    public void RemoveThisCar(GameObject car)
    {
        _cars.Remove(car);
        Destroy(car);
    }

    public void GameClear()
    {

    }

    public void GameStart()
    {

    }

}
