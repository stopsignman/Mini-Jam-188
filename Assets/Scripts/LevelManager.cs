using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Gun gun = null;
    public AudioClip deathSound;

    void Awake()
    {
        Instance = this;
    }

    public void OnDeath()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound, 2);
    }
}
