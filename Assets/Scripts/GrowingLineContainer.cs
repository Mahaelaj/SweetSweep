using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingLineContainer : MonoBehaviour
{
	[SerializeField] GrowingLine GrowingLine;

	public Vector3 vecDiff;
	int MovingLines = 2;
	int linesConnectedwithBarriers = 0;
	GrowingLine line1;
	GrowingLine line2;

	// Use this for initialization
	void Start()
	{
		float degrees = Mathf.Atan2(vecDiff.y, vecDiff.x) * Mathf.Rad2Deg;

		line1 = Instantiate(GrowingLine, transform.localPosition, Quaternion.identity);
		line1.gameObject.transform.eulerAngles = new Vector3(0, 0, degrees);
		line1.vecDiff = vecDiff.normalized;
		line1.growingLineContainer = this;

		line2 = Instantiate(GrowingLine, transform.localPosition, Quaternion.identity);
		line2.gameObject.transform.eulerAngles = new Vector3(0, 0, degrees + 180);
		line2.vecDiff = vecDiff.normalized * -1;
		line2.growingLineContainer = this;

		Physics.IgnoreCollision(line1.GetComponent<MeshCollider>(), line2.GetComponent<MeshCollider>());
	}

	public void onLineHitBarrier(GameObject gameObject)
	{
		MovingLines--;
		linesConnectedwithBarriers++;
		if (linesConnectedwithBarriers == 2)
		{
			SceneManager.CollisionPoint collisionPoint = SceneManager.instance.AddCollidingObject(new GameObject[] { line1.gameObject, line2.gameObject }, transform.localPosition);
			MeshGenerator.instance.generateMesh(collisionPoint);
		}
	}

	public void onBallHitLine()
	{
		MovingLines--;
	}
}
