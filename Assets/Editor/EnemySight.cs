using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemyAI))]
public class EnemySight : Editor
{
    void OnSceneGUI()
    {
        EnemyAI myTarget = (EnemyAI) target;
        Transform headPosition = myTarget.enemyHead;
        Handles.color = new Color(1f, 1f, 1f, 0.2f);

        Handles.DrawSolidArc(headPosition.position,
            headPosition.up,
            Quaternion.Euler(0.0f,-myTarget.sightHalfAngle, 0.0f) * headPosition.forward,
            myTarget.sightHalfAngle * 2.0f,
            myTarget.sightDistance);

        Handles.color = Color.white;
    }
}
