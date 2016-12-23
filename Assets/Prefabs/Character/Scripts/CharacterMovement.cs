using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float m_Damping = 0.15f;
	public float speed = 6f;            // The speed that the player will move at.
	public Animator anim;               // Reference to the animator component.
	Vector3 movement;                   // The vector to store the direction of the player's movement.
    Ray camRay;
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	RaycastHit floorHit;
	float camRayLength = 200f;          // The length of the ray from the camera into the scene.
	int crouchAnimLayer;
	int runAnimLayer;
    int walkAnimLayer;
    int currentAnimationLayer;
    float currentLayerWeight;
	float desiredLayerWeight;
	Vector3 playerToMouse;
    StepSoundController stepSC;
    public Camera fakeCamera;

	// Use this for initialization
	void Start () {
		floorMask = LayerMask.GetMask ("Floor"); 
		crouchAnimLayer = anim.GetLayerIndex ("Crouch");
		runAnimLayer = anim.GetLayerIndex ("Run");
        walkAnimLayer = anim.GetLayerIndex("Walk");
        currentLayerWeight = 0.0f;
		desiredLayerWeight = 0.0f;
        stepSC = GetComponent<StepSoundController>();
	}

	void Update()
	{
        if (Input.GetKey (KeyCode.LeftShift)) {
            BlendAnimationLayers(crouchAnimLayer);
        } else if (Input.GetKey (KeyCode.LeftControl)) {
            BlendAnimationLayers(runAnimLayer);
        } else {
            BlendAnimationLayers(walkAnimLayer);
        }
	}

    void BlendAnimationLayers(int newAnimationLayer)
    {
        if(currentAnimationLayer != newAnimationLayer)
        {
            currentAnimationLayer = newAnimationLayer;
            currentLayerWeight = 0.0f;
        } else if(currentLayerWeight < 1.0f)
        {
            currentLayerWeight = Mathf.Lerp(currentLayerWeight, 1.0f, Time.deltaTime * 2f);
        }

        for(int i = 0; i < anim.layerCount; i++)
        {
            if(currentAnimationLayer == i)
            {
                anim.SetLayerWeight(i, currentLayerWeight);
            } else
            {
                anim.SetLayerWeight(i, Mathf.Lerp(anim.GetLayerWeight(i),0.0f, Time.deltaTime*2f));
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate ()
	{
        // Store the input axes.
        float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		// Turn the player to face the mouse cursor.
		Turning ();
		// Move the player around the scene.
		Move (h, v);
		// Animate the player.
		Animating (h, v);
	}

	private void Move(float h, float v)
	{
		if(anim.GetFloat("StepSound") > 0.95f)
        {
            stepSC.PlayTerrainStepSound();
        }
	}

    private void Turning()
	{
        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookhere = new Vector3(0,mouseInput,0);
        transform.Rotate(lookhere);
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        /*camRay = fakeCamera.ScreenPointToRay(Input.mousePosition);
        
        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, 200f, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - transform.position;
            
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Set the player's rotation to this new rotation.
            transform.rotation = Quaternion.LookRotation(playerToMouse);
        }*/
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void Animating(float h, float v)
	{
		if (h != 0 || v != 0) {
			anim.SetBool ("moving", true);
			anim.SetFloat ("h", h, m_Damping, Time.deltaTime);
			anim.SetFloat ("v", v, m_Damping, Time.deltaTime);
		} else {
			anim.SetBool ("moving", false);
			anim.SetFloat ("h", 0f, m_Damping, Time.deltaTime);
			anim.SetFloat ("v", 0f, m_Damping, Time.deltaTime);
		}
	}
}
