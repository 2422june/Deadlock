using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum BGM
    {
        None = 0,
    }

    public enum SE
    {
        None = 0,
    }

    public struct CarInfo
    {
        public Vector3 start, center, center2, end;
        public Vector3 dir;
    }
}
