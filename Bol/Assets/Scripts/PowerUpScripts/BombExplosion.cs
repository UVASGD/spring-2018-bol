using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class BombExplosion : MonoBehaviour
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
			Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
		}

		Destroy(gameObject);
	}

}