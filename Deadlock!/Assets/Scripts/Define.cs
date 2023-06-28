using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum CatingType
    {
        RoadButton
    }

    public enum PathType
    {
        UP2Down = 0, UP2Right = 1, UP2Left = 2,
        Right2Up = 3, Right2Down = 4, Right2Left = 5,
        Down2Up = 6, Down2Right = 7, Down2Left = 8,
        Left2Up = 9, Left2Down = 10, Left2Right = 11,
        None = 12
    }


    public enum SE
    {
        pee1, pee2
    }

    [System.Serializable]
    public struct CarInfo
    {
        public Vector3 start, center, center2, end;
        public Vector3 dir;
    }

    public static string CarSourcePath = "Prefabs/Car";
}
