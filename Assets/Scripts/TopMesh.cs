using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMesh : MonoBehaviour
{
	public Material topMaterial;

	// Use this for initialization
	public void generateMesh(Vector3[] vertices)
	{
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		Vector2[] vertices2d = new Vector2[vertices.Length];
		for (int j = 0; j < vertices.Length; j++)
		{
			vertices2d[j] = new Vector3(vertices[j].x, vertices[j].y);
		}
		Triangulator tr = new Triangulator(vertices2d);
		int[] indices = tr.Triangulate();
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		MeshRenderer renderer = GetComponent<MeshRenderer>();
		MeshFilter filter = GetComponent<MeshFilter>();
		filter.mesh = mesh;
	}
}
