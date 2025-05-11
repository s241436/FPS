using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class invisibleWall : MonoBehaviour
{
    [Tooltip("Tag of the object allowed to collide with this wall (e.g., 'Player')")]
    public string allowedTag = "Player";

    private BoxCollider boxcollider;


    private void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == allowedTag)
        {
            boxcollider.isTrigger = false;
        }

    
    }
}


    

