using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 15;

    private WaypointPath _waypointPath;
    private Vector3 _patrolTargetPosition;

    private Vector3 _direction;

    // Awake is called before Start
    private void Awake()
    {
        _waypointPath = GetComponentInChildren<WaypointPath>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (_waypointPath)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();


            _direction = _patrolTargetPosition - transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!_waypointPath) return;

        //set our direction toward that waypoint:
        //subtracting our position from target position
        //gives us the slope line between the two
        //We can get direction by normalizing it
        //We can get distance by magnitude

        //if we are close enough to the target,
        //time to get the next waypoint


        RaycastHit hit;

        //Grounded is the position on the relative ground the spider should be
        Vector3 grounded = transform.position;

        //Designed so hopefully if a RayCast misses it still kind of works
        if (Physics.Raycast(transform.position, -transform.up + Vector3.Scale(transform.forward, new Vector3(Time.deltaTime * patrolSpeed, Time.deltaTime * patrolSpeed, Time.deltaTime * patrolSpeed)), out hit, 1000f))
        {
            //A RayCast is shot down and at an angle forward, and the spider is put at that location
            grounded = hit.point + hit.normal * (transform.localScale.y / 2);

            //Direction towards the next point
            _direction = _patrolTargetPosition - transform.position;

            //Since the spider follows a plane, we can project the point onto the plane it is on to ignore the "y" axis
            Vector3 planeDir = Vector3.ProjectOnPlane(_direction, hit.normal).normalized;

            //Point towards the waypoint
            Quaternion targetRotation = Quaternion.LookRotation(planeDir, hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30f);

        }


        transform.position = grounded;

        //If close enough, get next point
        if ((_patrolTargetPosition - transform.position).magnitude < .3)
        {
            //get next waypoint
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            //change direction
            _direction = _patrolTargetPosition - transform.position;
        }


    }


}
