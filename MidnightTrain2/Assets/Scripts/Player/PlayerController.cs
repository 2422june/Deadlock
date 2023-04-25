using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;
using UnityEngine.UI;
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

    private int _state = 0;
    private float timer = 3;

    [SerializeField]
    private TMP_Text text;

    // ��� ��ǥ(L - R)
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
            text.text = $"������ �� ({timer}����)";
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
                        text.text = $"���� ����";
                        break;
                    case 1:
                        _rightMovePos[0] = _leftPos;
                        _rightMovePos[1] = _rightPos;
                        text.text = $"���̵� ����";
                        break;
                    case 2:
                        _leftMovePos[0] = _leftPos;
                        _leftMovePos[1] = _rightPos;
                        text.text = $"���̵� ����";
                        break;
                    case 3:
                        _straightMovePos[0] = _leftPos;
                        _straightMovePos[1] = _rightPos;
                        text.text = $"���� ����";
                        break;
                }

                _state++;
            }
        }
    }
}
