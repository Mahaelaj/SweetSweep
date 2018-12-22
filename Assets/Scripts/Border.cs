using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
	}
}
