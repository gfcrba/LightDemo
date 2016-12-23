using UnityEngine;
using System.Collections;

public class Spotter : MonoBehaviour {
	public Light spotlight;
	public Light spotlightGlow;
	private Light leftSpotlightGlow;
	private Light rightSpotlightGlow;
	public Light playerHighlight;
	public Color hideColor;
	public Transform leftArm;
	public float defaultGlowDistance = 2.0f;
	public float defaultGlowHeight = 3.0f;
	public float defaultGlowAngle = 25.0f;
	public uint rayCount;
	private Color startColor;
	private Ray[] glowRay;
	private RaycastHit[] info;
    [Range(0,1)]
    public float glowAlphaAngle = Mathf.PI/6.0f;
    
    // Use this for initialization
    void Start () {
		startColor = playerHighlight.color;
		hideColor = new Color(0.5f,0.4f,0.3f,1.0f);
		info = new RaycastHit[rayCount];
		glowRay = new Ray[rayCount];
		for (var i = 0; i < rayCount; i++) 
		{
			glowRay[i] = new Ray();
		}
	}
	
	// Update is called once per frame

	void FixedUpdate()
	{
		armConnection ();
		moveGlowToCollision ();
	}

	void Update () 
	{
		if (Input.GetKeyUp("r")) {
			toggleLight();
		}

	}

	void toggleLight()
	{
		if (spotlight.isActiveAndEnabled) {
			spotlight.enabled = false;
			playerHighlight.color = hideColor;
		} else {
			spotlight.enabled = true;
			playerHighlight.color = startColor;
 		}
	}

	void armConnection()
	{
		this.transform.position = leftArm.position;//Vector3.SmoothDamp(this.transform.position, leftArm.position, ref velocity, smoothTime);
        //this.transform.rotation = leftArm.rotation;//Quaternion.Slerp (this.transform.rotation, leftArm.rotation, Time.deltaTime); 

    }

	void moveGlowToCollision()
	{
		float angle = -defaultGlowAngle;
		for (int i = 0; i < rayCount; i++) 
		{
			glowRay[i].origin = spotlight.transform.position;
			glowRay[i].direction = Quaternion.Euler(0.0f, angle, 0.0f) * spotlight.transform.forward;
			angle += (2 * defaultGlowAngle) / rayCount;
		}
		for (int i = 0; i < rayCount; i++) {
			Physics.Raycast (glowRay[i], out info[i]);
		}
		Vector3 pos = calculateMiddleVector3 ();
        float distance = (pos - spotlight.transform.position).magnitude;
        pos.y = calculateGlowHeight(distance);
		spotlightGlow.transform.position = Vector3.Slerp(spotlightGlow.transform.position, pos, Time.deltaTime * 10);
		Debug.DrawLine(spotlight.transform.position, pos);
	}

	Vector3 calculateMiddleVector3() 
	{
		Vector3 ret = Vector3.zero;
		for (var i = 0; i < rayCount; i++) {
			ret += info[i].point;
		}
        //ret = Vector3.ClampMagnitude(ret, ret.magnitude - glowDistanceMultiplier);
        return ret / rayCount;
	}

    float calculateGlowHeight(float distance)
    {
        return Mathf.Abs(distance*Mathf.Sin(glowAlphaAngle));
    }
}
