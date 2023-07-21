using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum TreeType
{
    Basic = 0,
    Green = 1,
    Little = 2
}

[System.Serializable]
class TreeData
{
    public Sprite treeImage;
    public DropTable treeDropTable;
    public EntitySO treeEntitySo;
}

public class TreeScript : InteractionBase
{
    [SerializeField] private TreeData[] _treeDatas = new TreeData[3];
    private Dictionary<TreeType, TreeData> _treeSelecter = new Dictionary<TreeType, TreeData>();
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        for(int i = 0; i < _treeDatas.Length; i++)
        {
            _treeSelecter.Add((TreeType)i, _treeDatas[i]);
        }
        int idx = Random.Range(0, 11);
        TreeType _type = TreeType.Basic;
        if(idx <= 5)
        {
            _type = TreeType.Basic;
        }
        else if(idx <= 8)
        {
            _type = TreeType.Little;
        }
        else
        {
            _type = TreeType.Green;
        }

        _spriteRenderer.sprite = _treeSelecter[_type].treeImage;
        _dropTable = _treeSelecter[_type].treeDropTable;
        _entitySO = _treeSelecter[_type].treeEntitySo;
    }
    public override void InteractElement()
    {
        transform.DOShakePosition(0.08f, 0.2f, 5);
    }

    public override void Init()
    {
        onOutLine = false;
    }
}
