using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] ParticleSystem pressedParticlesStatic;
    ParticleSystem activeParticles;
    [SerializeField] CandyCaneContainer candyCaneContainerPrefab;
    const float minDragDistance = 50f;

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



        CandyCaneContainer candyCane = Instantiate(candyCaneContainerPrefab, inputLocation, Quaternion.identity);
        candyCane.vecDiff = eventData.pressPosition - eventData.position;
    }
}
