using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentZone;

    [SerializeField] WaypointSystem sammyWaypointSystem;
    GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void AdvanceGameZone()
    {
        currentZone++;

        if (currentZone == 2) { sammyWaypointSystem.activeZoneWaypoints = sammyWaypointSystem.zone2Waypoints; }
        else if (currentZone == 3) { sammyWaypointSystem.activeZoneWaypoints = sammyWaypointSystem.zone3Waypoints; }
        else if (currentZone == 4) { sammyWaypointSystem.activeZoneWaypoints = sammyWaypointSystem.zone4Waypoints; }
        else if (currentZone == 5) { sammyWaypointSystem.activeZoneWaypoints = sammyWaypointSystem.zone5Waypoints; }
        sammyWaypointSystem.currentWaypointIndex = 0;
    }
}
