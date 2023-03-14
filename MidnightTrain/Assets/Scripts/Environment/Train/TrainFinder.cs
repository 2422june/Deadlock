using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainFinder : ManagerBase
{
    public Transform _train;
    public Transform _speakers;

    public override void Init()
    {
        _train = GameObject.Find("Train").transform;
        _speakers = _train.Find("Speakers");
    }
}
