using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	[SerializeField] ParticleSystem pressedParticlesStatic;
	ParticleSystem activeParticles;
	[SerializeField] GrowingLineContainer growingLineContainer;
	const float minDragDistance = 50f;

	bool isPressed = false;

	Vector3 startPressPosition;

	void Start()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	void Update()
	{

		if (Input.GetMouseButton(0) && !isPressed)
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				isPressed = true;
				Vector3 inputLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				inputLocation.z = 0;

				startPressPosition = inputLocation;

				activeParticles = Instantiate(pressedParticlesStatic, inputLocation, Quaternion.identity);

				activeParticles.Play();
			}
		}

		if (!Input.GetMouseButton(0) && isPressed)
		{
			isPressed = false;
			Vector3 inputLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			inputLocation.z = 0;

			activeParticles.Stop();

			//if (Vector2.Distance(startPressPosition, inputLocation) < minDragDistance) return;
			GrowingLineContainer growingLine = Instantiate(growingLineContainer, startPressPosition, Quaternion.identity);
			growingLine.vecDiff = startPressPosition - inputLocation;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Vector3 inputLocation = Camera.main.ScreenToWorldPoint(eventData.position);
		inputLocation.z = 0;
		activeParticles = Instantiate(pressedParticlesStatic, inputLocation, Quaternion.identity);
		activeParticles.Play();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		activeParticles.Stop();
		if (Vector2.Distance(eventData.pressPosition, eventData.position) < minDragDistance) return;

		Vector3 inputLocation = Camera.main.ScreenToWorldPoint(eventData.pressPosition);
		inputLocation.z = 0;

		GrowingLineContainer growingLine = Instantiate(growingLineContainer, inputLocation, Quaternion.identity);
		growingLine.vecDiff = eventData.pressPosition - eventData.position;
	}
}
