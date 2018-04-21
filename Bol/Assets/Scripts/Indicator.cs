using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

    public LineRenderer lr;
	public PlayerInput curInput;
    public Rigidbody rb;

	public float minScale = 0.25f;
	public float maxScale = 1.0f;
    float oldHorizAngle;
    float oldVertAngle;

    public int resolution;
    float velocity;
    float radAngle;
    float g;

	// Use this for initialization
	void Start () {
		if (!curInput) {
			curInput = GetComponent<PlayerInput>();
		}
        if (!lr)
        {
            lr = GetComponent<LineRenderer>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        oldHorizAngle = curInput.horizontalAngle;
        oldVertAngle = curInput.verticalAngle;
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
        bool result = (oldHorizAngle != curInput.horizontalAngle) || (oldVertAngle != curInput.verticalAngle);
        if (result)
        {
            oldVertAngle = curInput.verticalAngle;
            oldHorizAngle = curInput.horizontalAngle;
        }
        return result;
    }

    Vector3[] CalculateArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radAngle = curInput.verticalAngle * Mathf.Deg2Rad;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radAngle));
        Quaternion rotation = Quaternion.AngleAxis(curInput.horizontalAngle - 90, Vector3.up); // Not sure why it needs the -90, but that's what made it work...
        for(int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = rotation * CalculateArcPoint(t, maxDistance);
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
        lr.enabled = !lr.enabled;
	}
}
