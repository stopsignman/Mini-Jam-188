using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // SaveManager.Instance.mouseSensitivity
    private float mouseSensitivity = 400f;
    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private Transform X_orientation;
    private float xRot;
    private float yRot;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CalculateInput();
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
        X_orientation.rotation = Quaternion.Euler(xRot, 0, 0);
    }

    private void CalculateInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * Time.deltaTime * mouseSensitivity;
        xRot -= mouseY * Time.deltaTime * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }
}
