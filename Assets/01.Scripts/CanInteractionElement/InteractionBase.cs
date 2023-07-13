using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    protected abstract void InteractElement();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            InteractElement();
        }
    }
}
