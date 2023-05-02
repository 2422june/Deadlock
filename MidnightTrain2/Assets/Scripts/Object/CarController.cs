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
    float _destRot;
    float _rotValue;
    float _moveStep;

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

        Vector3 dir = transform.rotation.eulerAngles;
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

        //_destRot = 90 - Mathf.Atan2((_path[_pathIndex] - transform.position).z, (_path[_pathIndex] - transform.position).x);
        //_rot.y = _destRot;
        //_rot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rot), 1f).eulerAngles;
        //transform.rotation = Quaternion.Euler(_rot);
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
        //transform.LookAt(_path[_pathIndex]);
        _rot = transform.rotation.eulerAngles;
        _pathIndex++;

        if (_pathIndex >= 4)
        {
            _isMovable = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Car"))
        {
            _isMovable = false;
            Manager._game._isAccident = true;
            GameObject source = Resources.Load<GameObject>("Prefabs/Explosion");
            GameObject go = Instantiate(source, collision.contacts[0].point, Quaternion.identity);
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
