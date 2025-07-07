using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Gun gun = null;
    public AudioClip deathSound;
    public AudioClip winSound;
    public Image whiteImage;
    private bool fading = false;
    public float fadeSpeed = 1f;
    public TextMeshProUGUI eventText;
    public int curBoxesOpened = 0;
    public TextMeshProUGUI boxText;

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

    public void OnWin()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(winSound, 2);
        fading = true;
        StartCoroutine(WaitForFade());
    }

    private void Update()
    {
        if (fading)
        {
            whiteImage.color = new Color(1, 1, 1, whiteImage.color.a + (fadeSpeed * Time.deltaTime));
        }
        boxText.text = curBoxesOpened.ToString();
    }

    public void OnBoxOpen(string text)
    {
        eventText.text = text;
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(13);
        SceneManager.LoadScene(2);
    }
}
