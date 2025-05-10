using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VectorLerp : MonoBehaviour
{
    [SerializeField] int speed;

    [SerializeField] float trigger_distance = 0;
    bool triggered = false;

    GameObject targetPoint;
    [SerializeField] GameObject transformObject;

    GameObject player;

    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {

        
        if ((transformObject.transform.position.magnitude - player.transform.position.magnitude) < trigger_distance)
        {
            triggered = true;
        }
        else if (trigger_distance == 0)
        {
            triggered = true;
        }

        if (triggered == true)
        {
            movetarget();
        }


     

    }

    private void movetarget()
    {
        if (Vector3.Distance(transformObject.transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            // Increase waypoint index
            currentWaypointIndex++;
            // resets the loop
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transformObject.transform.position = Vector3.Lerp(transformObject.transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);

    }

   





}
