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
    public GameObject button1;
    private readonly int buttonLayerNumber = 6;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler.interactPressed)
        {
            if(panelFinder.interactionInRange && !panelEntered)
            {
                EnterPanel();
            }
            else if (panelEntered)
            {
                ExitPanel();
            }
            playerInputHandler.interactPressed = false;
        }

        if (playerInputHandler.clickPressed)
        {
            if (panelEntered)
            {
                Debug.Log("clicked");
                var ray = ShootClickRay();
                Physics.Raycast(ray, out RaycastHit hit);
                if (hit.transform.gameObject.layer == buttonLayerNumber)
                {
                    Debug.Log("Button");
                }

            }
        }
    }

    private void EnterPanel()
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

    private void ExitPanel()
    {
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
