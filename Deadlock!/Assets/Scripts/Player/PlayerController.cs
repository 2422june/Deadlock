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

    private void Start()
    {
    }
    

    void Update()
    {

    }

    void ReturnHitInfo()
    {
        
    }
}
