using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighScript : MonoBehaviour {

    private Material highlightedObjectMaterial;
    public Color highlightColor = new Color(0.1f,0.2f,0.1f);

    void Start()
    {
        highlightedObjectMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public void Highlight(bool on)
    {    
        if(highlightedObjectMaterial)
        {
            if (on)
            {
                highlightedObjectMaterial.EnableKeyword("_EMISSION");
                highlightedObjectMaterial.SetColor("_EmissionColor", highlightColor);
            }
            else
            {
                highlightedObjectMaterial.SetColor("_EmissionColor", Color.black);
                highlightedObjectMaterial.DisableKeyword("_EMISSION");
            }
        }    
        
    }

    /*void Update()
    {
        if(isHighlighted)
        {
            OnMouseEnter();
        } else
        {
            OnMouseExit();
        }
    }*/
}
