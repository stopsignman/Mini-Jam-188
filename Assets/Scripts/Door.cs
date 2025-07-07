using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAmount;
    private float curOpenAmount = 0f;
    public LuckyBox[] boxesToOpen;
    public float openSpeed;
    private bool doorOpened = false;
    private bool canOpen = false;

    private void Update()
    {
        canOpen = true;
        foreach (LuckyBox box in boxesToOpen)
        {
            if (!box.opened)
            {
                canOpen = false;
            }
        }
        if (canOpen)
        {
            if (!doorOpened)
            {
                if (OpenDoor() != false)
                {
                    doorOpened = true;
                    Destroy(this);
                }
            }
        }
    }

    private bool OpenDoor()
    {
        gameObject.transform.Translate(-transform.up * openSpeed * Time.deltaTime);
        curOpenAmount += openSpeed * Time.deltaTime;
        if (curOpenAmount > openAmount)
        {
            return true;
        }
        return false;
    }

}
