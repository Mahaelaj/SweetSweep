using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CandyCaneMask : MonoBehaviour
{

	[SerializeField] float growRate = 2;
	public CandyCaneContainer candyCaneContainer;

	bool isMoving = true;
	public Vector3 vecDiff;

	// Use this for initialization
	void Start()
	{
		//GetComponent<BoxCollider2D>().isTrigger = true;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Sweet")
		{
			candyCaneContainer.onBallHitLine();
			SceneManager.instance.loseLife();
		}

		if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "StaticLine")
		{
			gameObject.tag = "StaticLine";
			GetComponent<BoxCollider2D>().isTrigger = false;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			isMoving = false;
			SceneManager.instance.AddCollidingObject(new GameObject[] { collision.gameObject, gameObject }, collision.GetContact(0).point);
			candyCaneContainer.onLineHitBarrier(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isMoving)
		{
			transform.localScale += Vector3.right * growRate * Time.deltaTime;
			transform.localPosition += vecDiff * growRate * Time.deltaTime * .5f;
		}
	}
}
