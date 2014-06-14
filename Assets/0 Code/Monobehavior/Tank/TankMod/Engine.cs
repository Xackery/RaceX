using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour
{
	private bool isForward = false;
	public float currentForce;


	void FixedUpdate () {

		if (Input.GetButtonDown("Throttle")) {
			isForward = true;
		}

		if (isForward) {
			float force = Mathf.Clamp(currentForce,1,20000) * 2;
			rigidbody.AddRelativeForce(transform.forward * force);
			currentForce = Mathf.Clamp(force, 1, 20000);
		}

		if (isForward && Input.GetButtonUp("Throttle")) {
			isForward = false;
			currentForce = 0;
		}

	}
}
