using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem inputSystem;
    public InputSystem.PlayerActions playerActions;
    public bool sprintPressed;
    public bool interactPressed;
    public bool clickPressed;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool isUsingPanel = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
    {
        inputSystem = new InputSystem();
        playerActions = inputSystem.Player;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput = playerActions.Move.ReadValue<Vector2>();
        LookInput = playerActions.Look.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        playerActions.Enable();

        playerActions.Interact.performed += ctx => interactPressed = true;

        playerActions.Sprint.performed += ctx => sprintPressed = true;
        playerActions.Sprint.canceled += ctx => sprintPressed = false;

        playerActions.Click.performed += ctx => clickPressed = true;
        playerActions.Click.canceled += ctx => clickPressed = false;
    }

    private void ResetInput()
    {
        interactPressed = false;
        sprintPressed = false;

        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

}
