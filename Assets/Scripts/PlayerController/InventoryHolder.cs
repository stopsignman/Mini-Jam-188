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
    public float dropHeight = 1f;

    public void AddToInventory(GameObject item)
    {
        if (heldItems.Count >= maxHeldItems)
        {
            Debug.Log("Inventory full!");
        }
        else
        {
            heldItems.Add(item);
            item.GetComponent<BoxCollider>().enabled = false;
            item.SetActive(false);
            HoldItem(heldItems.Count - 1);
        }
    }

    private void HoldItem(int index)
    {
        if (heldItem != null)
        {
            heldItem.SetActive(false);
            heldItem.GetComponent<Item>().held = false;
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
            heldIndex = index;
            itemScript.held = true;
            itemScript.FollowPlayer();
            UpdateUI();
        }
    }

    private void DropHeldItem()
    {
        // dont ask me why this works
        dropHeight = 1f;
        Transform orientation = transform.GetChild(1).gameObject.transform;
        Ray for_ray = new Ray(transform.position, orientation.right);
        Vector3 for_point = for_ray.GetPoint(1f);
        Ray down_ray = new Ray(for_point, Vector3.down);
        Vector3 point = down_ray.GetPoint(dropHeight * 0.5f);
        heldItem.GetComponent<Item>().Detach(point);
        heldItem.GetComponent<Item>().held = false;
        heldItems.RemoveAt(heldIndex);
        heldItem = null;
        if (heldItems.Count >= 1)
        {
            HoldItem(0);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = HideUI();
        ui.SetActive(true);

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = null;
            slots[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

        if (heldItems.Count != 0)
        {
           for (int i = 0; i < heldItems.Count; i++)
            {
                Sprite image = heldItems[i].GetComponent<Item>().inventorySprite;

                slots[i].GetComponent<Image>().sprite = image;
                slots[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            } 
        } 
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

        if (Input.GetKeyDown(KeyCode.Q) && heldItem != null)
        {
            DropHeldItem();
        }
    }

    private void Update()
    {
        HandleInput();
    }
}
