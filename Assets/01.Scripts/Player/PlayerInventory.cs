using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemElement
{
    public Sprite thisSprite;
    public int thisCount;
    public int thisNum;

    public ItemElement(Sprite sp, int count, int num)
    {
        thisSprite = sp;
        thisCount = count;
        thisNum = num;
    }
}

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Image _Inventory;
    [SerializeField] private Transform _inventoryContent;
    [SerializeField] private Transform[] _iElementsGroup = new Transform[12];
    [SerializeField] private TextMeshProUGUI[] _countTexts;
    private Dictionary<int, ItemElement> _itemTable;

    Image texImage;
    int num;

    private void Awake()
    {
        for(int i = 0; i < _iElementsGroup.Length; i++)
        {
            _iElementsGroup[i] = _inventoryContent.Find($"Slot_{i + 1}");
        }
    }

    private void Start()
    {
        ActiveInventory(false);
    }

    public void ActiveInventory(bool active)
    {
        _Inventory.enabled = active;
        for(int i = 0; i < _iElementsGroup.Length; i++)
        {
            texImage = (Image)_iElementsGroup[i].GetComponent("Image");
            texImage.enabled = active;
            texImage = (Image)_iElementsGroup[i].Find("TexImage").GetComponent("Image");
            texImage.enabled = active;
        }
    }

    public void GetItem(int idx, Sprite sp)
    {
        if(_itemTable[idx] != null) // 이미 있을 때
        {
            _itemTable[idx].thisCount++;
            if(_itemTable[idx].thisCount > 99)
            {
                _countTexts[_itemTable[idx].thisNum].text = "99+";
                return;
            }
            _countTexts[_itemTable[idx].thisNum].text = _itemTable[idx].thisCount.ToString();
        }
        else // 새로운 아이템일 때
        {
            for(int i = 0; i < _iElementsGroup.Length; i++)
            {
                texImage = (Image)_iElementsGroup[i].Find("TexImage").GetComponent("Image");
                if (texImage.sprite == null)
                {
                    num = i + 1;
                    break;
                }
            }
            _itemTable[idx] = new ItemElement(sp, 1, num);
            texImage.sprite = sp;
        }
    }
}
