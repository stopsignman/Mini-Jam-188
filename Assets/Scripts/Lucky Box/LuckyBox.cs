using UnityEngine;

public class LuckyBox : Interactable
{
    public GameObject[] events;
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
        LevelManager.Instance.curBoxesOpened++;
        Instantiate(events[index], transform.position, events[index].transform.rotation);
        Instantiate(confettiPrefab, transform.position, confettiPrefab.transform.rotation);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(GetComponent<BoxCollider>());
    }
}
