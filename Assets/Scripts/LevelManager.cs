using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Gun gun = null;

    void Awake()
    {
        Instance = this;   
    }
}
