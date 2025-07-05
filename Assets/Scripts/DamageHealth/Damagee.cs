using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Damagee : MonoBehaviour
{
    public float health;
    [System.NonSerialized]
    public float maxHealth;

    private void Start()
    {
        maxHealth = health;
    }
    public void TakeDamage(float blow)
    {
        health -= blow;
        if (health <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                EnemyAnimator anim = gameObject.GetComponent<EnemyAnimator>();
                anim.OnDeath();
                Destroy(gameObject.GetComponent<Enemy>());
                Destroy(gameObject.GetComponent<NavMeshAgent>());
                Debug.Log("waiting apparently");
                StartCoroutine(WaitForAnimation());
                return;
            }
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
