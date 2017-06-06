using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    private Transform player;
    private EnemyAI ai;

    void Awake () {
        player = GameManager.Instance().player.transform;
        ai = GetComponent<EnemyAI>();
	}
	
	void Update()
    {
        Vector3 directionToPlayer = player.position - ai.enemyHead.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angle = Vector3.Angle(directionToPlayer, ai.enemyHead.forward);
        if (distanceToPlayer < ai.sightDistance)
        {
            if (angle < ai.sightHalfAngle)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(ai.enemyHead.position, directionToPlayer.normalized, out hitInfo, ai.sightDistance))
                {
                    if (hitInfo.collider.tag == "Player")
                    {
                        ai.SawAPlayer();
                    }
                }
            }
        }
    }
}
