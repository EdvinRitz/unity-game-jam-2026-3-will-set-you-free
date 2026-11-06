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
        var hits = Physics.RaycastAll(fPCamera.transform.position, fPCamera.transform.forward);
        foreach (RaycastHit hit in hits)
        {
            if(hit.transform.gameObject.tag.ToString() == "Panel")
            {
                if(Vector3.Distance(player.transform.position, hit.transform.position) <= interactionRange)
                {
                    interactionInRange = true;
                    Debug.Log("In range");
                }
            }
            else
            {
                 interactionInRange = false;
            }
        }
    }
}
