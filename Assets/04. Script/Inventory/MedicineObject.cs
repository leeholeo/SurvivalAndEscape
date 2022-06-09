using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Medicine Object", menuName = "Inventory System/Items/Medicine")]
public class MedicineObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Medicine;
    }
}
