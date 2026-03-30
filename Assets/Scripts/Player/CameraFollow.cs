using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform foxTarget;

    [SerializeField] ClimbMechanic climbMechanic;

    Transform focusTarget;
    float focusBlend;
    float focusVelocity;
    bool isFocusing;

    [SerializeField] float regularDistance;
    [SerializeField] float regularHeight;
    [SerializeField] float mountedDistance;
    [SerializeField] float mountedHeight;

    private float currentYAngle;

    private float rotationVelocity;

    private void LateUpdate()
    {
        // Blend between fox and focus target smoothly
        float targetBlend = isFocusing ? 1f : 0f;
        focusBlend = Mathf.SmoothDamp(focusBlend, targetBlend, ref focusVelocity, 0.5f);

        Vector3 lookTarget;
        if (!climbMechanic.foxIsMounted)
        {
            lookTarget = Vector3.Lerp(
            foxTarget.position + Vector3.up * regularHeight * 0.5f,
            focusTarget != null ? focusTarget.position : foxTarget.position, focusBlend);
        }
        else
        {
            lookTarget = Vector3.Lerp(
            foxTarget.position + Vector3.up * mountedHeight * 0.5f,
            focusTarget != null ? focusTarget.position : foxTarget.position, focusBlend);
        }
        

        // Adjust y rotation
        float foxAngle = foxTarget.eulerAngles.y;
        float focusAngle = foxAngle;
        if(focusTarget != null)
        {
            Vector3 directionToFocus = focusTarget.position - foxTarget.position;
            focusAngle = Quaternion.LookRotation(directionToFocus).eulerAngles.y;
        }
        float targetAngle = Mathf.LerpAngle(foxAngle, focusAngle, focusBlend);
        currentYAngle = Mathf.SmoothDampAngle(currentYAngle, targetAngle, ref rotationVelocity, 0.6f);

        // Calculate position
        Quaternion rotation = Quaternion.Euler(0f, currentYAngle, 0f);

        Vector3 offset;
        if (!climbMechanic.foxIsMounted)
        { offset = rotation * new Vector3(0f, regularHeight, -regularDistance); }
        else { offset = rotation * new Vector3(0f, mountedHeight, -mountedDistance); }

        transform.position = foxTarget.position + offset;

        // Always look at fox
        if(!climbMechanic.foxIsMounted) { transform.LookAt(lookTarget); }
        else { transform.LookAt(lookTarget + Vector3.down * 5f); }
        
    }

    public void FocusOn(Transform target)
    {
        focusTarget = target;
        isFocusing = true;
    }

    public void ReturnToFox()
    {
        isFocusing = false;
    }
}
