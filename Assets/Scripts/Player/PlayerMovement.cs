using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] CharacterController playerController;

    [SerializeField] float moveForce;

    float verticalMovementInputRaw;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Inputs();
        Move();
    }

    private void Inputs()
    {
        verticalMovementInputRaw = Input.GetAxisRaw("Vertical");

        Input.GetKeyDown(KeyCode.Space);
    }

    private void Move()
    {
        rb.AddForce(0, 0, verticalMovementInputRaw * moveForce);
    }
}
