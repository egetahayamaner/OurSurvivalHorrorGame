using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorControl : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float crouchedSpeed = 3.0f;
    public float rotationSpeed = 10.0f;
    private Animator survivorAnimator;

    void Start()
    {
        survivorAnimator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
        bool isMoving = movement != Vector3.zero;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isCrouched = Input.GetKey(KeyCode.LeftCommand);
        bool isCrouchedWalking =  Input.GetKey(KeyCode.LeftCommand) && Input.GetKey(KeyCode.W);

        // Determine the current speed based on the character's state
        float currentSpeed;
        if (isCrouchedWalking) {
            currentSpeed = crouchedSpeed; // Use the crouched walking speed when crouched walking
        } else if (isRunning) {
            currentSpeed = runSpeed; // Use running speed when running
        } else {
            currentSpeed = walkSpeed; // Default to walking speed
        }

        transform.Translate(movement * currentSpeed * Time.deltaTime);


        

        // Mouse rotation
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0.0f, mouseX * rotationSpeed, 0.0f);

        // Update Animator Parameters
        survivorAnimator.SetBool("IdleToWalk", isMoving && !isRunning);
        survivorAnimator.SetBool("WalkToRun", isMoving && isRunning);
        survivorAnimator.SetBool("isCrouched", isCrouched);
        survivorAnimator.SetBool("isCrouchedWalking", isCrouchedWalking);
    }
}
