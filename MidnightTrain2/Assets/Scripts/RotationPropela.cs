using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPropela : MonoBehaviour
{
    [SerializeField]
    private Transform propela;

    void Update()
    {
        propela.Rotate(Vector3.forward * 360 * Time.deltaTime);
        transform.position += Vector3.forward * Time.deltaTime;
    }
}
