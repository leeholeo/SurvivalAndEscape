using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    public MainScript mainScript;

    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    // Start is called before the first frame update
    public void InitializeSlot()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public abstract void CreateSlots();

    public void UpdateSlots()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                // Debug.Log("------");
                // Debug.Log(_slot);
                // Debug.Log(_slot.Key);
                // Debug.Log(_slot.Key.transform);
                // Debug.Log(_slot.Key.transform.GetChild(0));
                // Debug.Log(_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>());
                // Debug.Log(_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite);
                // Debug.Log("------");
                // Debug.Log(_slot);
                // Debug.Log(_slot.Value);
                // Debug.Log(_slot.Value.item);
                // Debug.Log(_slot.Value.item.Id);
                // Debug.Log("------");
                // Debug.Log(inventory);
                // Debug.Log(inventory.database);
                // Debug.Log(inventory.database.GetItem[_slot.Value.item.Id]);
                // Debug.Log(inventory.database.GetItem[_slot.Value.item.Id].uiDisplay);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                // Debug.Log("------");
                // Debug.Log(_slot);
                // Debug.Log(_slot.Key);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        //Debug.Log("PointerEnter");
        mainScript.mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            //Debug.Log("PointerEnter-ContainsKey");
            mainScript.mouseItem.hoverItem = itemsDisplayed[obj];
        }
    }
    public void OnExit(GameObject obj)
    {
        //Debug.Log("PointerExit");
        mainScript.mouseItem.hoverObj = null;
        mainScript.mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        //Debug.Log("BeginDrag");
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);
        mouseObject.transform.SetParent(transform.parent);
        if(itemsDisplayed[obj].ID >= 0)
        {
            //Debug.Log("BeginDrag-itemsDisplayed[obj].ID >= 0");
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mainScript.mouseItem.obj = mouseObject;
        mainScript.mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        //Debug.Log("EndDrag");
        if(mainScript.mouseItem.hoverObj)
        {
            //Debug.Log("EndDrag-mouseItem.hoverObj");
            inventory.MoveItem(itemsDisplayed[obj], mainScript.mouseItem.hoverItem.parent.itemsDisplayed[mainScript.mouseItem.hoverObj]);
        }
        else
        {
            //Debug.Log("EndDrag-Remove");
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(mainScript.mouseItem.obj);
        mainScript.mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        //Debug.Log("Drag");
        if(mainScript.mouseItem.obj != null)
        {
            //Debug.Log("Drag-mouseItem.obj != null");
            mainScript.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnClick(GameObject obj)
    {
        //Debug.Log("Clicked");
        if(itemsDisplayed[obj].ID >= 0)
        {
            inventory.UseItem(itemsDisplayed[obj].item);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
