using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public List<GameObject> heldItems;
    private int maxHeldItems = 5;
    public GameObject heldItem;
    public int heldIndex = 0;
    public GameObject ui;

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
            HoldItem(heldItems.Count-1);
        }
    }

    private void HoldItem(int index)
    {
        GameObject itemToHold = heldItems[index];
        Item itemScript = itemToHold.GetComponent<Item>();
        if (itemScript == null)
        {
            Debug.LogError("Cannot hold something that isn't an item!");
        }
        else
        {
            itemToHold.SetActive(true);
            itemScript.held = true;
            itemScript.FollowPlayer();
        }

    }

    private void DropHeldItem()
    {

    }

    private void UpdateUI()
    {

    }

    private void HandleInput()
    {

    }

    private void Update()
    {
        HandleInput();   
    }
}
