using UnityEngine;

public class Damager : MonoBehaviour
{
    public float blow = 10f;
    public void OnAttack(Damagee damagee)
    {
        damagee.TakeDamage(blow);
    }
}
