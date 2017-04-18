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

    void OnMouseEnter()
    {        
        highlightedObjectMaterial.EnableKeyword("_EMISSION");
        highlightedObjectMaterial.SetColor("_EmissionColor", highlightColor);
    }

    void OnMouseExit()
    {
        highlightedObjectMaterial.SetColor("_EmissionColor", Color.black);
        highlightedObjectMaterial.DisableKeyword("_EMISSION");
    }
}
