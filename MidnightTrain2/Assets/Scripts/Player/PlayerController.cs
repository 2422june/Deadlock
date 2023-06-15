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
    private GameObject _wall;

    [SerializeField]
    private GameObject _canvas;
    private TMP_Text _systemMessage;

    private bool isActive;

    private void Start()
    {
        _wall = GameObject.Find("Wall");
        _canvas = GameObject.Find("UIs");
        //_systemMessage = _canvas.transform.Find("SystemMessage").GetComponent<TMP_Text>();


        isActive = false;
    }
    

    void Update()
    {

    }

    void ReturnHitInfo()
    {
        
    }
}
