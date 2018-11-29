using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneContainer : MonoBehaviour
{
	[SerializeField] GameObject CandyCane;
	[SerializeField] CandyCaneMask CandyCaneMask;
	public Vector3 vecDiff;
	int MovingLines = 2;
	int linesConnectedwithBarriers = 0;
	CandyCaneMask line1;
	CandyCaneMask line2;


	private void Start()
	{
		float degrees = Mathf.Atan2(vecDiff.y, vecDiff.x) * Mathf.Rad2Deg;

		CandyCane.transform.eulerAngles = new Vector3(0, 0, degrees + 90);

		line1 = Instantiate(CandyCaneMask, transform.localPosition, Quaternion.identity);
		line1.gameObject.transform.eulerAngles = new Vector3(0, 0, degrees);
		line1.vecDiff = vecDiff.normalized;
		line1.candyCaneContainer = this;

		line2 = Instantiate(CandyCaneMask, transform.localPosition, Quaternion.identity);
		line2.gameObject.transform.eulerAngles = new Vector3(0, 0, degrees + 180);
		line2.vecDiff = vecDiff.normalized * -1;
		line2.candyCaneContainer = this;
	}

	public void onLineHitBarrier(CollisionTracker tracker)
	{
		MovingLines--;
		linesConnectedwithBarriers++;
		if (linesConnectedwithBarriers == 2)
		{
			line1.gameObject.GetComponent<CollisionTracker>().AddCollidingObject(line2.gameObject);
			line2.gameObject.GetComponent<CollisionTracker>().AddCollidingObject(line1.gameObject);
			MeshGenerator.instance.generateMesh(line1.gameObject.GetComponent<CollisionTracker>(), line2.gameObject.GetComponent<CollisionTracker>());
		}
	}

	public void onBallHitLine()
	{
		Debug.Log("ball hit line");
		MovingLines--;
	}

	void calculateSpaceUsed()
	{

	}
}
