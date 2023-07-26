using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject DropItemObj;
    public float percent;
    public int count;
}

[CreateAssetMenu(menuName = "ScriptableObject/DropTable", fileName = "New DropTable")]
public class DropTable : ScriptableObject
{
    public List<DropItem> DropItemList = new List<DropItem>();
}
