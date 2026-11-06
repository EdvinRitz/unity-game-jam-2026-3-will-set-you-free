using UnityEngine;

public class PanelFinder : MonoBehaviour
{
    public Camera fpCamera;
    public float interactionRange;
    public bool interactionInRange;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out RaycastHit hit, interactionRange))
        {
            if(hit.transform.gameObject.tag.ToString() == "Panel")
            {
                if(Vector3.Distance(player.transform.position, hit.transform.position) <= interactionRange)
                {
                    interactionInRange = true;
                }
            }
            else
            {
                interactionInRange = false;
            }
            
        }
        else if(!Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, interactionRange) || hit.transform.gameObject.tag.ToString() != "Panel")
        {
            interactionInRange = false;
        }
    }
}
