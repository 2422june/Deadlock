using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float _moveSpeed;
    [SerializeField]
    private Vector3[] _path = new Vector3[4];

    private Vector3 _dir;
    private Transform _arrow;

    [SerializeField]
    private int _pathIndex;
    private bool _isMovable;

    private Define.PathType _type;

    public void Init(Define.CarInfo info, Define.PathType type, int arrowDir)
    {
        _type = type;

        _path[0] = info.start;
        _path[1] = info.center;
        _path[2] = info.center2;
        _path[3] = info.end;
        _dir = info.dir.normalized;

        transform.position = _path[0];
        transform.LookAt(transform.position + _dir);

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
        transform.LookAt(_path[_pathIndex]);
        
        Vector3 rayL, rayR, rayM;
        rayL = transform.position + (Vector3.left * 0.2f);
        rayR = transform.position + (Vector3.right * 0.2f);
        rayM = transform.position;

        if (Physics.Raycast(rayM, transform.forward, 1, 1 << 6) && !Manager._game.IsCanDrive(_type))
        {
            return;
        }

        if (!Physics.Raycast(rayL, transform.forward, 1.7f, 1 << 3)
            && !Physics.Raycast(rayR, transform.forward, 1.7f, 1 << 3))
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }

        if (Vector3.Distance(_path[_pathIndex], transform.position) <= 0.1f)
        {
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        transform.position = _path[_pathIndex];
        _pathIndex++;

        if (_pathIndex >= 4)
        {
            _isMovable = false;
            Manager._game.RemoveThisCar(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Car"))
        {
            _isMovable = false;
            GameObject source = Resources.Load<GameObject>("Prefabs/Explosion");
            GameObject go = Instantiate(source, collision.contacts[0].point, Quaternion.identity);
            GetComponent<Rigidbody>().useGravity = true;
            Manager._game.GameOver();
        }
    }
}
