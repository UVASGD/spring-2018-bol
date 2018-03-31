using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class GravityVortexExplosion : MonoBehaviour
{
	public float radius = 65.0F;
	public float power = 2000.0F;

	IEnumerator Start()
	{
		yield return new WaitForSeconds (2);

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders)
		{
			Vector3 forceDirection = transform.position - hit.transform.position;
			Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();

			if (rb != null) {
				rb.AddForce (forceDirection.normalized * power);
			}
		}

		Destroy(gameObject);
	}

}