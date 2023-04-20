using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

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
    private Vector3[] _selectPos = new Vector3[2];
    [SerializeField]
    private Vector3[] _stopPos = new Vector3[2];
    [SerializeField]
    private Vector3[] _rightMovePos = new Vector3[2];
    [SerializeField]
    private Vector3[] _leftMovePos = new Vector3[2];
    [SerializeField]
    private Vector3[] _straightMovePos = new Vector3[2];

    void Update()
    {
        if(OVRInput.Get(OVRInput.RawButton.A) && OVRInput.Get(OVRInput.RawButton.X))
        {

        }
    }
}
