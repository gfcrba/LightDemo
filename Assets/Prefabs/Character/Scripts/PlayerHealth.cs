using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyHand")
        {
            anim.enabled = false;
        }
    }
}
