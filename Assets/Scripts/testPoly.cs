using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPoly : MonoBehaviour
{

	float width = 1;
	float height = 1;

	// Use this for initialization
	void Start()
	{
		Sprite o = gameObject.GetComponent<SpriteRenderer>().sprite;
		Vector2[] sv = o.vertices;
		Debug.Log(sv[0]);
		sv[0] = new Vector2(-.4f, .4f);
		sv[1] = new Vector2(.4f, .4f);
		o.OverrideGeometry(sv, o.triangles);
	}
}
