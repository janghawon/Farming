using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckElement : MonoBehaviour
{
    [SerializeField] private float _detectRange;
    [SerializeField] private LayerMask _layerMask;
    public InteractionBase selectIb;

    Collider2D col;

    public bool canInteraction;

    private void Start()
    {
        canInteraction = true;
    }

    public void ReactionIB()
    {
        InteractionBase ib = col?.GetComponent<InteractionBase>();
        ib?.InteractElement();
    }

    public void SelectInteraction()
    {
        if(col != null && canInteraction)
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
                selectIb = ib;
                UIManager.Instance.SetGuideText("plz press E", ib.transform.position, true);
                ib.onOutLine = false;
            }
        }
        else
        {
            UIManager.Instance.SetGuideText("", Vector2.zero, false);
        }
    }
}
