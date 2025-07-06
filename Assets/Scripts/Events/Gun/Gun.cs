using UnityEngine;

public class Gun : Item
{
    public GameObject bulletPrefab;
    private Bullet bullet;
    public override void UseItem()
    {
        bullet = Instantiate(bulletPrefab, transform.position + (transform.right * 0.5f), bulletPrefab.transform.rotation).GetComponent<Bullet>();
        bullet.playerBullet = true;
    }
}
