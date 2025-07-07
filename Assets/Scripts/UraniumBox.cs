using UnityEngine;

public class UraniumBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.OnWin();
        }
    }
}
