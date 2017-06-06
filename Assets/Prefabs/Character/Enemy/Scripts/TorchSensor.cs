using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSensor : MonoBehaviour
{
    private EnemyAI ai;

    void Start()
    {
        ai = GetComponent<EnemyAI>();
    }

	void OnTriggerStay(Collider other)
    {
        if (other.tag == "Torch")
        {
            ai.HitByPlayerTorch();
        }
    }

    /*void OnTriggerExit(Collider other)
    {
        if(other.tag == "Torch")
        {
            ai.CalmDown();
        }
    }*/
}
