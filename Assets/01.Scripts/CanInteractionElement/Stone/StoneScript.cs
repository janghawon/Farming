using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum StoneType
{
    small = 0,
    Big = 1
}

[System.Serializable]
class StoneData
{
    public Sprite stoneImage;
    public DropTable stoneDropTable;
    public EntitySO stoneEntitySo;
}

public class StoneScript : InteractionBase
{
    [SerializeField] private StoneData[] _stoneDatas = new StoneData[2];
    private Dictionary<StoneType, StoneData> _stoneSelecter = new Dictionary<StoneType, StoneData>();
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        for (int i = 0; i < _stoneDatas.Length; i++)
        {
            _stoneSelecter.Add((StoneType)i, _stoneDatas[i]);
        }
        int idx = Random.Range(0, 11);
        StoneType _type = StoneType.small;
        if (idx <= 4)
        {
            _type = StoneType.Big;
        }
        _spriteRenderer.sprite = _stoneSelecter[_type].stoneImage;
        _dropTable = _stoneSelecter[_type].stoneDropTable;
        _entitySO = _stoneSelecter[_type].stoneEntitySo;
    }
    public override void InteractElement()
    {
        transform.DOShakePosition(0.08f, 0.2f, 5);
        _colResourceBar.Call();
        FXManager.Instance.SetFx(FXType.smoke, transform.position, 1, 0.2f);
    }

    public override void Init()
    {
        onOutLine = false;
    }
}
