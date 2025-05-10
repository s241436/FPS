using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float JumpHeight = 2f;

    // To increase gravity speed when falling
    public float fallGravityMultiplier = 2f;
    public float mouseSensitivity = 2.0f;
    public float pitchRange = 60.0f;

    private float forwardInputValue;
    private float strafeInputValue;
    private bool jumpInput;

    //Physics fall velocity
    private float terminalVelocity = 53f;
    private float verticalVelocity;

    private float rotateCameraPitch;

    //Zoom
    public float defaultFieldOfView;
    public float zoomFieldOfView;
    public float zoomSpeed;

    private Camera firstPersonCam;
    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        firstPersonCam = GetComponentInChildren<Camera>();
        //Cursor.lockState = CursorLockMode.Locked;

    }


    // Update is called once per frame
    void Update()
    {
        forwardInputValue = Input.GetAxisRaw("Vertical");
        strafeInputValue = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        Movement();
        JumpAndGravity();
        CameraMovement();
    }

    void CameraMovement()
    {
        //Rotate the player around
        float rotateYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotateYaw, 0);

        //Rotate the camera up and down
        rotateCameraPitch += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        //Lock the rotation so we cannot flip the camera. 
        rotateCameraPitch = Mathf.Clamp(rotateCameraPitch, -pitchRange, pitchRange);
        firstPersonCam.transform.localRotation = Quaternion.Euler(rotateCameraPitch, 0, 0);

        //Zoom the camera
        Zoom();
    }
     


    void Movement()
    {
        Vector3 direction = (transform.forward * forwardInputValue + transform.right * strafeInputValue).normalized * movementSpeed * Time.deltaTime;

        //Add physics using Vector3s up direction (World coordinates) as the direction of gravity.
        direction += Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(direction);
    }

    void JumpAndGravity()
    {
        if (characterController.isGrounded)
        {
            // stop our velocity dropping infiniteloy when ground
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }
            if (jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            }

        }


        else
        {
            //Apply gravity over time if under terminal gracity
            if (verticalVelocity < terminalVelocity)
            {
                //Set gravity multiplier if falling downwards.
                float gravityMultiplier = 1;
                if (characterController.velocity.y < -1)
                {
                    gravityMultiplier = fallGravityMultiplier;
                }

                verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

            }
        }

    }

    void Zoom()
    {
      
        if (Input.GetMouseButton(1))
        {
            firstPersonCam.fieldOfView = Mathf.Lerp(firstPersonCam.fieldOfView, zoomFieldOfView, zoomSpeed * Time.deltaTime);
         
        }
        else
        {
            firstPersonCam.fieldOfView = Mathf.Lerp(firstPersonCam.fieldOfView, defaultFieldOfView, zoomSpeed * Time.deltaTime);
             
        }
            
    }
}
