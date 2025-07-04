using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHolder : MonoBehaviour
{
    public List<GameObject> heldItems;
    private int maxHeldItems = 5;
    public GameObject heldItem;
    public int heldIndex = 0;
    public GameObject ui;
    public List<GameObject> slots;
    private IEnumerator hideCoroutine;

    public void AddToInventory(GameObject item)
    {
        if (heldItems.Count >= maxHeldItems)
        {
            Debug.Log("Inventory full!");
        }
        else
        {
            heldItems.Add(item);
            Destroy(item.GetComponent<SphereCollider>());
            item.SetActive(false);
            HoldItem(heldItems.Count - 1);
        }
    }

    private void HoldItem(int index)
    {
        if (heldItem != null)
        {
            heldItem.SetActive(false);
        }
        GameObject itemToHold = heldItems[index];
        Item itemScript = itemToHold.GetComponent<Item>();
        if (itemScript == null)
        {
            Debug.LogError("Cannot hold something that isn't an item!");
        }
        else
        {
            itemToHold.SetActive(true);
            heldItem = itemToHold;
            itemScript.held = true;
            itemScript.FollowPlayer();
            UpdateUI(index, itemScript);
        }

    }

    private void DropHeldItem()
    {

    }

    private void UpdateUI(int index, Item heldItem)
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = HideUI();
        ui.SetActive(true);
        Sprite image = heldItem.inventorySprite;

        slots[index].GetComponent<Image>().sprite = image;
        StartCoroutine(hideCoroutine);
    }

    public IEnumerator HideUI()
    {
        yield return new WaitForSeconds(1);
        ui.SetActive(false);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (heldItems.Count >= 1)
            {
                HoldItem(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (heldItems.Count >= 2)
            {
                HoldItem(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (heldItems.Count >= 3)
            {
                HoldItem(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (heldItems.Count >= 4)
            {
                HoldItem(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (heldItems.Count >= 5)
            {
                HoldItem(4);
            }
        }
    }

    private void Update()
    {
        HandleInput();
    }
}
