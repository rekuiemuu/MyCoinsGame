/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public ItemStorage itemStorage;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                InventoryRenderer.Instance.OpenInventory(itemStorage, true);
            }
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public ItemStorage itemStorage;
    public GameObject tooltipPrefab;

    private GameObject tooltip;
    private bool isPlayerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;

            // Создаем экземпляр подсказки, если он не существует
            if (tooltip == null)
            {
                tooltip = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
                tooltip.transform.SetParent(transform); // Привязываем подсказку к объекту

                // Позиционируем подсказку выше объекта
                tooltip.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // Уничтожаем подсказку, если она существует
            if (tooltip != null)
            {
                Destroy(tooltip);
                tooltip = null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                InventoryRenderer.Instance.OpenInventory(itemStorage, true);
            }
        }
    }

    void Update()
    {
        if (isPlayerNearby && tooltip == null)
        {
            // Создаем экземпляр подсказки, если он не существует
            tooltip = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
            tooltip.transform.SetParent(transform); // Привязываем подсказку к объекту

            // Позиционируем подсказку выше объекта
            tooltip.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        }
        else if (!isPlayerNearby && tooltip != null)
        {
            // Уничтожаем подсказку, если она существует
            Destroy(tooltip);
            tooltip = null;
        }
    }
}
