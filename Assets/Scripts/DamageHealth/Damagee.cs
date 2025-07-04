using UnityEngine;

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
            Destroy(gameObject);
        }
    }
}
