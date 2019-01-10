using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingLine : MonoBehaviour
{
	[SerializeField] float growRate = 2;
	bool isMoving = true;
	public Vector3 vecDiff;
	public GrowingLineContainer growingLineContainer;

	// Use this for initialization
	void Start()
	{
		GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
	}

	void OnCollisionEnter(Collision collision)
	{
		if ((collision.gameObject.tag == "Border" || collision.gameObject.tag == "StaticLine") && isMoving)
		{
			isMoving = false;
			SceneManager.instance.AddCollidingObject(new GameObject[] { collision.gameObject, gameObject }, collision.contacts[0].point, false);
			growingLineContainer.onLineHitBarrier(gameObject);
			transform.localScale += Vector3.right * .1f;
			transform.localPosition += vecDiff * .1f * .5f;
			gameObject.tag = "StaticLine";
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Sweet" && gameObject.tag == "MovingLine")
		{
			growingLineContainer.onBallHitLine();
			SceneManager.instance.loseLife();
			Destroy(gameObject);
		}

		if (isMoving && (collider.gameObject.tag == "Border" || collider.gameObject.tag == "StaticLine"))
		{
			
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			gameObject.GetComponent<MeshCollider>().isTrigger = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isMoving)
		{
			transform.localScale += Vector3.right * growRate * Time.deltaTime;
			transform.localPosition += vecDiff * growRate * Time.deltaTime * .5f;
			GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
		}
	}
}
