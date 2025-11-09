using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5.0f;
    public float declaration = 5.0f;
    public float maxSpeed;
    public float maxWalkSpeed = 3.0f;
    public float maxRunSpeed = 7.0f;
    public float jumpForce = 7.0f;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;


    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveInput;
    private bool isRunning;
    private bool jumpRequest;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = camRight.y = 0f; // 위쪽 기울기 제거
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * z + camRight * x;
        moveInput = moveDir.normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            anim.SetBool("IsRun", true);
        }
        else
        {
            isRunning = false;
            anim.SetBool("IsRun", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }
        
        if (isGrounded)
        {
            anim.SetBool("IsJump", false);
        }
        else
        {
            anim.SetBool("IsJump", true);
        }

    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        Vector3 velocity = rb.linearVelocity;
        if (isRunning)
            maxSpeed = maxRunSpeed;
        else
            maxSpeed = maxWalkSpeed;
        Vector3 targetVelocity = moveInput * maxSpeed;
        

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        Vector3 velocityChange = targetVelocity - horizontalVelocity; 

        float accel = moveInput.magnitude > 0 ? acceleration : declaration;

        horizontalVelocity += velocityChange * accel * Time.fixedDeltaTime;
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxSpeed);

        rb.linearVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);

        anim.SetFloat("Speed", horizontalVelocity.magnitude);

        if (jumpRequest)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequest = false;
        }

        
    }
}
