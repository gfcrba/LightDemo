using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool alerted;
    public bool playerFound;
    public float attackDistance;
    public float sightDistance;
    public float sightHalfAngle;
    public Transform enemyHead;
    public TorchSensor enemySkin;
    public float calmDownTime = 3.0f;
    private float currentCalmDown;

    void Update()
    {
        if(alerted)
        {
            if(currentCalmDown <= 0.0f)
            {
                CalmDown();
            }

            currentCalmDown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        Debug.Log("Ам ням ням!!!");
    }

    public void SawAPlayer()
    {
        playerFound = true;
        SetAlerted();
    }

    public void LostPlayer()
    {
        playerFound = false;
    }

    public void HitByPlayerTorch()
    {
        SetAlerted();
    }

    public void CalmDown()
    {
        alerted = false;
    }

    private void SetAlerted()
    {
        alerted = true;
        currentCalmDown = calmDownTime;
    }
}
