using UnityEngine;

public class LuckyBox : Interactable
{
    public GameObject[] events = {};
    public int index = 0;
    public bool opened = false;
    public GameObject confettiPrefab;

    public override void OnInteract(GameObject player)
    {
        if (!opened)
        {
            OpenBox();
        }

    }

    private void OpenBox()
    {
        opened = true;
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        Instantiate(events[index], spawnPos, events[index].transform.rotation);
        Instantiate(confettiPrefab, transform.position, confettiPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
