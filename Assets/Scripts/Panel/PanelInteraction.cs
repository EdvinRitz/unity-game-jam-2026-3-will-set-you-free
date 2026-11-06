using UnityEngine;

public class PanelInteraction : MonoBehaviour
{
    public GameObject cameraPoint;
    public GameObject resetPoint;
    public GameObject player;
    public PanelFinder panelFinder;
    public PlayerInputHandler playerInputHandler;
    public bool panelEntered = false;

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
            Debug.Log("interact pressed");
            playerInputHandler.interactPressed = false;
        }
    }

    private void EnterPanel()
    {
        Debug.Log("Panel Entered");
        playerInputHandler.isUsingPanel = true;
        player.transform.position = cameraPoint.transform.position;
        player.transform.rotation = cameraPoint.transform.rotation;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        panelEntered = true;
    }

    private void ExitPanel()
    {
        Debug.Log("Panel Exited");  
        player.transform.position = resetPoint.transform.position;
        player.transform.rotation = resetPoint.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInputHandler.isUsingPanel = false;
        panelEntered = false;
    }
}
