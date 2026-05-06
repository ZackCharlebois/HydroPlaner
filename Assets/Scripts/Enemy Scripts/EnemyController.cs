using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 1;

    private WaypointPath _waypointPath;
    private Vector3 _patrolTargetPosition;

    private Vector3 _direction;
    private RaycastHit _lastRayCast;

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






        //this if/else is not in the video (it was made in the GameManager videos)
        //Be sure to update the line in the if clause to match the change in the
        //video instead of adding it above

        //UPDATE: how velocity is set
        //normalized reduces dir magnitude to 1, so we can
        //keep at the speed we want by multiplying
        Vector3 _lastPosition = transform.position;



        RaycastHit hit;
        Vector3 grounded = transform.position;
        Vector3 planeDir = _direction;


        //Designed so hopefully if a RayCast misses it still kind of works
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down + Vector3.Scale(transform.forward, new Vector3(.0f, .0f, .0f))), out hit, 1000f))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20f);


            grounded = hit.point + hit.normal * (transform.localScale.y / 2);


            //Will only update the direction IF it changes what wall it is on
            if (_lastRayCast.normal != hit.normal)
            {
                _direction = _patrolTargetPosition - transform.position;
                //_direction = Vector3.Scale(_direction, flip(hit.normal));

            }




            planeDir = _direction;
            //planeDir = _direction - (Vector3.Scale(_patrolTargetPosition, hit.normal) - (Vector3.Scale(transform.position, hit.normal)));


            _lastRayCast = hit;
        }


        Vector3 move = planeDir.normalized * patrolSpeed * Time.deltaTime;
        

        transform.position = grounded + move;


        if ((_patrolTargetPosition - transform.position).magnitude < .3)
        {
            //get next waypoint
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            //change direction
            _direction = _patrolTargetPosition - transform.position;
        }


    }

    private static Vector3 flip(Vector3 v)
    {       
        float x = Mathf.Abs(v.x);
        float y = Mathf.Abs(v.y);   
        float z = Mathf.Abs(v.z); 


        return new Vector3(1 - x, 1 - y, 1 - z);
    }

}
