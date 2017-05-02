using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTriggerObject : TriggerObject
{
    private float opacityFalloff = 0.04f;
    private float opacity = 1.0f;
    private bool hide = false;
    private Material mat;
    private Color roofColor;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetInt("_ZWrite", 1);
        roofColor = mat.GetColor("_Color");
    }

    void Update()
    {
        if(hide)
        {
            opacity -= opacityFalloff;
            if(opacity <= 0.0f)
            {
                gameObject.SetActive(false);
            }

            if(mat)
            {
                roofColor.a = opacity;
                mat.SetColor("_Color", roofColor);
            }
        }
    }

    public override bool OnGameTrigger()
    {
        hide = true;
        return OneTimeExecute;
    }
}
