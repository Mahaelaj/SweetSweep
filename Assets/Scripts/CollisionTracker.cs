using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
	public List<CollisionTracker> collidingObjects = new List<CollisionTracker>();
	public void AddCollidingObject(GameObject obj)
	{
		if (!collidingObjects.Find(o => o.gameObject.GetInstanceID() == obj.GetInstanceID()))
		{
			collidingObjects.Add(obj.GetComponent<CollisionTracker>());
		}
	}
}
