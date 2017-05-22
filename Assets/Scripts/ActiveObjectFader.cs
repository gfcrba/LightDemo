using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectFader : MonoBehaviour {
    public bool fade = false;
    public float targetTransparency = 0.5f;
    private float oldTransparency;
    private Material mat;
    private Color oldColor;
    private Color newColor;
    void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        oldColor = mat.GetColor("_Color");
        newColor = oldColor;
        oldTransparency = oldColor.a;
        mat.SetInt("_ZWrite", 1);
    }
	// Update is called once per frame
	void LateUpdate () {
		if(fade)
        {
            newColor.a = Mathf.Lerp(newColor.a, targetTransparency, Time.deltaTime * 5f);
        } else
        {
            newColor.a = Mathf.Lerp(newColor.a, oldTransparency, Time.deltaTime * 2f);
        }

        mat.SetColor("_Color", newColor);
        fade = false;
    }
}
