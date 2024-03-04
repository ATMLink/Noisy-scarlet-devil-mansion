using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeRecover : MonoBehaviour
{
    private void Recover(IntVariable hp)
    {
        var time = Time.deltaTime;
        if(time%1.5f==0)
            hp.setValue(hp.Value+1);
    }
}
