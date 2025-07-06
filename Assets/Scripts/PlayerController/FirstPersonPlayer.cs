using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstPersonPlayer : MonoBehaviour
{
    private float moveSpeed = 4f;
    private float sprintSpeed = 45f;
    public float moveMultiplier = 1f;
    private bool running = false;
    [SerializeField]
    private Transform orientation;

    [SerializeField]
    private float groundDrag = 5f;
    [SerializeField]
    private float playerHeight;
    [SerializeField]
    public LayerMask whatIsGround;
    private bool grounded;

    private float jumpForce = 6f;
    private float jumpCooldown = .25f;
    private float airMultipler = .4f;
    private bool readyToJump = true;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    public float damageMultiplier = 1f;
    public bool gamePaused = false;
    public GameObject pauseUI;
    public Slider healthBar;
    public GameObject leg;
    private bool canKick = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void ReturnToMenu()
    {
        SaveManager.Instance.ReturnToMenu();
    }
    public void UnPauseGame()
    {
        pauseUI.SetActive(false);
        gamePaused = false;
        SaveManager.Instance.UnPauseGame();
    }
    public void PauseGame()
    {
        pauseUI.SetActive(true);
        gamePaused = true;
        SaveManager.Instance.PauseGame();
    }

    private void CalculateInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
        }
        else
        {
            running = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            if (running)
            {
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f * moveMultiplier, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * moveMultiplier, ForceMode.Force);
            }
        }
        else
        {
            if (running)
            {
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f * airMultipler * moveMultiplier, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultipler * moveMultiplier, ForceMode.Force);
            }

        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
        CalculateInput();
        SpeedControl();
        healthBar.value = gameObject.GetComponent<Damagee>().health;
        if (Input.GetKeyDown(KeyCode.F) && canKick)
        {
            canKick = false;
            leg.GetComponent<Animator>().SetBool("kicking_b", true);
            Damager damager = gameObject.GetComponent<Damager>();
            Camera playerCamera = transform.GetChild(0).gameObject.GetComponent<Camera>();
            var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Physics.Raycast(ray, out var hit, 5f);
            hit.collider?.GetComponent<Damagee>().TakeDamage(damager.blow);
            StartCoroutine(WaitForLeg());
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    IEnumerator WaitForLeg()
    {
        yield return new WaitForSeconds(0.3f);
        leg.GetComponent<Animator>().SetBool("kicking_b", false);
        canKick = true;
    }
}
