using UnityEngine;

public class PanelFinder : MonoBehaviour
{
    public Camera fPCamera;
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
        interactionInRange = false;

        if(Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out RaycastHit hit, interactionRange))
        {
            if(hit.transform.gameObject.tag.ToString() == "Panel")
            {
                if(Vector3.Distance(player.transform.position, hit.transform.position) <= interactionRange)
                {
                    interactionInRange = true;
                }
            }
            
        }
    }
}
