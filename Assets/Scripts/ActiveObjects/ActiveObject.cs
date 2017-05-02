using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    public float ActivationDistance = 2.0f;

    public virtual bool ActivateObject()
    {
        GameObject player = GameManager.Instance().player;

        if (ActivationDistance < (player.transform.position - transform.position).magnitude)
        {
            return false;
        }

        return true;
    }

	void OnMouseDown()
    {
        ActivateObject();
    }
}
