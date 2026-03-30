using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    [SerializeField] Transform zone1WaypointsParent;
    [SerializeField] Transform zone2WaypointsParent;
    [SerializeField] Transform zone3WaypointsParent;
    [SerializeField] Transform zone4WaypointsParent;
    [SerializeField] Transform zone5WaypointsParent;

    public Transform[] zone1Waypoints;
    public Transform[] zone2Waypoints;
    public Transform[] zone3Waypoints;
    public Transform[] zone4Waypoints;
    public Transform[] zone5Waypoints;

    public Transform[] activeZoneWaypoints;

    public int currentWaypointIndex = 0;

    Vector3 smoothDampPosVelocity;

    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float distanceToWaypointThreshold;

    public bool canMove = true;

    private void Start()
    {
        zone1Waypoints = new Transform[zone1WaypointsParent.childCount];
        for (int i = 0; i < zone1WaypointsParent.childCount; i++)
        {
            zone1Waypoints[i] = zone1WaypointsParent.GetChild(i);
        }

        zone2Waypoints = new Transform[zone2WaypointsParent.childCount];
        for (int i = 0; i < zone2WaypointsParent.childCount; i++)
        {
            zone2Waypoints[i] = zone2WaypointsParent.GetChild(i);
        }
        
        zone3Waypoints = new Transform[zone3WaypointsParent.childCount];
        for (int i = 0; i < zone3WaypointsParent.childCount; i++)
        {
            zone3Waypoints[i] = zone3WaypointsParent.GetChild(i);
        }

        zone4Waypoints = new Transform[zone4WaypointsParent.childCount];
        for (int i = 0; i < zone4WaypointsParent.childCount; i++)
        {
            zone4Waypoints[i] = zone4WaypointsParent.GetChild(i);
        }

        zone5Waypoints = new Transform[zone5WaypointsParent.childCount];
        for (int i = 0; i < zone5WaypointsParent.childCount; i++)
        {
            zone5Waypoints[i] = zone5WaypointsParent.GetChild(i);
        }

        // Initial assignment of active waypoints, done through game manager after that
        activeZoneWaypoints = zone1Waypoints;
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
            Vector3 direction = (activeZoneWaypoints[currentWaypointIndex].position - transform.position);
            
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
            Vector3 target = new Vector3(activeZoneWaypoints[currentWaypointIndex].position.x, transform.position.y, activeZoneWaypoints[currentWaypointIndex].position.z);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref smoothDampPosVelocity, moveSpeed);
        }
    }

    void CheckWaypointReached()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, activeZoneWaypoints[currentWaypointIndex].position);

        if(distanceToWaypoint < distanceToWaypointThreshold && currentWaypointIndex != activeZoneWaypoints.Length - 1)
        {
            currentWaypointIndex++;
        }
    }
}
