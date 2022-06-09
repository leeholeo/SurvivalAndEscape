using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Electronics Object", menuName = "Inventory System/Items/Electronics")]
public class ElectronicsObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Electronics;
    }
}
