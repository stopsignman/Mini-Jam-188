using UnityEngine;

public class Gun : Item
{
    public GameObject bulletPrefab;
    private Bullet bullet;
    public Vector3 offset;
    public override void UseItem()
    {
        bullet = Instantiate(bulletPrefab, transform.position + transform.forward, bulletPrefab.transform.rotation).GetComponent<Bullet>();
        bullet.playerBullet = true;
    }
}
