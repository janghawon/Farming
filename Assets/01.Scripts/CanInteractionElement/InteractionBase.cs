using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    [SerializeField] private Material _outLineMat;
    [SerializeField] private Material _litMat;
    protected SpriteRenderer _spriteRenderer;
    protected abstract void InteractElement();
    protected virtual void InteractionBefore(bool onOutLine)
    {
        if (onOutLine)
            _spriteRenderer.material = _litMat;
        else
        {
            _spriteRenderer.material = _outLineMat;
        }
            


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            InteractElement();
            InteractionBefore(false);
        }
    }
}
