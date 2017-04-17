using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float m_Damping = 0.15f;
	public Animator anim;               // Reference to the animator component.
    public bool backCamera = false;

	private Vector3 movement;                   // The vector to store the direction of the player's movement.
    
	private Ray camRay;
	private int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	private RaycastHit floorHit;
	private Vector3 playerToMouse;

	private int crouchAnimLayer;
	private int runAnimLayer;
    private int walkAnimLayer;
    private int currentAnimationLayer;
    private float currentLayerWeight;
	private float desiredLayerWeight;

    private StepSoundController stepSC;

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
        SwitchCameraMode();
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

		Vector3 camForw = Vector3.ProjectOnPlane (Camera.main.transform.forward, Vector3.up);
		float angle = Vector3.Angle(camForw.normalized, transform.forward);
		Vector3 cross = Vector3.Cross(transform.forward,camForw.normalized);
		movement.Set (h, 0f, v);
		movement = Quaternion.Euler (0f, angle * Mathf.Sign(cross.y), 0f) * movement;

		// Turn the player to face the mouse cursor.
		if (!backCamera) {
			Turning ();
		}

		PlayStepSound ();

		// Animate the player.
		MovementAnimation (movement.x, movement.z);
	}

	private void PlayStepSound()
	{
		if(anim.GetFloat("StepSound") > 0.95f)
        {
            stepSC.PlayTerrainStepSound();
        }
	}

    private void Turning()
	{
        if(backCamera)
        {
            float mouseInput = Input.GetAxis("Mouse X");
            Vector3 lookhere = new Vector3(0, mouseInput, 0);
            transform.Rotate(lookhere);
            return;
        }

		camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		// Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, 200f, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - transform.position;
            
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Set the player's rotation to this new rotation.
            transform.rotation = Quaternion.LookRotation(playerToMouse);
        }
    }

    private void SwitchCameraMode()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            backCamera = !backCamera;
            if (backCamera)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        
    }

	private void MovementAnimation(float h, float v)
	{
		anim.SetBool ("moving", (h != 0 || v != 0));
		anim.SetFloat ("h", h, m_Damping, Time.deltaTime);
		anim.SetFloat ("v", v, m_Damping, Time.deltaTime);
	}
}
