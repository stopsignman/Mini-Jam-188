using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public float openAmount;
    private float curOpenAmount = 0f;
    public LuckyBox openBox;
    public float openSpeed;
    private bool doorOpened = false;

    private void Update()
    {
        if (openBox.opened)
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
        leftDoor.transform.Translate(-transform.right * openSpeed * Time.deltaTime);
        rightDoor.transform.Translate(transform.right * openSpeed * Time.deltaTime);
        curOpenAmount += openSpeed * Time.deltaTime;
        if (curOpenAmount > openAmount)
        {
            return true;
        }
        return false;
    }

}
