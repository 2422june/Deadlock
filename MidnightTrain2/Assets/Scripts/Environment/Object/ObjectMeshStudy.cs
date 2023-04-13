using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ObjectMeshStudy : MonoBehaviour
{
    private MeshFilter _meshFilter; // �޽��� �����ϱ� ���� �޽� ���Ϳ� �����Ѵ�.
    private Mesh _mesh;             // ������ �����ϱ� ���� �޽��� �����Ѵ�.

    [SerializeField]
    private Vector3[] _originVertexs, _nowVertexs; //������Ʈ�� �ʱ� �޽� ������ ���� ������ ��ġ�� ������ �迭

    [SerializeField]
    private Vector3[] _verticsVelocitys; //�� ������ �̵� �ӵ�

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();   // �޽� ���͸� �޾ƿ�
        _mesh = _meshFilter.mesh;                   // �޽� ������ �޽��� �޾ƿ�
        _originVertexs = _mesh.vertices;            // ������Ʈ�� ������ �ʱ� ���� �迭�� ����
        _nowVertexs = _originVertexs;               // ������Ʈ�� ������ ���� ���� �迭�� ����
        _verticsVelocitys = new Vector3[_originVertexs.Length]; //�� ������ �̵� �ӵ� ����
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }
}
