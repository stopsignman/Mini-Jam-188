using UnityEngine;
using UnityEngine.InputSystem;

public class Item : Interactable
{
    public Vector3 holdOffset;
    public Sprite inventorySprite;
    public bool held = false;
    private GameObject playerRef;
    public Quaternion heldRotation = Quaternion.Euler(0, 0, 0);
    public override void OnInteract(GameObject player)
    {
        playerRef = player;
        InventoryHolder inventory = player.GetComponent<InventoryHolder>();
        if (inventory == null)
        {
            Debug.LogError("Player does not have inventory script");
        }
        else
        {
            inventory.AddToInventory(gameObject);
        }
    }

    public virtual void UseItem()
    {
        
    }

    public void FollowPlayer()
    {
        Transform orientation = playerRef.transform.GetChild(1).transform;
        if (orientation == null)
        {
            Debug.LogError("Orientation not found");
        }
        else
        {
            transform.SetParent(playerRef.transform.GetChild(0).transform);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localPosition += holdOffset;
            transform.localRotation = heldRotation;
        }
    }

    public void Detach(Vector3 groundPoint)
    {
        transform.SetParent(null);
        transform.position = groundPoint;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && held)
        {
            UseItem();
        }
    }

    private void Update()
    {
        HandleInput();   
    }
}
