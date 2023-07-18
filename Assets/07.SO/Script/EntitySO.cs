using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EntitySO", fileName = "New EntitySO")]
public class EntitySO : ScriptableObject
{
    public Vector2 Range_Speed;
    public float DestroyCount;
}
