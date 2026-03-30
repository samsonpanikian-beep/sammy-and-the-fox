using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    [SerializeField] Transform waypointsParent;
    public Transform[] waypoints;

    public int currentWaypointIndex = 0;

    Vector3 smoothDampPosVelocity;

    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float distanceToWaypointThreshold;

    public bool canMove = true;

    private void Start()
    {
        waypoints = new Transform[waypointsParent.childCount];

        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }
    }

    private void Update()
    {
        LookAtWaypoint();
        MoveTowardWaypoint();
        CheckWaypointReached();
    }

    void LookAtWaypoint()
    {
        if (canMove)
        {
            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position);
            direction.y = 0f;

            if (direction == Vector3.zero) { return; }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void MoveTowardWaypoint()
    {
        if (canMove)
        {
            Vector3 target = new Vector3(waypoints[currentWaypointIndex].position.x, transform.position.y, waypoints[currentWaypointIndex].position.z);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref smoothDampPosVelocity, moveSpeed);
        }
    }

    void CheckWaypointReached()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

        if(distanceToWaypoint < distanceToWaypointThreshold && currentWaypointIndex != waypoints.Length - 1)
        {
            currentWaypointIndex++;
        }
    }
}
