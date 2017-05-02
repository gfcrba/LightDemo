using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighScript : MonoBehaviour {

    private Material highlightedObjectMaterial;
    public Color highlightColor = new Color(0.1f,0.2f,0.1f);
    private Transform spotLightIKTransform;
    private Vector3 defaultSpotlightPos;
    private Quaternion defaultSpotlightRot;
    private bool isHighlighted = false;
    private Renderer rend;
    private Spotter spotter;
    void Start()
    {
        highlightedObjectMaterial = gameObject.GetComponent<Renderer>().material;
        spotLightIKTransform = GameManager.Instance().player.GetComponent<SpotlightIK>().spotlight.transform;
        rend = GetComponent<Renderer>();
        spotter = GameManager.Instance().player.GetComponentInChildren<Spotter>();
    }

    void OnMouseEnter()
    {        
        highlightedObjectMaterial.EnableKeyword("_EMISSION");
        highlightedObjectMaterial.SetColor("_EmissionColor", highlightColor);
        isHighlighted = true;
        spotter.focusTo = rend.bounds.center - spotLightIKTransform.position;
        spotter.focused = true;
        
        //spotLightIKTransform.rotation = Quaternion.LookRotation((rend.bounds.center - spotLightIKTransform.position).normalized);
    }

    void Update()
    {
        if(isHighlighted)
        { 
            spotter.focusTo = rend.bounds.center - spotLightIKTransform.position;

            Debug.DrawRay(spotLightIKTransform.position, rend.bounds.center - spotLightIKTransform.position, Color.red);
            Debug.DrawRay(spotLightIKTransform.position, spotLightIKTransform.forward * 10f, Color.green);
        }
    }

    void OnMouseExit()
    {
        highlightedObjectMaterial.SetColor("_EmissionColor", Color.black);
        highlightedObjectMaterial.DisableKeyword("_EMISSION");
        isHighlighted = false;
        spotter.focused = false;
        //spotLightIKTransform.rotation = Quaternion.LookRotation(GameManager.Instance().player.transform.forward);
    }
}
