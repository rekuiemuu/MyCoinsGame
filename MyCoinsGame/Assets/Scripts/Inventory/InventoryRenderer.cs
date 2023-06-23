using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryRenderer : MonoBehaviour
{
    public ItemsDatabase data;
    public List<ItemInventory> items = new List<ItemInventory>();
    public GameObject gameObjShow;
    public GameObject InventoryMainObject;
    public GameObject InventorySecondObject;
    public int maxCount;
    public Camera cam;
    public EventSystem es;
    public int currentID;
    public ItemInventory currentItem;
    public RectTransform movingObject;
    public Vector3 offset;
    public GameObject inventoryBackground;
    public GameObject chestBackground;
    public static InventoryRenderer Instance;
    private bool isSingleSelection;
    public ItemStorage currentStorage;
    public static bool inventoryIsOpen;
    public ItemStorage itemStorage;
    public ItemStorage playerStorage;
    private float currentStorageMaxWeight;
    public Text volumeText;
    private float currentWeight;
    private bool storageIsFull;

    public void Start()
    {
        Instance = this;
        inventoryIsOpen = false;
        currentStorageMaxWeight = 49f;

        if (items.Count == 0)
        {
            AddGraphics();
        }
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryIsOpen)
            { 
                CloseInventory();
            }
            else
            {
                OpenInventory(itemStorage, false);
            }
        }
        currentWeight = 0;
        for (int i = 49; i < 98; i++)
        {
            currentWeight += data.items[items[i].id].weight * items[i].count;
        }
        volumeText.text = "Volume: " + currentWeight.ToString() + "/" + currentStorageMaxWeight.ToString();

        if (currentItem != null && es.currentSelectedGameObject != null)
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if ((data.items[currentItem.id].weight * currentItem.count + currentWeight > currentStorageMaxWeight
            && currentID != -1 && int.Parse(es.currentSelectedGameObject.name) > 48)
            || (currentID > 35 && data.items[II.id].weight * II.count > currentStorageMaxWeight - currentWeight
            && int.Parse(es.currentSelectedGameObject.name) < 49))
            {
                storageIsFull = true;
            }
            else
            {
                storageIsFull = false;
            }
        }
    }

    public void OpenInventory(ItemStorage itemStorage, bool isSecondInventory)
    {
        List<ItemInventory> newItems = itemStorage.GetItems();
        List<ItemInventory> playerItems = playerStorage.GetItems();
        int length = items.Count / 2;
        int startIndex;

        if (isSecondInventory)
        {
            startIndex = items.Count - length;

            for (int i = startIndex; i < length + startIndex; i++)
            {
                items[i].id = newItems[i - startIndex].id;
                items[i].count = newItems[i - startIndex].count;
            }

            currentStorage = itemStorage;
            currentStorageMaxWeight = itemStorage.maxWeight;
            volumeText.text = "Volume: " + currentWeight.ToString() + "/" + currentStorageMaxWeight.ToString();
        }
        else
        {
            currentStorage = null;
        }

        for (int i = 0; i < 49; i++)
        {
            items[i].id = playerItems[i].id;
            items[i].count = playerItems[i].count;
        }

        inventoryBackground.SetActive(!inventoryBackground.activeSelf);
        chestBackground.SetActive(isSecondInventory);

        if (inventoryBackground.activeSelf)
        {
            UpdateInventory();
        }

        Time.timeScale = inventoryBackground.activeSelf ? 0f : 1f;
        inventoryIsOpen = true;
    }

        public void CloseInventory()
    {
        if (currentID != -1)
        {
            if (isSingleSelection)
            {
                currentItem.count += items[currentID].count;
                isSingleSelection = false;
            }
            AddInventoryItem(currentID, currentItem);
            currentID = -1;
            movingObject.gameObject.SetActive(false);
            currentItem.count = 0;
            currentItem.id = 0;
        }

        List<ItemInventory> newItems = new List<ItemInventory>();
        List<ItemInventory> playerItems = new List<ItemInventory>();
        for (int i = 0; i < 49; i++)
        {
            ItemInventory newItem = new ItemInventory();
            newItem.id = items[i + 49].id;
            newItem.count = items[i + 49].count;
            newItems.Add(newItem);
        }
        for (int i = 0; i < 49; i++)
        {
            ItemInventory newItem = new ItemInventory();
            newItem.id = items[i].id;
            newItem.count = items[i].count;
            playerItems.Add(newItem);
        }
        if (currentStorage != null)
        {
            currentStorage.SetItems(newItems);
            currentStorageMaxWeight = 49;
        }
        playerStorage.SetItems(playerItems);

        inventoryBackground.SetActive(!inventoryBackground.activeSelf);

        if (inventoryBackground.activeSelf)
        {
            UpdateInventory();
        }
        else
        {
            chestBackground.SetActive(false);
        }

        Time.timeScale = inventoryBackground.activeSelf ? 0f : 1f;

        inventoryIsOpen = false;
    }

    public void SearchForSameItem(int id, int count)
    {
        // ищем наш предмет в инвентаре, чтобы новые предметы адекватно добавлялись
        Item item = data.items[id];
        for (int i = 0; i < maxCount / 2; i++)
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
                        i = maxCount;
                    }
                }
            }
        }
        // добавляем предмет в ячейку
        if (count > 0)
        {
            for (int i = 0; i < maxCount / 2; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    // добавляем предмет класса item в инвентарь
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    // добавляем предмет класса iteminventory в инвентарь
    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    // добавляем префабы кнопок в инвентарь при старте
    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem;

            if (i < maxCount / 2)
            {
                newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;
            }
            else
            {
                newItem = Instantiate(gameObjShow, InventorySecondObject.transform) as GameObject;
            }

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }

    // обновляем текст количества у предметов в инвентаре и картинку
    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
    }

    // при нажатии на предмет в инвентаре
    public void SelectObject()
    {
        if (storageIsFull)
        {
            return; 
        }
        if (currentID == -1 && items[int.Parse(es.currentSelectedGameObject.name)].id == 0)
        {
            return;
        }
        // если предмет не выбран, мы его выбираем
        if (currentID == -1 && !Input.GetKey(KeyCode.LeftControl))
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            // заменяем наш выбранный объект на пустую ячейку
            AddItem(currentID, data.items[0], 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];
            if (II.count < 1)
            {
                return;
            }

            if (currentItem.id != II.id && currentItem.id != 0)
            {
                return;
            }

            if (currentItem.count >= 64)
            {
                return;
            }

            currentID = int.Parse(es.currentSelectedGameObject.name);
            if (currentItem.id == 0)
            {
                currentItem = CopyInventoryItem(items[currentID]);
                currentItem.count = 1;
            }
            else
            {
                currentItem.count++;
            }

            II.count--;
            if (II.count > 1)
            {
                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }
            else
            {
                II.itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            if (II.count == 0)
            {
                AddItem(currentID, data.items[0], 0);
            }
            isSingleSelection = true;
        }
        else
        {
            // если предмет выбран и мы хотим сложить его в другой слот
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {
                // если предметы разные, меняем местами
                if (!isSingleSelection)
                {
                    AddInventoryItem(currentID, II);
                    AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (II.id != 0)
                    {
                        currentItem.count += items[currentID].count;

                        AddInventoryItem(currentID, II);
                        AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                    }
                    else
                    {
                        AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                    }
                }

            }
            else
            {
                // если предметы одинаковые, то складываем их в одну ячейку (до 64)
                if (II.count + currentItem.count <= 64)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - 64);
                    II.count = 64;
                }

                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }
            currentID = -1;

            movingObject.gameObject.SetActive(false);
            currentItem.count = 0;
            currentItem.id = 0;
            isSingleSelection = false;
        }

        //
        if (currentItem.count != 0 && currentItem.count > 1)
        {
            movingObject.GetComponentInChildren<Text>().text = currentItem.count.ToString();
        }
        else
        {
            movingObject.GetComponentInChildren<Text>().text = "";
        }
    }

    // двигаем нашу картинку с предметом
    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z + 10f;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();
        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;
        return New;
    }
}