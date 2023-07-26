using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase : PoolableMono
{
    protected bool isPup;
    protected bool isMagnetic;

    Vector2 currentPos;
    Vector2 pos;

    public void PupItem(Vector2 targetPos)
    {
        currentPos = transform.position;
        pos = targetPos;
        isPup = true;

        transform.DOMove(pos, 0.5f).SetEase(Ease.InBounce);
    }

    private void Update()
    {
        if(isPup && Time.frameCount % 3 == 0 && (Vector2)transform.position == pos)
        {
            isPup = false;
            isMagnetic = true;
        }
        MagneticFunc();
    }

    private void MagneticFunc()
    {
        if(isMagnetic)
        {

        }
    }

    public override void Init()
    {
        transform.position = Vector3.zero;
    }
}
