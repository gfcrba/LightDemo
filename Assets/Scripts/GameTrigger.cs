using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrigger : MonoBehaviour {

    public List<TriggerObject> objects;
	
    void OnTriggerEnter(Collider other)
    {
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if(objects[i].OnGameTrigger())
            {
                objects.RemoveAt(i);
            }
        }
    }
}
