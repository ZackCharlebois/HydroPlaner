using System;
using System.Collections;
using System.Collections.Generic; //for List
using UnityEngine;

public class WaypointPath : MonoBehaviour
{

    [SerializeField] private List<Vector3> points;
    private int _currentPointIndex = 0;

    //variable to check if going backwards through path
    private bool _backTrack = false;


    private void Awake()
    {
        var transforms = GetComponentsInChildren<Transform>(true);
        foreach (var t in transforms)
        {
            //if (t.gameObject != gameObject) //if you want to exclude the gameobject this is on
            points.Add(t.position);
        }

        //just in case there were no child objects
        //we add a single point at 0,0,0 to avoid issues
        if (points.Count <= 0)
        {
            points.Add(new Vector3(0, 0));
        }

        
    }

    //Unlinks the WaypointPath to the enemy after all the positions are recorded.
    //Makes waypoints not visibly move when the enemy moves
    private void Start()
    {
        GetComponent<Transform>().SetParent(null, true);

    }

    public Vector3 GetNextWaypointPosition()
    {
        //If going back, follow paths backwards
        if (_backTrack)
        {
            _currentPointIndex--;
        }
        else
        {
            _currentPointIndex++;
        }


            
        if (_currentPointIndex >= points.Count - 1) _backTrack = true;
        else if (_currentPointIndex == 0) _backTrack = false;


        return points[_currentPointIndex];
    }
}
