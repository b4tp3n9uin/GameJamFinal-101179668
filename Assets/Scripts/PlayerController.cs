using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isRunning;

    float walkSpeed = 7;
    float runSpeed = 10;

    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;

    // Animation References
    Animator playerAnimator;

    public readonly int moveXHash = Animator.StringToHash("MovementX");
    public readonly int moveYHash = Animator.StringToHash("MovementY");
    public readonly int runHash = Animator.StringToHash("isRunning");



    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(InputVector.magnitude > 0))
        {
            MoveDirection = Vector3.zero;
        } 

        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movementDir = MoveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDir;
    }

    public void OnMovement(InputValue value)
    {
        InputVector = value.Get<Vector2>();
        // Animate
        playerAnimator.SetFloat(moveXHash, InputVector.x);
        playerAnimator.SetFloat(moveYHash, InputVector.y);
    }

    public void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
        playerAnimator.SetBool(runHash, isRunning);
    }
}
