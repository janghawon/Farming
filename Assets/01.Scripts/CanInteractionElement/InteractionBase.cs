using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    public bool onOutLine;
    [SerializeField] private Material _outLineMat;
    [SerializeField] private Material _litMat;
    protected SpriteRenderer _spriteRenderer;
    protected abstract void InteractElement();

    private void Update()
    {
        if (onOutLine)
            _spriteRenderer.material = _litMat;
        else
        {
            _spriteRenderer.material = _outLineMat;
        }
        onOutLine = true;
    }

    public void SetSortingOrder(int num)
    {
        _spriteRenderer.sortingOrder = num;
    }
}
