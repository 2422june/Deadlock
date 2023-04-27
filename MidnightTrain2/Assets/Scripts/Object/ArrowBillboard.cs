using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBillboard : MonoBehaviour
{
    private Transform _player;
    private float _rotValue;
    private Vector3 _rot; 

    private void Start()
    {
        _player = Manager._player;
    }

    private void Update()
    {
        _rotValue = Mathf.Atan2(_player.position.z - transform.position.z, _player.position.x - transform.position.x);
        _rot.y = _rotValue;
    }
}
