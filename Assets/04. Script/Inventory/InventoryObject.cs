using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDataBaseObject database;
    public Inventory Container;
    public bool isPlayerInven = false;
    public bool isSpecial = false;
    public ItemObject specialItem;
    private Item tempItem;

    private PlayerObject playerObject;
    private FoodObject foodObject;
    private WaterObject waterObject;

    public void InitSpace()
    {
        if(isPlayerInven)
        {
            return;
        }
        else if(isSpecial)
        {
            tempItem = new Item(specialItem);
            SetEmptySlot(tempItem, 1);
            return;
        }
        else
        {
            int rand = Random.Range(-1, 19);
            if(rand < 0)
            {
                return;
            }
            else
            {
                tempItem = new Item(database.Items[rand]);
                SetEmptySlot(tempItem, 1);
                return;
            }
        }
    }

    public void AddItem(Item _item, int _amount)
    {
        if(_item.states.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;
        }

        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID == _item.Id && Container.Items[i].amount < _item.MaxStackSize)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        // set up functions for full inventory
        return null;    
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    public int CheckItem(Item _item)
    {
        int itemCount = 0;
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID == _item.Id)
            {
                itemCount += Container.Items[i].amount;
            }
        }
        return itemCount;
    }

    // public Tuple<int, List<Tuple<int, int>>> CheckItem(Item _item)
    // {
    //     int itemCount = 0;
    //     List<Tuple<int, int>> itemIdxAndCntList = new List<Tuple<int, int>>();
    //     for (int i = 0; i < Container.Items.Length; i++)
    //     {
    //         if(Container.Items[i].item == _item)
    //         {
    //             int itemAmount = Container.Items[i].amount;
    //             itemIdxAndCntList.Add(new Tuple<int, int> (i, itemAmount));
    //             itemCount += itemAmount;
    //         }
    //     }
    //     return Tuple.Create(itemCount, itemIdxAndCntList);
    // }

    public bool SubtractItem(Item _item, int _amount)
    {
        Debug.Log("_item");
        for(int i = 0; i < Container.Items.Length; i++)
        {
            Debug.Log(Container.Items[i].ID);
            Debug.Log(_item.Id);
            if(Container.Items[i].ID == _item.Id)
            {
                if (Container.Items[i].amount > _amount)
                {
                    Container.Items[i].AddAmount(-_amount);
                    _amount = 0;
                }
                else
                {
                    Container.Items[i].UpdateSlot(-1, null, 0);
                    _amount -= Container.Items[i].amount;
                }

                if (_amount == 0)
                    return true;
            }
        }
        Debug.LogWarning($"You are subtracting {_item.Name} more than you have!");
        return false;
    }

    public void UseItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID == _item.Id)
            {
                playerObject = GameObject.Find("Player").GetComponent<PlayerScript>().playerObject;
                string _tempType = database.GetItem[_item.Id].type.ToString();
                if (_tempType == "Food")
                {
                    Debug.Log("Food");
                    int retoreVal = database.GetItem[_item.Id].restoreHungerValue;
                    playerObject.ChangeHungryPoint(retoreVal);
                    Container.Items[i].AddAmount(-1);
                }
                else if (_tempType == "Water")
                {
                    Debug.Log("Water");
                    int retoreVal = database.GetItem[_item.Id].restoreThirstValue;
                    playerObject.ChangeThirstyPoint(retoreVal);
                    Container.Items[i].AddAmount(-1);
                }
                else
                {
                    Debug.Log("Metarial");
                }
                return;
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        // string saveData = JsonUtility.ToJson(this, true);
        // BinaryFormatter bf = new BinaryFormatter();
        // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        // bf.Serialize(file, saveData);
        // file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream =  new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        try
        {
            formatter.Serialize(stream, Container);
        }
        catch (System.Exception e)
        {}
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            File.Delete(savePath);
            return;
            // BinaryFormatter bf = new BinaryFormatter();
            // FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            // file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer;
            try
            {
                newContainer = (Inventory)formatter.Deserialize(stream);
            }
            catch (System.Exception e)
            {
                File.Delete(savePath);
                return;
            }
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }

    public void Delete()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            File.Delete(string.Concat(Application.persistentDataPath, savePath));
            Debug.Log("Save deleted");
        }
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[16];
}
[System.Serializable]
public class InventorySlot
{
    public UserInterface parent;
    public int ID;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
        if(amount == 0)
        {
            ID = -1;
            item = null;
        }
    }
}