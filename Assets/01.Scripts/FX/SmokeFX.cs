using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFX : PoolableMono
{
    private void Start()
    {
        gameObject.name = "Smoke";
    }

    public void DeleteThisObj()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
    }
}
