using UnityEngine;
using System.Collections;

public class DeleteCasters : MonoBehaviour {
	//Publics
	public GameObject SceneLight;

	// Privates
	DynamicLight dl;
	GameObject [] casters;
	bool key = false;


	void Start(){
		// Load into array all gameobjects with '2ddl' tag
		casters = GameObject.FindGameObjectsWithTag ("2ddl");
		// instantiate a dynamicLight component from SceneLight GameObject
		dl = SceneLight.GetComponent<DynamicLight>(); 
	}


	// Update is called once per frame
	void Update () {
		// Simple method that check a key pressed
		if (Input.anyKey && key == false) {
			key = true;	
			// if exist a key, call the delete process
			StartCoroutine( deleteProc());
		}
	}

	IEnumerator deleteProc(){
		// Iterate each object o array
		foreach (GameObject g in casters) {
			// if object is not null, destroy
			if(g != null){
				// destroy and dont wait
				DestroyImmediate(g);
				if(dl){
					// if exist a light, then wait for end of frame for avoid rendering issues
					yield return new WaitForSeconds(.01f);
					// call rebuild, and light will be updated
					dl.Rebuild();

				}
				break;
			}
				
		}
		//restore key value after .1 sec
		yield return new WaitForSeconds(.1f);
		key = false;
	}

}
