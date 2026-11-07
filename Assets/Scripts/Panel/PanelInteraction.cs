using UnityEngine;
using UnityEngine.InputSystem;

public class PanelInteraction : MonoBehaviour
{
    public GameObject cameraPoint;
    public GameObject resetPoint;
    public GameObject player;
    public Camera fpCamera;
    public PanelFinder panelFinder;
    public PlayerInputHandler playerInputHandler;
    public bool panelEntered = false;

    private readonly int timerButtonLayerNumber = 6;
    private readonly int dragButtonLayerNumber = 7;
    private Rigidbody draggedButton;
    private Vector3 target;
    private const float DragSpeed = 10f;

    void Awake()
    {
        cameraPoint = transform.Find("Camera Point").gameObject;
        resetPoint = transform.Find("Reset Point").gameObject;
    }

    void Update()
    {
        if (!panelEntered)
            return;

        var click = playerInputHandler.playerActions.Click;

        // Select a button only on a fresh press, never during the rest of a hold.
        if (click.WasPressedThisFrame() && Physics.Raycast(ShootClickRay(), out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == timerButtonLayerNumber)
            {
                Debug.Log("Timer Button");
            }
            else if (hit.collider.gameObject.layer == dragButtonLayerNumber)
            {
                draggedButton = hit.rigidbody;
                if (draggedButton != null)
                    target = draggedButton.position;
            }
        }

        if (!click.IsPressed())
        {
            ReleaseDrag();
            return;
        }

        if (draggedButton != null)
        {
            Plane dragPlane = new Plane(Vector3.up, draggedButton.position);
            Ray dragRay = ShootClickRay();
            if (dragPlane.Raycast(dragRay, out float distance))
                target = dragRay.GetPoint(distance);
        }
    }

    void FixedUpdate()
    {
        if (draggedButton == null)
            return;

        Vector3 difference = target - draggedButton.position;
        difference.y = 0f;
        draggedButton.linearVelocity = Vector3.ClampMagnitude(difference * DragSpeed, DragSpeed);
    }

    private void ReleaseDrag()
    {
        if (draggedButton == null)
            return;

        draggedButton.linearVelocity = Vector3.zero;
        draggedButton = null;
    }

    public void EnterPanel()
    {
        Debug.Log("Panel Entered");
        playerInputHandler.isUsingPanel = true;
        player.transform.position = cameraPoint.transform.position;
        player.transform.rotation = cameraPoint.transform.rotation;
        fpCamera.transform.rotation = cameraPoint.transform.rotation;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        panelEntered = true;
    }

    public void ExitPanel()
    {
        ReleaseDrag();
        Debug.Log("Panel Exited");
        player.transform.position = resetPoint.transform.position;
        player.transform.rotation = resetPoint.transform.rotation;
        fpCamera.transform.rotation = resetPoint.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInputHandler.isUsingPanel = false;
        panelEntered = false;
    }

    private Ray ShootClickRay()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        return fpCamera.ScreenPointToRay(mousePosition);
    }
}
