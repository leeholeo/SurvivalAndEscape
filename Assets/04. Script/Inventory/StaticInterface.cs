using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for(int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, /*Vector3.zero, Quaternion.identity,*/ transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate {OnEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit, delegate {OnExit(obj);});
            AddEvent(obj, EventTriggerType.BeginDrag, delegate {OnDragStart(obj);});
            AddEvent(obj, EventTriggerType.EndDrag, delegate {OnDragEnd(obj);});
            AddEvent(obj, EventTriggerType.Drag, delegate {OnDrag(obj);});

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    public void DistructSlot()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            Destroy(_slot.Key);
        }
        itemsDisplayed.Clear();
    }
}
