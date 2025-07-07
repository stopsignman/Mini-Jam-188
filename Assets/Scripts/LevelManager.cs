using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Gun gun = null;
    public AudioClip deathSound;

    void Awake()
    {
        Instance = this;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void OnDeath()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound, 2);
    }
}
