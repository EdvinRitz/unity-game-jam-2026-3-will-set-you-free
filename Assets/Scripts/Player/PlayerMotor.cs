using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
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
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMove(playerInputHandler.MoveInput);
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if (playerInputHandler.sprintPressed)
        {
            speed = 7f;
        }
        else
        {
            speed = 5f;
        }
        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
