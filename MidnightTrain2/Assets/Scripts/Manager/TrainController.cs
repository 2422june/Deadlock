using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrainController : ManagerBase
{
    private bool[] _isOnFuzz = new bool[3];
    private bool _isOnEngine, _isOnPower;

    #region On button

    public void OnFuzz(int index)
    {
        _isOnFuzz[index] = true;
    }

    public void OnEngine()
    {
        _isOnEngine = true;
    }

    public void OnPower()
    {
        _isOnPower = true;
    }
    #endregion
    
    #region Off button
    public void OffPower()
    {
        _isOnPower = false;
    }

    public void OffFuzz(int index)
    {
        _isOnFuzz[index] = false;
    }

    public void OffEngine()
    {
        _isOnEngine = false;
    }
    #endregion

    bool IsNotYet()
    {
        return _isOnEngine;
    }

    void NotYet()
    {

    }


}
