using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;
using UnityEngine.UI;
/**
* https://0bsoletelab.itch.io/halt-vr-traffic-officer-simulator
* 
* 지정: 한손으로 가르킴
* 멈춤: 두손으로 가르킴
* 오른쪽 이동: 손을 몸 앞으로 댔다가 오른쪽으로 뻗음
* 왼쪽 이동: 손을 몸 앞으로 댔다가 왼쪽으로 뻗음
* 직진: 손을 앞으로 뻗었다가 머리 옆으로
*/
public enum Gesture
{
    select,
    stop,
    rightMove,
    leftMove,
    straightMove
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform _leftController, _rightController;
    private Vector3 _rightPos, _leftPos;
    private Vector3 _rightDir, _leftDir;
    private LineRenderer _rightLine, _leftLine;
    private float _radius;

    private RaycastHit _leftHit, _rightHit;
    private Vector3 _leftHitPoint, _rightHitPoint;
    private Ray _leftRay, _rightRay;

    [SerializeField]
    private GameObject _wall;

    [SerializeField]
    private GameObject _canvas;
    private TMP_Text _systemMessage;

    private bool isActive;

    private void Start()
    {
        _wall = GameObject.Find("Wall");
        _canvas = GameObject.Find("UIs");
        _systemMessage = _canvas.transform.Find("SystemMessage").GetComponent<TMP_Text>();

        _rightLine = _rightController.GetComponent<LineRenderer>();
        _leftLine = _leftController.GetComponent<LineRenderer>();

        _radius = 10f;

        isActive = false;
    }
    

    void Update()
    {
        SetHandPos();
        SetHandDir();
        SetRay();

        ShotRay();
        DrawHandLine();

        if (isActive)
        {
            if (OVRInput.Get(OVRInput.RawButton.A) && OVRInput.Get(OVRInput.RawButton.X))
            {
            }
        }
    }

    void SetHandPos()
    {
        _rightPos = _rightController.position;
        _leftPos = _leftController.position;
    }

    void SetHandDir()
    {
        _rightDir = (_rightController.forward);
        _leftDir = (_leftController.forward);
    }

    void SetRay()
    {
        _rightRay.origin = _rightPos;
        _rightRay.direction = _rightDir;

        _leftRay.origin = _leftPos;
        _leftRay.direction = _leftDir;
    }

    void ShotRay()
    {
        _rightHitPoint = _rightDir * _radius;
        if (Physics.Raycast(_rightRay, out _rightHit, _radius))
        {
            _rightHitPoint = _rightHit.point;
        }

        _leftHitPoint = _leftDir * _radius;
        if (Physics.Raycast(_leftRay, out _leftHit, _radius))
        {
            _leftHitPoint = _leftHit.point;
        }
    }

    void DrawHandLine()
    {
        _rightLine.SetPosition(0, _rightPos);
        _rightLine.SetPosition(1, _rightHitPoint);

        _leftLine.SetPosition(0, _leftPos);
        _leftLine.SetPosition(1, _leftHitPoint);
    }

    void ReturnHitInfo()
    {
        
    }
}
