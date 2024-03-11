using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeRecoverManager : BaseManager
{
    private RealTimeRecover realTimeRecover;
    
    private float timer = 0.0f;

    public void RecoverControl(bool state)
    {
        if(state)
            realTimeRecover.Recover(enemies, timer);
    }
}
