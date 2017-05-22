using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZWriter : MonoBehaviour {
    Material mat;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mat = gameObject.GetComponent<Renderer>().material;
        mat.SetInt("_ZWrite", 1);
	}
}
