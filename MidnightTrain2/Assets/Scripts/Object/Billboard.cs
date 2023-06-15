using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _player
    {
        get { return Manager._player; }
        set { }
    }

    private void Update()
    {
        transform.LookAt(_player.position);
    }
}
