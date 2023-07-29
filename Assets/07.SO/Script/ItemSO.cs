using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemSO", fileName = "New ItemSO")]
public class ItemSO : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemName;
    public int ItemIDX;
}
