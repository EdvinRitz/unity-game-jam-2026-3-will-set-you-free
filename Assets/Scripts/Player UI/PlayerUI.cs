using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public PanelFinder panelFinder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panelFinder.interactionInRange)
        {
            interactText.text = "Press E to interact";
        }
        else
        {
            interactText.text = "";
        }
            

            
    }
}
