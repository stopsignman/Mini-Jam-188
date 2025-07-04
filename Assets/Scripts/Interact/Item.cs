using UnityEngine;

public class Item : Interactable
{
    public override void OnInteract()
    {
        Destroy(gameObject);
    }
}
