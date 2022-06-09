using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Metal Object", menuName = "Inventory System/Items/Metal")]
public class MetalObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Metal;
    }
}
