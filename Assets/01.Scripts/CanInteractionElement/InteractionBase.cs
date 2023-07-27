using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBase : PoolableMono
{
    protected EntitySO _entitySO;
    [SerializeField] protected DropTable _dropTable;
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

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1.5f, 0));
        Vector2 canPos;
        RectTransform _rect = GameObject.Find("UICANVAS").GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, screenPos, null, out canPos);
        _rect = CollectResourceBar.Instance.gameObject.GetComponent<RectTransform>();

        _rect.localPosition = canPos;

        CollectResourceBar.Instance.
        SetAndStart(_entitySO.Range_Speed.x, _entitySO.Range_Speed.y, _entitySO.DestroyCount, _entitySO.DestroyCount);
    }

    ItemBase dropItem;
    int randomV = 0;
    Vector2 randomPos;
    public virtual void DropItem()
    {
        FXManager.Instance.SetFx(FXType.smoke, transform.position + new Vector3(0, 0.5f, 0), 2, 1);

        for(int i = 0; i < _dropTable.DropItemList.Count; i++)
        {
            randomV = Random.Range(1, 101);
            if(_dropTable.DropItemList[i].percent >= randomV)
            {
                for(int j = 0; j < _dropTable.DropItemList[i].count; j++)
                {
                    dropItem = PoolManager.Instance.Pop(_dropTable.DropItemList[i].DropItemObj.name) as ItemBase;
                    dropItem.name = _dropTable.DropItemList[i].DropItemObj.name;
                    dropItem.transform.position = transform.position;
                    randomPos = (Vector2)transform.position + Random.insideUnitCircle * 2;
                    dropItem.PupItem(randomPos);
                }
            }
        }
        UIManager.Instance.canTexting = true;
        PoolManager.Instance.Push(this);
    }
}
