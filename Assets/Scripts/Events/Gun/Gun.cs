using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : Item
{
    public GameObject bulletPrefab;
    private Bullet bullet;
    public float cooldownTime = 1f;
    private bool canShoot = true;
    public int numBullets = 50;
    public int magSize = 12;
    public int curNumBullets = 12;
    public GameObject bulletUI;
    public TextMeshProUGUI currentBulletText;
    public TextMeshProUGUI maxBulletText;

    public override void OnInteract(GameObject player)
    {
        base.OnInteract(player);
        bulletUI.SetActive(true);
    }
    public override void Detach(Vector3 groundPoint)
    {
        base.Detach(groundPoint);
        bulletUI.SetActive(false);
    }
    public override void UseItem()
    {
        if (canShoot && curNumBullets > 0)
        {
            bullet = Instantiate(bulletPrefab, transform.position + (transform.right * 0.5f), bulletPrefab.transform.rotation).GetComponent<Bullet>();
            bullet.playerBullet = true;
            curNumBullets -= 1;
            canShoot = false;
            StartCoroutine(ShootCooldown());
        }
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    private void Update()
    {
        base.HandleInput();
        currentBulletText.text = curNumBullets.ToString();
        maxBulletText.text = numBullets.ToString();  

        if (numBullets > 0 && Input.GetKeyDown(KeyCode.R))
        {
            int bulletsToAdd = Mathf.Clamp(numBullets, 0, magSize - curNumBullets);
            curNumBullets += bulletsToAdd;
            numBullets -= bulletsToAdd;
        } 
    }
}
