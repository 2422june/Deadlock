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
* ����: �Ѽ����� ����Ŵ
* ����: �μ����� ����Ŵ
* ������ �̵�: ���� �� ������ ��ٰ� ���������� ����
* ���� �̵�: ���� �� ������ ��ٰ� �������� ����
* ����: ���� ������ �����ٰ� �Ӹ� ������
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

    // ��� ��ǥ(L - R)
    public Vector3[] _forwardPos = new Vector3[2];
    public Vector3[] _openPos = new Vector3[2];
    public Vector3[] _gatherPos = new Vector3[2];

    private bool isActive;

    private void Start()
    {
        _wall = GameObject.Find("Wall");
        _canvas = GameObject.Find("UIs");
        _systemMessage = _canvas.transform.Find("SystemMessage").GetComponent<TMP_Text>();
        _systemMessage.text = "������ �����ϴ���";

        isActive = false;
        _state = 0;

        StartCoroutine(Tutorial());
    }

    private IEnumerator Tutorial()
    {
        if (_state == 0)
        {
            yield return new WaitForSeconds(3);
            _systemMessage.text = "Ʃ�丮���� �����ϰڽ��ϴ�.";
            yield return _two;
            _systemMessage.text = "���� ���� �ν��� �ְڽ��ϴ�.";
            yield return _two;
            _systemMessage.text = "����� ������ ������ ���� A�� X�� �����ּ���.";
            isActive = true;
        }
        else if (_state == 1)
        {
            yield return _two;
            _systemMessage.text = "����� ������ ������ A�� X�� �����ּ���.";
            isActive = true;
        }
        else if (_state == 2)
        {
            yield return _two;
            _systemMessage.text = "���� �е��� ����� ���� �տ� ������ A�� X�� �����ּ���.";
            isActive = true;
        }
        else if (_state == 3)
        {
            yield return _two;
            _systemMessage.text = "���� �ν��� �Ϸ� �Ǿ����ϴ�.";
            yield return _two;
            _systemMessage.text = "������� �ð� �ǽñ� �ٶ��ϴ�.";
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
        _systemMessage.text = "����Ϸ�";
    }
    

    void Update()
    {
        if (isActive)
        {
            _rightPos = _rightController.localPosition;
            _leftPos = _leftController.localPosition;

            if (OVRInput.Get(OVRInput.RawButton.A) && OVRInput.Get(OVRInput.RawButton.X))
            {
                _systemMessage.text = $"�ν��� ({(int)timer}�� ����)";
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
