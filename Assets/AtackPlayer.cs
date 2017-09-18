using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackPlayer : MonoBehaviour {
    public Rigidbody2D fireBall;
    private float speedFire = 0.3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            FireTD(10);
        }
        else
        if (Input.GetKey(KeyCode.DownArrow))
        {
            FireTD(-10);
        }
        else
        if (Input.GetKey(KeyCode.RightArrow))
        {
            FireRL(10);
        }
        else
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            FireRL(-10);
        }
    }
    void FireRL(int flip)
    {
        Rigidbody2D clone = Instantiate(fireBall, transform.position, Quaternion.identity) as Rigidbody2D;
        clone.velocity = transform.TransformDirection(transform.right * flip);
        clone.transform.right = transform.right;
    }
    void FireTD(int flip)
    {
        Rigidbody2D clone = Instantiate(fireBall, transform.position, Quaternion.identity) as Rigidbody2D;
        clone.velocity = transform.TransformDirection(transform.up * flip);
        clone.transform.up = transform.up;
    }
}
