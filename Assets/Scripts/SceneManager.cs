using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
	[SerializeField] Text livesText;
	[SerializeField] int lives = 5;
	public GameObject[] sweets;
	public static SceneManager instance = null;
	public GameObject boardMesh = null;

	public GameObject leftBorder;
	public GameObject rightBorder;
	public GameObject topBorder;
	public GameObject bottomBorder;
	public struct CollisionPoint
	{
		public GameObject[] objects;
		public Vector2 point;

		public bool isBorder;
	}

	public List<CollisionPoint> collisionPoints = new List<CollisionPoint>();
	public CollisionPoint AddCollidingObject(GameObject[] objects, Vector2 point, bool isBorder)
	{
		CollisionPoint collisionPoint = new CollisionPoint();
		collisionPoint.objects = objects;
		collisionPoint.point = point;
		collisionPoint.isBorder = isBorder;
		collisionPoints.Add(collisionPoint);
		return collisionPoint;
	}

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a SceneManager.
			Destroy(gameObject);
	}

	private void Start()
	{
		sweets = GameObject.FindGameObjectsWithTag("Sweet");
		AddCollidingObject(new GameObject[] { topBorder, leftBorder }, new Vector2(-5.86f, 4.9f), true);
		AddCollidingObject(new GameObject[] { topBorder, rightBorder }, new Vector2(8.78f, 4.9f), true);
		AddCollidingObject(new GameObject[] { bottomBorder, rightBorder }, new Vector2(8.78f, -4.9f), true);
		AddCollidingObject(new GameObject[] { bottomBorder, leftBorder }, new Vector2(-5.86f, -4.9f), true);
	}


	public void loseLife()
	{
		lives--;
		livesText.text = lives.ToString();

		if (lives <= 0) loseLevel();
	}

	void loseLevel()
	{

	}
}
