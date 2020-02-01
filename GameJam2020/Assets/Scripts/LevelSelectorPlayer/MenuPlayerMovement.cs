using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [Range(0f, 1f)] [SerializeField] private float buildUpSpeed;

    private float minInput;
    private float buildUpRot;

    private Vector2 movementInput;
    private Vector2 newMovementInput;
    private Vector3 forward;
    private Vector3 right;

    private Rigidbody rb;

    private MenuPlayerController playerController;
    private MenuInputHandler playerInput;

    [SerializeField] private bool moveIsometric;

    

    void Awake()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<MenuInputHandler>();
        playerController = GetComponent<MenuPlayerController>();
        minInput = playerController.GetMinInput();
        buildUpRot = playerController.GetBuildUpRot();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        newMovementInput = playerInput.movementInput;
        if (newMovementInput.magnitude < minInput)
            newMovementInput = Vector2.zero;

        float realBuildUpSpeed = 1f - Mathf.Pow(1f - buildUpSpeed, Time.deltaTime * 60);
        movementInput = Vector2.Lerp(movementInput, newMovementInput, realBuildUpSpeed);

        // Movement ---------------------------------------------------
        Vector3 hMovement;
        Vector3 vMovement;

        if (moveIsometric)
        {
            hMovement = Vector3.right * movementInput.x;
            vMovement = Vector3.forward * movementInput.y;
        }
        else
        {
            hMovement = right * movementInput.x;
            vMovement = forward * movementInput.y;
        }

        Vector3 finalMovement = Vector3.ClampMagnitude((hMovement + vMovement), 1.0f) * speed * Time.deltaTime;        

        transform.position += finalMovement;

        // Rotation ---------------------------------------------------
        if (newMovementInput.magnitude > minInput)
        {
            Vector3 heading = Vector3.Normalize(hMovement + vMovement);
            Vector3 finalHeading = Vector3.Lerp(transform.forward, heading, buildUpRot);
            float angleDiff = Vector3.Angle(heading, transform.forward);

            transform.forward = (angleDiff < 160) ? finalHeading : heading;
        }
    }
}
