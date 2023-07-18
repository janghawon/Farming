using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckElement : MonoBehaviour
{
    [SerializeField] private float _detectRange;
    [SerializeField] private LayerMask _layerMask;
    InteractionBase _selectIb;

    private void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, _detectRange, _layerMask);
        if(col != null)
        {
            if(col.TryGetComponent<InteractionBase>(out InteractionBase ib))
            {
                ib.onOutLine = false;
            }
        }
        
    }
}
