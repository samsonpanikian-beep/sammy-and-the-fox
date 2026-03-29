using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    float downForce;

    Vector3 moveDirection;

    [SerializeField] Transform groundCheck1;
    [SerializeField] Transform groundCheck2;

    float verticalMovementInputRaw;

    bool jumpInput;
    bool sprintInput;

    public bool isGrounded;
    public bool canTurn;
    public bool movingVertically;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();
        Inputs();
        Move();
    }

    void GroundCheck()
    {
        RaycastHit hitInfo1;
        RaycastHit hitInfo2;

        bool check1;
        bool check2;

        check1 = Physics.SphereCast(groundCheck1.position, 0.01f, Vector3.down, out hitInfo1);
        check2 = Physics.SphereCast(groundCheck2.position, 0.01f, Vector3.down, out hitInfo2);

        if (!check1 && !check2)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Inputs()
    {
        verticalMovementInputRaw = Input.GetAxisRaw("Vertical");

        jumpInput = Input.GetKeyDown(KeyCode.Space);
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        if(verticalMovementInputRaw == -1f)
        {
            canTurn = false;
        }
        else { canTurn = true; }

        if (verticalMovementInputRaw != 0f)
        {
            movingVertically = true;
        }
        else {  movingVertically = false;  }
    }

    private void Move()
    {
        if (sprintInput) { moveSpeed = 23f; }
        else { moveSpeed = 17f; }

        Vector3 moveDirection = (transform.forward * verticalMovementInputRaw).normalized;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed), 1f * Time.deltaTime);

        if (isGrounded)
        {
            rb.linearDamping = 0f;
            downForce = -1f;

            if (jumpInput)
            {
                rb.AddForce(0, jumpForce, 0);
            }
        }
        else
        {
            rb.linearDamping = 0.1f;

            rb.AddForce(0, downForce, 0);
            downForce -= 0.05f;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck1.position, 0.01f);
        Gizmos.DrawSphere(groundCheck2.position, 0.01f);

        Gizmos.color = Color.yellow;
    }
}
