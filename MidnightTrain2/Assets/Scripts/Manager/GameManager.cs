using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : ManagerBase
{
    private string _carSourcePath = "Prefabs/Car";
    private List<GameObject> _cars = new List<GameObject>();
    private GameObject _car;

    private Define.CarInfo[] _carInfos = new Define.CarInfo[12];

    int UP2Down = 0, UP2Right = 1, UP2Left = 2,
        Right2Up = 3, Right2Down = 4, Right2Left = 5,
        Down2Up = 6, Down2Right = 7, Down2Left = 8,
        Left2Up = 9, Left2Right = 10, Left2Down = 11;

    private enum PathType
    {
        UP2Down = 0, UP2Right = 1, UP2Left = 2,
        Right2Up = 3, Right2Down = 4, Right2Left = 5,
        Down2Up = 6, Down2Right = 7, Down2Left = 8,
        Left2Up = 9, Left2Right = 10, Left2Down = 11,
        None = 12
    }
    [SerializeField]
    private PathType _pathType = PathType.None;

    int SpawnPoint = 1,
        centerPoint = 2,
        EndPoint = 3;

    public string[] _pointType = {"", "SpawnPoint","CenterPoint", "EndPoint"};
    private Vector3[,] _points = new Vector3[4, 5];

    private Transform _rootTransform;

    private List<string> _parents = new List<string>();
    [SerializeField]
    private Button _startButton, _endButton, _goButton, _accidentButton;
    private TMP_Text _startButtonTxt, _endButtonTxt;
    public bool _isAccident;

    int _startButtonDir, _endButtonDir;

    public override void Init()
    {
        _cars.Clear();

        _rootTransform = GameObject.Find("@DynamicObjects").transform;

        SetPoint();

        SetStartUpPath();
        SetStartRightPath();
        SetStartDownPath();
        SetStartLeftPath();

        _startButton = Manager._find.FindUI<Button>("Start");
        _endButton = Manager._find.FindUI<Button>("End");
        _goButton = Manager._find.FindUI<Button>("Go");
        _accidentButton = Manager._find.FindUI<Button>("Accident");

        _startButtonTxt = _startButton.GetComponentInChildren<TMP_Text>();
        _endButtonTxt = _endButton.GetComponentInChildren<TMP_Text>();

        _startButton.onClick.AddListener(OnClickStartButton);
        _endButton.onClick.AddListener(OnClickEndButton);
        _goButton.onClick.AddListener(OnClickGoButton);
        _accidentButton.onClick.AddListener(OnClickAccidentButton);

        _startButtonDir = 0;
        _endButtonDir = 1;
        _startButtonTxt.text = GetDirText(_startButtonDir);
        _endButtonTxt.text = GetDirText(_endButtonDir);

        _isAccident = false;
    }

    private void OnClickStartButton()
    {
        _startButtonDir = (_startButtonDir + 1) % 4;
        if(_startButtonDir == _endButtonDir)
        {
            _startButtonDir  = (_startButtonDir + 1) % 4;
        }
        _startButtonTxt.text = GetDirText(_startButtonDir);
    }

    private void OnClickEndButton()
    {
        _endButtonDir = (_endButtonDir + 1) % 4;
        if (_endButtonDir == _startButtonDir)
        {
            _endButtonDir = (_endButtonDir + 1) % 4;
        }
        _endButtonTxt.text = GetDirText(_endButtonDir);
    }

    private void OnClickAccidentButton()
    {
        CreateCar(PathType.UP2Right);
        CreateCar(PathType.Down2Left);
    }

    private string GetDirText(int dir)
    {
        switch(dir)
        {
            case 0:
                return "UP";
            case 1:
                return "RIGHT";
            case 2:
                return "DOWN";
            case 3:
                return "LEFT";
        }

        return "";
    }

    private void OnClickGoButton()
    {
        PathType dir = 0;
        int startDir = _startButtonDir;
        int endDir = _endButtonDir;
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
        CreateCar(dir);
    }

    private void SetPoint()
    {
        Transform pointParent, point;

        for (int pointType = 1; pointType <= 3; pointType++)
        {
            pointParent = _rootTransform.Find(_pointType[pointType]);

            for (int index = 1; index <= 4; index++)
            {
                point = pointParent.Find($"{_pointType[pointType]}{index}");

                _points[pointType, index] = point.position;
            }
        }
    }

    private void SetStartUpPath()
    {
        _carInfos[UP2Down].start = _points[SpawnPoint, 1];
        _carInfos[UP2Down].center = _points[centerPoint, 1];
        _carInfos[UP2Down].center2 = _points[centerPoint, 1];
        _carInfos[UP2Down].end = _points[EndPoint, 3];
        _carInfos[UP2Down].dir = Vector3.back;

        _carInfos[UP2Right].start = _points[SpawnPoint, 1];
        _carInfos[UP2Right].center = _points[centerPoint, 2];
        _carInfos[UP2Right].center2 = _points[centerPoint, 4];
        _carInfos[UP2Right].end = _points[EndPoint, 2];
        _carInfos[UP2Right].dir = Vector3.back;

        _carInfos[UP2Left].start = _points[SpawnPoint, 1];
        _carInfos[UP2Left].center = _points[centerPoint, 2];
        _carInfos[UP2Left].center2 = _points[centerPoint, 2];
        _carInfos[UP2Left].end = _points[EndPoint, 4];
        _carInfos[UP2Left].dir = Vector3.back;
    }

    private void SetStartRightPath()
    {
        _carInfos[Right2Down].start = _points[SpawnPoint, 2];
        _carInfos[Right2Down].center = _points[centerPoint, 3];
        _carInfos[Right2Down].center2 = _points[centerPoint, 1];
        _carInfos[Right2Down].end = _points[EndPoint, 3];
        _carInfos[Right2Down].dir = Vector3.left;

        _carInfos[Right2Up].start = _points[SpawnPoint, 2];
        _carInfos[Right2Up].center = _points[centerPoint, 3];
        _carInfos[Right2Up].center2 = _points[centerPoint, 3];
        _carInfos[Right2Up].end = _points[EndPoint, 1];
        _carInfos[Right2Up].dir = Vector3.left;

        _carInfos[Right2Left].start = _points[SpawnPoint, 2];
        _carInfos[Right2Left].center = _points[centerPoint, 3];
        _carInfos[Right2Left].center2 = _points[centerPoint, 2];
        _carInfos[Right2Left].end = _points[EndPoint, 4];
        _carInfos[Right2Left].dir = Vector3.left;
    }

    private void SetStartDownPath()
    {
        _carInfos[Down2Right].start = _points[SpawnPoint, 3];
        _carInfos[Down2Right].center = _points[centerPoint, 4];
        _carInfos[Down2Right].center2 = _points[centerPoint, 4];
        _carInfos[Down2Right].end = _points[EndPoint, 2];
        _carInfos[Down2Right].dir = Vector3.forward;

        _carInfos[Down2Up].start = _points[SpawnPoint, 3];
        _carInfos[Down2Up].center = _points[centerPoint, 4];
        _carInfos[Down2Up].center2 = _points[centerPoint, 3];
        _carInfos[Down2Up].end = _points[EndPoint, 1];
        _carInfos[Down2Up].dir = Vector3.forward;

        _carInfos[Down2Left].start = _points[SpawnPoint, 3];
        _carInfos[Down2Left].center = _points[centerPoint, 4];
        _carInfos[Down2Left].center2 = _points[centerPoint, 2];
        _carInfos[Down2Left].end = _points[EndPoint, 4];
        _carInfos[Down2Left].dir = Vector3.forward;
    }
    
    private void SetStartLeftPath()
    {
        _carInfos[Left2Right].start = _points[SpawnPoint, 4];
        _carInfos[Left2Right].center = _points[centerPoint, 1];
        _carInfos[Left2Right].center2 = _points[centerPoint, 4];
        _carInfos[Left2Right].end = _points[EndPoint, 2];
        _carInfos[Left2Right].dir = Vector3.right;

        _carInfos[Left2Up].start = _points[SpawnPoint, 4];
        _carInfos[Left2Up].center = _points[centerPoint, 1];
        _carInfos[Left2Up].center2 = _points[centerPoint, 3];
        _carInfos[Left2Up].end = _points[EndPoint, 1];
        _carInfos[Left2Up].dir = Vector3.right;

        _carInfos[Left2Down].start = _points[SpawnPoint, 4];
        _carInfos[Left2Down].center = _points[centerPoint, 1];
        _carInfos[Left2Down].center2 = _points[centerPoint, 1];
        _carInfos[Left2Down].end = _points[EndPoint, 3];
        _carInfos[Left2Down].dir = Vector3.right;
    }

    private void Update()
    {
        if(_pathType != PathType.None)
        {
           CreateCar(_pathType);
            _pathType = PathType.None;
        }
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

        _car = ResourcesManager.Load(_carSourcePath);
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
