using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMachineActiveObject : ActiveObject
{
    bool currentStateOn = false;

    Renderer rend;

    public Material activeMaterial;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
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
            SendGameMessage("Хм... Электричество есть");
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
}
