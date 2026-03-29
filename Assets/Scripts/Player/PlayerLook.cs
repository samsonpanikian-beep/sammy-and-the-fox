using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform lookAtPoint;
    
    Vector3 lookAtPos;
    Vector3 moveVelocityVector;

    float horisontalMovementInputRaw;
    float lookVelocity;
    [SerializeField] float moveForwardAmount;

    public float dampAmount;
    float dampVelocity;

    float targetAngle;
    float currentAngle;
    float rotationVelocity;

    float stopVelocityX;
    float stopVelocityZ;

    float rotVelocityDamp;

    private void Update()
    {
        transform.LookAt(lookAtPoint);

        Inputs();
        MoveLookPoint();
    }

    void Inputs()
    {
        horisontalMovementInputRaw = Input.GetAxisRaw("Horizontal");
    }

    void MoveLookPoint()
    {
        if (playerMovement.canTurn)
        {
            if (horisontalMovementInputRaw != 0)
            {
                dampAmount = Mathf.SmoothDamp(dampAmount, 1f, ref dampVelocity, 0.3f);

                targetAngle = transform.eulerAngles.y + horisontalMovementInputRaw * 30f;
                currentAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, 0.4f);
                transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);

                if (!playerMovement.movingVertically)
                {
                    rb.linearVelocity = new Vector3(transform.forward.x * moveForwardAmount * dampAmount, rb.linearVelocity.y, transform.forward.z * moveForwardAmount * dampAmount);
                }
            }
            else
            {
                // Decay the rotation velocity directly, then apply it
                rotationVelocity = Mathf.SmoothDamp(rotationVelocity, 0f, ref rotVelocityDamp, 0.15f);
                currentAngle = transform.eulerAngles.y + rotationVelocity * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);

                if (!playerMovement.movingVertically)
                {
                    rb.linearVelocity = new Vector3(
                    Mathf.SmoothDamp(rb.linearVelocity.x, 0f, ref stopVelocityX, 0.15f),
                    rb.linearVelocity.y,
                    Mathf.SmoothDamp(rb.linearVelocity.z, 0f, ref stopVelocityZ, 0.15f)
                    );
                }
            }
        }
    }
}
