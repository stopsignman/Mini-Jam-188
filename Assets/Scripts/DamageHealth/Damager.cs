using UnityEngine;

public class Damager : MonoBehaviour
{
    public float blow = 10f;
    public void OnAttacK(Damagee damagee)
    {
        damagee.TakeDamage(blow);
    }
}
