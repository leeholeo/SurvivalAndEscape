using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Material Object", menuName = "Inventory System/Items/Material")]
public class MaterialObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Material;
    }
}
