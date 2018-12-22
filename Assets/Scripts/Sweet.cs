using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweet : MonoBehaviour
{

	[SerializeField] float speed = 1.0f;
	Rigidbody rb;
	Vector3 curVel;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(speed, speed, 0);
	}

	// Update is called once per frame
	void Update()
	{
		if (rb.velocity != Vector3.zero) curVel = rb.velocity;
		// if (rb.) curVel = rb.velocity;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "StaticLine")
		{
			rb.velocity = curVel * -1;
		}
		else rb.AddForce(new Vector3(speed, speed, 0));
	}
}
