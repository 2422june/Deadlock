using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
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

    [SerializeField]
    private GameObject _wall;

    private int _state = 0;
    private float timer = 3;
    private WaitForSeconds _second = new WaitForSeconds(1);
    private WaitForSeconds _two = new WaitForSeconds(2);

    [SerializeField]
    private GameObject _canvas;
    private TMP_Text _systemMessage;

    // 상대 좌표(L - R)
    public Vector3[] _forwardPos = new Vector3[2];
    public Vector3[] _openPos = new Vector3[2];
    public Vector3[] _gatherPos = new Vector3[2];

    private bool isActive;

    private void Start()
    {
        _wall = GameObject.Find("Wall");
        _canvas = GameObject.Find("UIs");
        _systemMessage = _canvas.transform.Find("SystemMessage").GetComponent<TMP_Text>();
        _systemMessage.text = "게임을 시작하는중";

        isActive = false;
        _state = 0;

        StartCoroutine(Tutorial());
    }

    private IEnumerator Tutorial()
    {
        if (_state == 0)
        {
            yield return new WaitForSeconds(3);
            _systemMessage.text = "튜토리얼을 시작하겠습니다.";
            yield return _two;
            _systemMessage.text = "먼저 동작 인식이 있겠습니다.";
            yield return _two;
            _systemMessage.text = "양손을 앞으로 적당히 뻗고 A와 X를 눌러주세요.";
            isActive = true;
        }
        else if (_state == 1)
        {
            yield return _two;
            _systemMessage.text = "양손을 적당히 벌리고 A와 X를 눌러주세요.";
            isActive = true;
        }
        else if (_state == 2)
        {
            yield return _two;
            _systemMessage.text = "벽을 밀듯이 양손을 가슴 앞에 모으고 A와 X를 눌러주세요.";
            isActive = true;
        }
        else if (_state == 3)
        {
            yield return _two;
            _systemMessage.text = "동작 인식이 완료 되었습니다.";
            yield return _two;
            _systemMessage.text = "무사고의 시간 되시길 바랍니다.";
            Debug.Log(_forwardPos[0] + " : " + _forwardPos[1]);
            Debug.Log(_openPos[0] + " : " + _openPos[1]);
            Debug.Log(_gatherPos[0] + " : " + _gatherPos[1]);
        }
    }

    private void SetGesturePos()
    {
        switch (_state)
        {
            case 0:
                _forwardPos[0] = _leftPos;
                _forwardPos[1] = _rightPos;
                break;
            case 1:
                _openPos[0] = _leftPos;
                _openPos[1] = _rightPos;
                break;
            case 2:
                _gatherPos[0] = _leftPos;
                _gatherPos[1] = _rightPos;
                break;
        }
        _systemMessage.text = "저장완료";
    }
    

    void Update()
    {
        if (isActive)
        {
            _rightPos = _rightController.localPosition;
            _leftPos = _leftController.localPosition;

            if (OVRInput.Get(OVRInput.RawButton.A) && OVRInput.Get(OVRInput.RawButton.X))
            {
                _systemMessage.text = $"인식중 ({(int)timer}초 남음)";
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    SetGesturePos();
                    _state++;
                    timer = 3;
                    isActive = false;
                    StartCoroutine(Tutorial());
                }
            }
        }


    }
}
