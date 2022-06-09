using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Stone Object", menuName = "Inventory System/Items/Stone")]
public class StoneObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Stone;
    }
}
