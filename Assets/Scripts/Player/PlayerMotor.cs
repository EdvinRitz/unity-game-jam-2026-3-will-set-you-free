using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator characterAnimator;
    private bool wasMoving;
    private PlayerInputHandler playerInputHandler;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float sprintSpeed = 7f;
    private bool isGrounded;
    private float gravity = -9.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<CharacterController>();
        characterAnimator = GetComponentInChildren<Animator>();
        if (characterAnimator != null) characterAnimator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler.isUsingPanel)
        {
            return;
        }
        ProcessMove(playerInputHandler.MoveInput);
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        bool isMoving = input.sqrMagnitude > 0.001f;
        bool isSprinting = isMoving && playerInputHandler.sprintPressed;
        if (characterAnimator != null)
        {
            if (isMoving != wasMoving)
                characterAnimator.Play(isMoving ? "run" : "wait", 0, 0f);
            characterAnimator.speed = isSprinting ? 1.5f : 1f;
        }
        wasMoving = isMoving;
        float currentSpeed = isSprinting ? sprintSpeed : speed;
        controller.Move(currentSpeed * Time.deltaTime * transform.TransformDirection(moveDirection));
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
