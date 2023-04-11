using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScroll : MonoBehaviour
{
    private float _speed;
    private MeshRenderer _renderer;
    private Material _material;
    private Vector2 _dir;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _material = _renderer.material;
        _speed = 0.5f;
        _dir = Vector2.down;
    }

    
    void Update()
    {
        _material.mainTextureOffset += _dir * _speed * Time.deltaTime;
    }
}
