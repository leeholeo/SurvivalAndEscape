using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Water,
    Medicine,
    Wood,
    Stone,
    Metal,
    Electronics,
    Material,
}

public enum ItemState
{
    Rotten,
}

public abstract class ItemObject : ScriptableObject
{
    public int MaxStackSize;
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public int restoreHungerValue;
    public int restoreThirstValue;
    public ItemStateMaker[] states;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public int MaxStackSize;
    public string Name;
    public int Id;
    public ItemStateMaker[] states;
    public Item(ItemObject item)
    {
        MaxStackSize = item.MaxStackSize;
        Name = item.name;
        Id = item.Id;
        states = new ItemStateMaker[item.states.Length];
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = new ItemStateMaker(item.states[i].min, item.states[i].max);
            {
                states[i].itemState = item.states[i].itemState;
            }
        }
    }
}

[System.Serializable]
public class ItemStateMaker
{
    public ItemState itemState;
    public int value;
    public int min;
    public int max;
    public ItemStateMaker(int _min, int _max)
    {
        min = _min;
        max = _max;
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}