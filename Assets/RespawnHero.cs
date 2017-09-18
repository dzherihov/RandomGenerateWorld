using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHero : MonoBehaviour {
    public GameObject Hero;
    Vector3 respawn;
	// Use this for initialization
	void Start () {
        
        respawn = new Vector3(26, -24, 0);
        Instantiate(Hero, respawn, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
