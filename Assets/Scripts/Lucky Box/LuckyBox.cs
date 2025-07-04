using UnityEngine;

public class LuckyBox : MonoBehaviour
{
    public GameObject[] events = {};
    private int index = 0;

    private void Start()
    {
        index = Random.Range(0, events.Length);
        OpenBox();
    }

    private void OpenBox()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
        Instantiate(events[index], spawnPos, events[index].transform.rotation);
    }
}
