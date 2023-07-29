using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase : PoolableMono
{
    [SerializeField] private ItemSO _itemSO;
    float _moveSpeed = 4f;
    Transform player;
    PlayerInventory _pi;
    protected bool isPup;
    protected bool isMagnetic;

    Vector2 currentPos;
    Vector2 pos;

    private void Start()
    {
        player = GameManager.Instance.Player;
        _pi = player.GetComponent<PlayerInventory>();
    }

    public void PupItem(Vector2 targetPos)
    {
        currentPos = transform.position;
        pos = targetPos;
        isPup = true;

        transform.DOMove(pos, 1f).SetEase(Ease.OutBounce);
    }

    private void Update()
    {
        if(isPup && Time.frameCount % 3 == 0 && (Vector2)transform.position == pos)
        {
            isPup = false;
            isMagnetic = true;
        }
        MagneticFunc();
    }

    private void MagneticFunc()
    {
        if(isMagnetic)
        {
            currentPos = player.position - transform.position;
            transform.Translate(currentPos * _moveSpeed * Time.deltaTime, Space.World);

            if(Vector2.Distance(player.position, transform.position) < 0.15f)
            {
                _pi.GetItem(_itemSO.ItemIDX, _itemSO.ItemSprite);
                PoolManager.Instance.Push(this);
            }
        }
    }

    public override void Init()
    {
        transform.position = Vector3.zero;
    }
}
