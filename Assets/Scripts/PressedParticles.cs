using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedParticles : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<ParticleSystem>().IsAlive()) Destroy(gameObject);
	}
}
