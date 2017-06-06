using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMachineActiveObject : ActiveObject
{
    bool currentStateOn = false;
    float temp = 0.0f;
    Renderer rend;
    bool broken = false;

    public ParticleSystem smoke;
    public ParticleSystem sparks;
    
    public Material activeMaterial;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if(currentStateOn && !broken)
        {
            temp += Time.deltaTime;
            float param = 1.0f / (temp * 10f);
            float sin = param * Mathf.Sin(temp * 50f) + (1.0f - param * 0.3f) - (1.0f - param);
            Color newColor = new Color(sin, sin, sin);
            rend.material.SetColor("_EmissionColor", newColor);
            if(sin < 0.00001f)
            {
                smoke.Play();
                sparks.Play();
                broken = true;
                currentStateOn = false;
            }
        }
    }

    public override bool ActivateObject()
    {
        if (!base.ActivateObject())
        {
            return false;
        }

        if(!currentStateOn)
        {
            SwitchOnMachine();
            currentStateOn = true;
            //SendGameMessage("Хм... Электричество есть");
        }

        return base.ActivateObject();
    }

    public override void DefaultMessage()
    {
        
    }

    private void SwitchOnMachine()
    {
        if(rend)
        {
            rend.material = activeMaterial;
        }
    }

    private IEnumerator strobEmission()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
