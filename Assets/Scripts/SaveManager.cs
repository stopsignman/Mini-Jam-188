using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public float mouseSensitivity = 400f;
    public bool gamePaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
