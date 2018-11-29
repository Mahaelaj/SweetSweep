using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

	public static MeshGenerator instance = null;

	public class CollisionNode
	{
		public CollisionTracker collisionTracker;
		public CollisionNode parentNode;
	}

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
		{

			//if not, set instance to this
			instance = this;


		}
		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a MeshGenerator.
			Destroy(gameObject);
	}

	public void generateMesh(CollisionTracker collisionTracker1, CollisionTracker collisionTracker2)
	{
		List<CollisionNode> collisionGraph = new List<CollisionNode>();


		// TODO: do the same search for collision tracker2 
		CollisionNode collisionNode1 = new CollisionNode();
		collisionNode1.collisionTracker = collisionTracker1;

		collisionGraph.Add(collisionNode1);

		List<CollisionNode>[] completeRoutes = new List<CollisionNode>[2];

		// breadth first search for graph creation
		int i = 0;
		while (i < collisionGraph.Count)
		{
			//int parentNodeId = collisionGraph[i].collisionTracker.gameObject.GetInstanceID();
			List<CollisionNode> nodesAlreadyInGraph = new List<CollisionNode>();
			foreach (CollisionTracker childObj in collisionGraph[i].collisionTracker.collidingObjects)
			{

				// check if the game object is already in the graph
				if (collisionGraph.Exists(n => n.collisionTracker.gameObject.GetInstanceID() == childObj.gameObject.GetInstanceID()))
				{

					nodesAlreadyInGraph.Add(collisionGraph.Find(n => n.collisionTracker.gameObject.GetInstanceID() == childObj.gameObject.GetInstanceID()));

					//	Debug.Log(nodesAlreadyInGraph.Count);
					// TODO: handle more than 2 nodes already in the graph
					// if at least 2 of the connecting game objects are already in the graph, then the polygon is created
					if (nodesAlreadyInGraph.Count >= 2)
					{
						getListOfConnectingGameObjects(collisionGraph, nodesAlreadyInGraph, collisionGraph[i]);
					}
				}

				// if it is not in the graph add the colliding objects to the graph
				else
				{
					CollisionNode collisionNode = new CollisionNode();
					collisionNode.collisionTracker = childObj;
					collisionNode.parentNode = collisionGraph[i];
					collisionGraph.Add(collisionNode);
				}
			}
			i++;
		}
	}


	// TODO: When a line hits a border the border should track the collision
	public List<CollisionNode> getListOfConnectingGameObjects(List<CollisionNode> graph, List<CollisionNode> pathStarters, CollisionNode connectingNode)
	{
		List<CollisionNode> gameObjectPollygons = new List<CollisionNode>();
		gameObjectPollygons.Add(connectingNode);

		CollisionNode collisionNode = pathStarters[0];
		while (collisionNode.parentNode != null)
		{
			gameObjectPollygons.Add(collisionNode);
			collisionNode = collisionNode.parentNode;
		}

		collisionNode = pathStarters[1];
		while (collisionNode.parentNode != null)
		{
			if (!gameObjectPollygons.Exists(n => n.collisionTracker.gameObject.GetInstanceID() == collisionNode.collisionTracker.gameObject.GetInstanceID())) gameObjectPollygons.Add(collisionNode);
			collisionNode = collisionNode.parentNode;
		}
		return gameObjectPollygons;
	}
}
