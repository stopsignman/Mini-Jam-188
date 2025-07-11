using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Damagee : MonoBehaviour
{
    public float health;
    [System.NonSerialized]
    public float maxHealth;
    public GameObject deathUI;
    public AudioClip[] damageSounds;
    public GameObject explosionPrefab;

    private void Start()
    {
        maxHealth = health;
    }
    public void TakeDamage(float blow)
    {
        health -= blow;
        if (gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(damageSounds[0], 1);
        }
        if (gameObject.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().PlayOneShot(damageSounds[0], 1);
        }
        
        if (health <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                EnemyAnimator anim = gameObject.GetComponent<EnemyAnimator>();
                anim.OnDeath();
                Destroy(gameObject.GetComponent<Enemy>());
                Destroy(gameObject.GetComponent<NavMeshAgent>());
                gameObject.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezeAll;
                gameObject.GetComponent<AudioSource>().PlayOneShot(damageSounds[1], 2);
                StartCoroutine(WaitForAnimation());
                return;
            }
            if (gameObject.CompareTag("Player"))
            {
                Camera playerCam = transform.GetChild(0).gameObject.GetComponent<Camera>();
                Destroy(playerCam.gameObject.GetComponent<FirstPersonCamera>());
                Destroy(gameObject.GetComponent<FirstPersonPlayer>());
                Destroy(gameObject.GetComponent<Interactor>());
                Ray down_ray = new Ray(playerCam.transform.position, Vector3.down);
                Vector3 down_point = down_ray.GetPoint(1.2f);
                playerCam.gameObject.transform.SetParent(null);
                playerCam.gameObject.transform.position = down_point;
                playerCam.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                gameObject.GetComponent<FirstPersonPlayer>().healthBar.transform.parent.gameObject.SetActive(false);
                LevelManager.Instance.OnDeath();
                StartCoroutine(DelayDeath());
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMenU()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathUI.SetActive(true);
    }

    IEnumerator Knockback(NavMeshAgent agent)
    {
        yield return new WaitForSeconds(2);
        agent.enabled = true;
    }
}
