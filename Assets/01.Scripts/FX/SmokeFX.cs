using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFX : PoolableMono
{
    public void DeleteThisObj()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
    }
}
