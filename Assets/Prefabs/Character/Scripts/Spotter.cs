using UnityEngine;
using System.Collections;

public class Spotter : MonoBehaviour {
	public Light spotlight;
	public Light spotlightGlow;
	public Light playerHighlight;
	public Color hideColor;
	public Transform leftArm;
	public float defaultGlowDistance = 2.0f;
	public float defaultGlowHeight = 3.0f;
	public float defaultGlowAngle = 45.0f;
	public uint rayCount;
	private Color startColor;
	private Ray[] glowRay;
	private RaycastHit[] info;
    [HideInInspector]
    public bool focused = false;
    [HideInInspector]
    public Vector3 focusTo;
    [Range(0,1)]
    public float glowAlphaAngle = Mathf.PI/6.0f;
    public Transform lightCone;
    private Vector3 defaultConeScale;
   
    private float mouseInputAccum = 0.0f;
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
        defaultConeScale = lightCone.localScale;
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
        if(!GameManager.Instance().freeCursor)
        {
            float mouseInput = Input.GetAxis("Mouse Y");
            mouseInputAccum += mouseInput / 40f;
            mouseInputAccum = saturateInput(mouseInputAccum);
        }
        
        Vector3 playerForward = GameManager.Instance().player.transform.forward;

        Vector3 newDir = Quaternion.AngleAxis(-35f * mouseInputAccum, GameManager.Instance().player.transform.right) * playerForward;
        

        spotlight.transform.rotation = Quaternion.LookRotation(newDir);
    }

    float saturateInput(float input)
    {
        if (input > 1f)
        {
            return 1f;
        }
        else if (input < -1f)
        {
            return -1f;
        }

        return input;
        
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
		this.transform.position = leftArm.position;
    }

	void moveGlowToCollision()
	{
        
        float angle = -90f;
		for (int i = 0; i < rayCount; i++) 
		{
            Vector3 p1 = spotlight.transform.forward * spotlight.range;
            float radius = spotlight.range * Mathf.Sin(Mathf.Deg2Rad * spotlight.spotAngle / 2.0f);
            Vector3 v1 = p1;
            Vector3 v2 = spotlight.transform.up * radius;
            glowRay[i].origin = spotlight.transform.position;
			glowRay[i].direction = v1 + Quaternion.AngleAxis(angle, v1) * v2;
            angle -= defaultGlowAngle;
        }
		for (int i = 0; i < rayCount; i++)
        {
			Physics.Raycast (glowRay[i], out info[i]);
            /*Debug.DrawLine(glowRay[i].origin, info[i].point, Color.cyan);
            if (info[i].transform.tag == "Enemy")
            {
                info[i].transform.SendMessage("HitByTorch", gameObject, SendMessageOptions.DontRequireReceiver);
            }*/
        }
        
		Vector3 pos = calculateMiddleVector3 ();
        spotlightGlow.transform.position = Vector3.Slerp(spotlightGlow.transform.position, pos, Time.deltaTime * 10);
        calculateLightConeSize(pos);
		//Debug.DrawLine(spotlight.transform.position, pos, Color.red);
    }

	Vector3 calculateMiddleVector3() 
	{
		Vector3 ret = Vector3.zero;
		for (var i = 0; i < rayCount; i++) {
			ret += info[i].point;
		}
        //Debug.DrawRay(spotlight.transform.position, spotlight.transform.forward * 10, Color.red);
        //return calcStandardDeviation() + spotlight.transform.position;
        return ret / rayCount;
	}

    Vector3 calcStandardDeviation()
    {
        float ret = 0f;
        float middleDist = 0f;
        for (var i = 0; i < rayCount; i++)
        {
            middleDist += info[i].distance;
        }
        middleDist = middleDist / rayCount;
        Debug.Log("Средняя длинна " + middleDist);
        for (var i = 0; i < rayCount; i++)
        {
            ret += Mathf.Pow(info[i].distance - middleDist, 2f);
        }
        ret = ret / rayCount;
        ret = Mathf.Sqrt(ret);
        Debug.Log("Средняя кв длинна " + ret);

        return spotlight.transform.forward * (middleDist - ret);
    }

    float calculateGlowHeight(float distance)
    {
        return Mathf.Abs(distance*Mathf.Sin(glowAlphaAngle));
    }

    void calculateLightConeSize(Vector3 pos)
    {
        float range = (pos - spotlight.transform.position).magnitude;
        float scaleAmount = range / spotlight.range;
        if(scaleAmount > 0)
        {
            lightCone.localScale = defaultConeScale * scaleAmount;
        }
        
    }
}
