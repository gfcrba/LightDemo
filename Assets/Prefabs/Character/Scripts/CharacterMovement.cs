using UnityEngine;
using System.Collections;

[System.Serializable]
public enum MovementType
{
    NoMovement,
    Walk,
    Run
}

public class CharacterMovement : MonoBehaviour {
	public float m_Damping = 0.15f;
	public Animator anim;               // Reference to the animator component.
    public bool backCamera = true;

	private Vector3 movement;
    private StepSoundController stepSC;
    private MovementType moveType;

	// Use this for initialization
	void Start () {
		stepSC = GetComponent<StepSoundController>();
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

		Turning ();
       
        // Animate the player.
        MovementAnimation (movement.x, movement.z);
        PlayStepSound();
    }

	private void PlayStepSound()
	{
		if(anim.GetFloat("StepSound") > 0.95f)
        {
            stepSC.PlayTerrainStepSound(moveType);
        }
	}

    private void Turning()
	{
        if (!GameManager.Instance().freeCursor)
        {
            float mouseInput = Input.GetAxis("Mouse X");
            Vector3 lookhere = new Vector3(0, mouseInput, 0);
            transform.Rotate(lookhere);
        }
    }

	private void MovementAnimation(float h, float v)
	{
        if(h != 0 || v != 0)
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                moveType = MovementType.Run;
            }
            else
            {
                moveType = MovementType.Walk;
            } 
        }
        else
        {
            moveType = MovementType.NoMovement;
        } 
		anim.SetBool("running", (h != 0 || v != 0) && Input.GetKey(KeyCode.LeftShift));
        anim.SetBool("walking", (h != 0 || v != 0) && !Input.GetKey(KeyCode.LeftShift));
        anim.SetFloat ("h", h, m_Damping, Time.deltaTime);
		anim.SetFloat ("v", v, m_Damping, Time.deltaTime);
	}
}
