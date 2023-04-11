using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ObjectMeshStudy : MonoBehaviour
{
    private MeshFilter _meshFilter; // 메쉬에 접근하기 위해 메쉬 필터에 접근한다.
    private Mesh _mesh;             // 정점에 접근하기 위해 메쉬에 접근한다.

    [SerializeField]
    private Vector3[] _originVertexs, _nowVertexs; //오브젝트의 초기 메쉬 정점과 현재 정점의 위치를 저장할 배열

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();   // 메쉬 필터를 받아옴
        _mesh = _meshFilter.mesh;                   // 메쉬 필터의 메쉬를 받아옴
        _originVertexs = _mesh.vertices;            // 오브젝트의 점정을 초기 정점 배열에 대입
        _nowVertexs = _originVertexs;               // 오브젝트의 점정을 현재 정점 배열에 대입
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
