using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour
{
	private bool isForward = false;
	private bool isLeft = false;
	public float currentForce;


	void FixedUpdate () {
		if (Input.GetButton("Horizontal")) {
			bool isLeft = (Input.GetAxis ("Horizontal") > 0);
			transform.Rotate((isLeft) ? new Vector3(0,1,0) : new Vector3(0,-1,0));
		}

		if (Input.GetButton("Vertical")) {
			isForward = true;
		} else if (isForward) {
			isForward = false;
			currentForce = 0;
		}

		if (isForward) {
			float force = Mathf.Clamp(currentForce,1,10000) * 1.2f;
			rigidbody.AddForce(transform.forward * force);
			currentForce = Mathf.Clamp(force, 1, 10000);
		}

	}
}
