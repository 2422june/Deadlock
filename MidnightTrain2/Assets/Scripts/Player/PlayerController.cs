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
