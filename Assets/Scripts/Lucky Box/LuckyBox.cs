using UnityEngine;

public class LuckyBox : Interactable
{
    public GameObject[] events = {};
    public int index = 0;
    

    public override void OnInteract(GameObject player)
    {
        OpenBox();
    }

    private void OpenBox()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        Instantiate(events[index], spawnPos, events[index].transform.rotation);
    }
}
