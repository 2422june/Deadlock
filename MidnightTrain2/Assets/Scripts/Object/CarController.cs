using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float _moveSpeed;
    [SerializeField]
    private Vector3[] _path = new Vector3[4];

    private Vector3 _dir, _rot;
    private Transform _arrow;

    float _rotStep;
    float destRot;
    float rotValue;
    float moveStep;

    [SerializeField]
    private int _pathIndex;
    private bool _isMovable;

    public void Init(Define.CarInfo info, int arrowDir)
    {
        _path[0] = info.start;
        _path[1] = info.center;
        _path[2] = info.center2;
        _path[3] = info.end;
        _dir = info.dir.normalized;

        transform.position = _path[0];
        transform.LookAt(transform.position + _dir);
        _rot = transform.rotation.eulerAngles;

        _moveSpeed = 10f;
        _pathIndex = 0;

        Vector3 dir = Vector3.zero;
        dir.z = arrowDir * -90;
        _arrow = transform.Find("Arrow");
        _arrow.rotation = Quaternion.Euler(dir);

        SetNextDestination();
        _isMovable = true;
    }

    private void Update()
    {
        if (!_isMovable)
            return;

        /*destRot = Mathf.Atan2(_path[_pathIndex].z - transform.position.z, _path[_pathIndex].x - transform.position.x);
        rotValue = destRot - _rot.y;
        moveStep = Vector3.Distance(transform.position, _path[_pathIndex]) / (_moveSpeed * Time.deltaTime);
        _rotStep = rotValue / moveStep;

        _rot.y += _rotStep * Time.deltaTime;
        transform.rotation = Quaternion.Euler(_rot);*/
        transform.LookAt(_path[_pathIndex]);
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;

        if (Vector3.Distance(_path[_pathIndex], transform.position) <= 0.1f)
        {
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        transform.position = _path[_pathIndex];
        transform.LookAt(_path[_pathIndex]);
        _rot = transform.rotation.eulerAngles;
        _pathIndex++;

        if (_pathIndex >= 4)
        {
            _isMovable = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        {
            _isMovable = false;
        }
    }
}
