using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckElement : MonoBehaviour
{
    [SerializeField] private float _detectRange;
    [SerializeField] private LayerMask _layerMask;
    public InteractionBase _selectIb;

    Collider2D col;

    public bool canInteraction;

    private void Start()
    {
        canInteraction = true;
    }

    private void SelectInteraction()
    {
        if(Input.GetKeyDown(KeyCode.E) && col != null && canInteraction)
        {
            canInteraction = false;
            InteractionBase ib = col.GetComponent<InteractionBase>();
            ib.SelectInteraction();
        }
    }

    private void Update()
    {
        col = Physics2D.OverlapCircle(transform.position, _detectRange, _layerMask);
        if(col != null)
        {
            if(col.TryGetComponent<InteractionBase>(out InteractionBase ib))
            {
                _selectIb = ib;
                ib.onOutLine = false;
            }
        }
        SelectInteraction();
    }
}
