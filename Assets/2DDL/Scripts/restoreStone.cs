using UnityEngine;
using System.Collections;

public class restoreStone : MonoBehaviour {

	// Use this for initialization
	Vector3 pos;
	Vector3 instancePos;

	void Start () {
		pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		instancePos = gameObject.transform.position;
		if(instancePos.y < -30){
			gameObject.transform.position = pos;
			GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}
	}
}
