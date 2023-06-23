using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStorage : MonoBehaviour
{
    public List<ItemInventory> items = new List<ItemInventory>();
    public InventoryRenderer inventoryRenderer;
    public float maxWeight;

    void Start()
    {
        while (items.Count < 49)
        {
            ItemInventory item = new ItemInventory();
            item.id = 0;
            item.count = 0;
            items.Add(item);
        }
    }

    public void SetItems(List<ItemInventory> newItems)
    {
        items = newItems;
    }

    public List<ItemInventory> GetItems()
    {
        return items;
    }

    public void SearchForSameItem(int id, int count)
    {
        // ищем наш предмет в инвентаре, чтобы новые предметы адекватно добавлялись
        Item item = inventoryRenderer.data.items[id];
        for (int i = 0; i < inventoryRenderer.maxCount / 2; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < 64)
                {
                    items[i].count += count;

                    if (items[i].count > 64)
                    {
                        count = items[i].count - 64;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = inventoryRenderer.maxCount / 2;
                    }
                }
            }
        }
        // добавляем предмет в ячейку
        if (count > 0)
        {
            for (int i = 0; i < inventoryRenderer.maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = inventoryRenderer.maxCount;
                }
            }
        }
    }

    // добавляем предмет класса item в инвентарь
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
    }

    // добавляем предмет класса iteminventory в инвентарь
    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = inventoryRenderer.data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
    public int count;
}

