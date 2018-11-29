using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweet : MonoBehaviour {

    [SerializeField] float speed = 1f;
    Rigidbody2D rb;
    Vector2 curVel;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity != Vector2.zero) curVel = rb.velocity;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "MovingLine")
        {
            rb.velocity = curVel * -1;
        }
    }
}
