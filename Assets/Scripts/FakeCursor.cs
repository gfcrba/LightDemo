using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    public Transform cursor;
    private Light spot;
    private Vector3 spotOrigin;
    private Vector3 spotForward;
    private RaycastHit hitInfo;
    private Ray spotRay;
    private Collider hitCollider = null;
    private HighlighScript highlight = null;
    private ActiveObject activeGO = null;
	// Use this for initialization
	void Start ()
    {
        spot = GameManager.Instance().player.GetComponentInChildren<Spotter>().spotlight;
        spotRay = new Ray();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(activeGO != null)
            {
                activeGO.ActivateObject();
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        spotOrigin = spot.transform.position;
        spotForward = spot.transform.forward;

        spotRay.origin = spotOrigin;
        spotRay.direction = spotForward;

        if(Physics.Raycast(spotRay, out hitInfo, 100f))
        {
            //Debug.DrawRay(spotOrigin, hitInfo.point - spotOrigin, Color.red);
            if(hitCollider != hitInfo.collider)
            {
                hitCollider = hitInfo.collider;
                if(highlight != null)
                {
                    highlight.Highlight(false);
                }
                highlight = hitCollider.GetComponent<HighlighScript>();
                if(highlight)
                {
                    highlight.Highlight(true);
                }
                activeGO = hitCollider.GetComponent<ActiveObject>();
            } 

            cursor.position = Vector3.Slerp(cursor.position, hitInfo.point + hitInfo.normal * 0.05f, Time.deltaTime*20f);
            //cursor.position += hitInfo.normal * 0.05f;
        }
	}
}
