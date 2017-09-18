using UnityEngine;
using System.Collections;

public class followBody : MonoBehaviour {
	GameObject stone;
	// Use this for initialization
	void Start () {
		stone = GameObject.Find("fixesStone");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = stone.transform.position;
	
	}
}
