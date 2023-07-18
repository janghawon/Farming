using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : MonoBehaviour
{
    protected EntitySO _entitySO;
    protected DropTable _dropTable;
    public bool onOutLine;
    [SerializeField] private Material _outLineMat;
    [SerializeField] private Material _litMat;
    protected SpriteRenderer _spriteRenderer;
    public abstract void InteractElement();

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
    public void SelectInteraction()
    {
        CollectResourceBar.Instance.isFinish = false;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector2 canPos;
        RectTransform _rect = GameObject.Find("UICANVAS").GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, screenPos, null, out canPos);
        _rect = CollectResourceBar.Instance.gameObject.GetComponent<RectTransform>();

        _rect.localPosition = canPos;

        CollectResourceBar.Instance.
        SetAndStart(_entitySO.Range_Speed.x, _entitySO.Range_Speed.y, _entitySO.DestroyCount, _entitySO.DestroyCount);
    }

    public virtual void DropItem()
    {

    }
}
