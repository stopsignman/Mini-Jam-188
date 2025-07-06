using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float blow = 10f;
    public bool playerBullet = true;
    private Rigidbody rb;
    public float damageMultiplier = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerBullet)
        {
            Transform orientation = GameObject.Find("Orientation").transform;
            Transform x_orientation = GameObject.Find("X Orientation").transform;
            Camera playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
            // stupid math i hate ts
            Ray screenRay = playerCam.ScreenPointToRay(new Vector3(0.5f, 0.5f));
            Vector3 forwardPoint = screenRay.GetPoint(100f);
            float xForce = (transform.position.x - forwardPoint.x) * 0.001f;
            rb.AddForce(new Vector3(orientation.forward.x - xForce, x_orientation.forward.y * 2, orientation.forward.z) * 1000f);
            rb.AddTorque(new Vector3(0, 100f, 0));
        }
        StartCoroutine(DestroyBullet());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && playerBullet)
        {
            collision.gameObject.GetComponent<Damagee>().TakeDamage(blow * damageMultiplier);
            Destroy(transform.GetChild(0).GetComponent<MeshRenderer>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            StartCoroutine(WaitForTrail());
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    IEnumerator WaitForTrail()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
