using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : ActiveObject
{
    [HideInInspector]
    public enum DoorState
    {
        Open,
        Close
    }

    public DoorState CurrentState = DoorState.Close;

    public float DoorOpenAngle = 94.0f;

    public float DoorCloseAngle = 0.0f;

    public bool AllowStateChange = true;

    private float CurrentDoorAngle;

    void Update ()
    {
        float TargetDoorAngle = DoorOpenAngle;
		if(CurrentState == DoorState.Close)
        {
            TargetDoorAngle = DoorCloseAngle;
        }

        CurrentDoorAngle = Mathf.Lerp(CurrentDoorAngle, TargetDoorAngle, Time.deltaTime * 5.0f);

        if (CurrentDoorAngle == TargetDoorAngle)
        {
            return;
        }

        transform.rotation = Quaternion.AngleAxis(CurrentDoorAngle, transform.up);
    }

    public override bool ActivateObject()
    {
        if (!base.ActivateObject())
        {
            return false;
        }

        if (!AllowStateChange)
        {
            SendGameMessage("Сосайтен :(");
            return false;
        }

        switch (CurrentState)
        {
            case DoorState.Open:
                CurrentState = DoorState.Close;
                break;
            case DoorState.Close:
                CurrentState = DoorState.Open;
                break;
        }

        return true;
    }

    public override void DefaultMessage()
    {
        SendGameMessage(CurrentState == DoorState.Close? "Открыл." : "Закрыл.");
    }


}
