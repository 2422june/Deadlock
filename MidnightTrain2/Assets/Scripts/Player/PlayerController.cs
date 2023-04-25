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

    [SerializeField]
    private Vector3 _rightPos, _leftPos;

    private int _state = 0;
    private float timer = 3;

    [SerializeField]
    private TMP_Text text;

    // 상대 좌표(L - R)
    public Vector3[] _selectPos = new Vector3[2];
    public Vector3[] _stopPos = new Vector3[2];
    public Vector3[] _rightMovePos = new Vector3[2];
    public Vector3[] _leftMovePos = new Vector3[2];
    public Vector3[] _straightMovePos = new Vector3[2];

    private bool isActive;

    void Update()
    {
        if (isActive)
        {
            if(OVRInput.GetUp(OVRInput.RawButton.A) && OVRInput.GetUp(OVRInput.RawButton.X))
            {
                isActive = false;
            }
            return;
        }

        _rightPos = _rightController.position;
        _leftPos = _leftController.position;

        if (OVRInput.Get(OVRInput.RawButton.A) && OVRInput.Get(OVRInput.RawButton.X))
        {
            text.text = $"누르는 중 ({timer}남음)";
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 3;
                isActive = true;
                switch (_state)
                {
                    case 0:
                        _selectPos[0] = _leftPos;
                        _selectPos[1] = _rightPos;
                        text.text = $"선택 저장";
                        break;
                    case 1:
                        _rightMovePos[0] = _leftPos;
                        _rightMovePos[1] = _rightPos;
                        text.text = $"우이동 저장";
                        break;
                    case 2:
                        _leftMovePos[0] = _leftPos;
                        _leftMovePos[1] = _rightPos;
                        text.text = $"좌이동 저장";
                        break;
                    case 3:
                        _straightMovePos[0] = _leftPos;
                        _straightMovePos[1] = _rightPos;
                        text.text = $"직진 저장";
                        break;
                }

                _state++;
            }
        }
    }
}
