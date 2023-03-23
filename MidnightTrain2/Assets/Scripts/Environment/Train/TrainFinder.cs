using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainFinder : ManagerBase
{
    public Transform _train; // 기차를 구성하는 오브젝트의 루트 오브젝트
    public Transform _speakers;

    public override void Init()
    {
        _train = GameObject.Find("Train").transform;
        _speakers = _train.Find("Speakers");
    }
}
