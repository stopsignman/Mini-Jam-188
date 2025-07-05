using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public float mouseSensitivity;
    public float volume;

    public class SaveData
    {
        public float mouseSensitivity;
        public float volume;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.mouseSensitivity = mouseSensitivity;
        data.volume = volume;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            mouseSensitivity = data.mouseSensitivity;
            volume = data.volume;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadGame();
    }
    public void GoToLevel()
    {
        SaveGame();
        SceneManager.LoadScene(1);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void QuitGame()
    {
        SaveGame();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
