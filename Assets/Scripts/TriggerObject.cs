using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TriggerObject : MonoBehaviour
{
    public bool OneTimeExecute = false;
    virtual public bool OnGameTrigger() { return OneTimeExecute; }
}
 