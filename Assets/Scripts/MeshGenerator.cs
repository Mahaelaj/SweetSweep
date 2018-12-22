using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshGenerator : MonoBehaviour
{
	public static MeshGenerator instance = null;
	public List<TopMesh> topMeshes;
	public SpriteMask spriteMask = null;
	int createdImageIndex = 0;

	public GameObject topMesh;

	public class CollisionNode
	{
		public GameObject[] objs;
		public CollisionNode parentNode;
		public Vector2 CollisionPoint;
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

	public void generateMesh(SceneManager.CollisionPoint startPoint)
	{
		List<CollisionNode> collisionGraph = new List<CollisionNode>();

		// TODO: do the same search for collision tracker2 
		CollisionNode collisionNode1 = new CollisionNode();
		collisionNode1.objs = startPoint.objects;
		collisionNode1.CollisionPoint = startPoint.point;

		collisionGraph.Add(collisionNode1);

		List<GameObject> exploredObjects = new List<GameObject>();
		List<CollisionNode>[] completeRoutes = new List<CollisionNode>[2];

		// breadth first search for graph creation
		int i = 0;
		while (i < collisionGraph.Count)
		{
			List<CollisionNode> collisionsAlreadyInGraph = new List<CollisionNode>();
			// loop through both of the objects of the node
			foreach (GameObject exploringObj in collisionGraph[i].objs)
			{
				// if the object looking into has already been explored, then there is no need to explore it
				if (exploredObjects.Exists(n => n.GetInstanceID() == exploringObj.GetInstanceID())) continue;

				// find the objects the current node object is colliding with. Add it to the collision graph if it does not exist yet
				foreach (SceneManager.CollisionPoint collisionPoint in SceneManager.instance.collisionPoints)
				{
					// we only want to explore the collisions that involve the object we are currently exploring 
					if (!Array.Exists(collisionPoint.objects, o => o.GetInstanceID() == exploringObj.GetInstanceID())) continue;

					// if the collision already exists in our collision graph then we do not want to add it again
					if (collisionGraph.Exists(n => n.CollisionPoint == collisionPoint.point) && collisionGraph.Count >= 4)
					{
						collisionsAlreadyInGraph.Add(collisionGraph.Find(n => n.CollisionPoint == collisionPoint.point));

						// TODO: handle more than 2
						if (collisionsAlreadyInGraph.Count == 2)
						{
							Vector3[] vertices = getListOfConnectingGameObjects(collisionGraph, collisionsAlreadyInGraph);
							GameObject meshObj = Instantiate(topMesh, Vector3.zero, Quaternion.identity);
							TopMesh mesh = meshObj.GetComponent<TopMesh>();
							mesh.generateMesh(vertices);
							return;
						}
						continue;
					}

					// the object that the object we are exploring is colliding with
					GameObject childObj = Array.Find(collisionPoint.objects, o => o.GetInstanceID() != exploringObj.GetInstanceID());

					// create a new node and add it to the graph
					CollisionNode newNode = new CollisionNode();
					newNode.objs = collisionPoint.objects;
					newNode.parentNode = collisionGraph[i];
					newNode.CollisionPoint = collisionPoint.point;
					collisionGraph.Add(newNode);
				}
				exploredObjects.Add(exploringObj);
			}
			i++;
		}
	}

	// TODO: When a line hits a border the border should track the collision
	public Vector3[] getListOfConnectingGameObjects(List<CollisionNode> graph, List<CollisionNode> pathStarters)
	{
		List<Vector2> path = new List<Vector2>();

		CollisionNode collisionNode = pathStarters[0];

		while (collisionNode != null)
		{
			path.Add(collisionNode.CollisionPoint);
			collisionNode = collisionNode.parentNode;
		}

		collisionNode = pathStarters[1];
		List<Vector2> secondHalfPath = new List<Vector2>();
		while (collisionNode.parentNode != null)
		{
			secondHalfPath.Add(collisionNode.CollisionPoint);
			collisionNode = collisionNode.parentNode;
		}

		secondHalfPath.Reverse();
		path.AddRange(secondHalfPath);

		Vector3[] pathArray = new Vector3[path.Count];
		for (int i = 0; i < path.Count; i++)
		{
			pathArray[i] = new Vector3(path[i].x, path[i].y, 0);
		}
		return pathArray;
	}

	public Vector3[] getPolygonVertices(List<CollisionNode> pollygonObjects)
	{
		Vector3[] vertices = new Vector3[pollygonObjects.Count + 1];

		// the last object in the pollygon objects will be the begining 

		return vertices;
	}
}
