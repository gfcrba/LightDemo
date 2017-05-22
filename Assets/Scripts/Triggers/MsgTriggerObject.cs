using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgTriggerObject : TriggerObject
{
    public string Message;
    public override bool OnGameTrigger()
    {
        GameManager.Instance().ShowGameMsg(Message);
        return OneTimeExecute;
    }
}
