using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public bool playerBullet = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 1000f);
        rb.AddTorque(new Vector3(0, 100f, 0));
    }
    private void Update()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
