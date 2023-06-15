using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PathManager : ManagerBase
{
    private GameObject _car;

    private Define.CarInfo[] _carInfos = new Define.CarInfo[12];

    int Up = 0, Right = 1, Down = 2, Left = 3;

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

    public string[] _pointType = { "", "SpawnPoint", "CenterPoint", "EndPoint" };
    private Vector3[,] _points = new Vector3[4, 5];

    private Transform _rootTransform;

    [SerializeField]
    private Button _startButton, _endButton, _goButton, _accidentButton;
    private TMP_Text _startButtonTxt, _endButtonTxt;

    int _startButtonDir, _endButtonDir;

    public override void Init()
    {

        _rootTransform = GameObject.Find("@DynamicObjects").transform;

        SetPoint();

        SetStartUpPath();
        SetStartRightPath();
        SetStartDownPath();
        SetStartLeftPath();

        InitButton();
        InitText();

        _startButtonDir = Up;
        _endButtonDir = Right;
        _startButtonTxt.text = GetDirText(_startButtonDir);
        _endButtonTxt.text = GetDirText(_endButtonDir);

        Manager._game.SetPath(_carInfos);
    }

    private void InitButton()
    {
        string[] parents = { "TestButton" };
        _startButton = Manager._find.FindUI<Button>("Start", parents, Manager._player);
        _endButton = Manager._find.FindUI<Button>("End", parents, Manager._player);
        _goButton = Manager._find.FindUI<Button>("Go", parents, Manager._player);
        _accidentButton = Manager._find.FindUI<Button>("Accident", parents, Manager._player);

        _startButton.onClick.AddListener(OnClickStartButton);
        _endButton.onClick.AddListener(OnClickEndButton);
    }

    private void InitText()
    {
        _startButtonTxt = _startButton.GetComponentInChildren<TMP_Text>();
        _endButtonTxt = _endButton.GetComponentInChildren<TMP_Text>();
    }

    private void OnClickStartButton()
    {
        _startButtonDir = (_startButtonDir + 1) % 4;
        if (_startButtonDir == _endButtonDir)
        {
            _startButtonDir = (_startButtonDir + 1) % 4;
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

    private string GetDirText(int dir)
    {
        switch (dir)
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

}
