using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

    public LineRenderer lr;
	public PlayerInput curInput;
    public Rigidbody rb;

    float horizAngle;
    float vertAngle;

    public int resolution;
    public int maxHorizontalDistance = 50;
    public int maxArcLength = 15; // Not exact, roughly.
    float velocity;
    float radAngle;
    float g;
    Vector3 myPos;

	// Use this for initialization
	void Start () {
		if (curInput == null) {
			curInput = GetComponent<PlayerInput>();
		}
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        horizAngle = curInput.horizontalAngle;
        vertAngle = curInput.verticalAngle;
	}

	Vector3 calculateDirectionVector() {
		float horizontalAngle = curInput.horizontalAngle;
		float verticalAngle = curInput.verticalAngle;

		Quaternion directionVertRot = Quaternion.AngleAxis(verticalAngle, Vector3.left);
		Quaternion directionHorizRot = Quaternion.AngleAxis(horizontalAngle, Vector3.up);
		return directionHorizRot*(directionVertRot*Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
        if (AnglesChanged())
        {
            g = Physics.gravity.magnitude;
            float acceleration = curInput.maxPower / rb.mass;
            velocity = (acceleration * Time.fixedDeltaTime);
            lr.positionCount = resolution + 1;
            lr.SetPositions(CalculateArray());
        }
    }

    bool AnglesChanged()
    {
        bool result = (horizAngle != curInput.horizontalAngle) || (vertAngle != curInput.verticalAngle);
        if (result)
        {
            vertAngle = curInput.verticalAngle;
            horizAngle = curInput.horizontalAngle;
        }
        return result;
    }

    Vector3[] CalculateArray()
    {
        myPos = gameObject.transform.position;
        Vector3[] arcArray = new Vector3[resolution + 1];
        radAngle = vertAngle * Mathf.Deg2Rad;
        Quaternion rotation = Quaternion.AngleAxis(horizAngle - 90, Vector3.up); // Not sure why it needs the -90, but that's what made it work...
        for(int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = (rotation * CalculateArcPoint(t, maxHorizontalDistance)) + myPos;
            Vector3 difference = arcArray[i] - myPos;
            if(difference.magnitude >= maxArcLength)
            {
                lr.positionCount = i + 1;
                break;
            }
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radAngle) * Mathf.Cos(radAngle)));
        return new Vector3(x,y);
    }

	public void toggleActive() {
        if (lr.enabled)
        {
            lr.positionCount = 0;
            lr.SetPositions(new Vector3[0]);
        }
        lr.enabled = !lr.enabled;
	}

    public void setActive(bool value)
    {
        if (!value)
        {
            lr.positionCount = 0;
            lr.SetPositions(new Vector3[0]);
        }
        lr.enabled = value;
    }
}
