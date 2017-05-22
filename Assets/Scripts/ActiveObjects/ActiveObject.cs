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
            SendGameMessage("Надо подойти ближе.");
            return false;
        }
        DefaultMessage();
        return true;
    }

    protected void SendGameMessage(string msg)
    {
        GameManager.Instance().ShowGameMsg(msg);
    }

    public virtual void DefaultMessage()
    {
        SendGameMessage("Гейдизайнер не продумал =)");
    }

	void OnMouseDown()
    {
        ActivateObject();
    }

    public void FakeMouseDown()
    {
        ActivateObject();
    }
}
