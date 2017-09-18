using UnityEngine;
using System.Collections;

public class ReachedEventManager : MonoBehaviour {

	DynamicLight light2d;
	GameObject[] GOsReached;
	TextMesh text;




	void Start () {
		// Find and set 2DLight Object //
		light2d = GameObject.Find("2DLight").GetComponent<DynamicLight>() as DynamicLight;

		// Find and set text obj //
		text = GameObject.Find("text").GetComponent<TextMesh>();

		// Add listeners
		light2d.InsideFieldOfViewEvent += waveReach;
		light2d.OnExitFieldOfView += onExitCaster;
		light2d.OnEnterFieldOfView += onEnterCaster;

	}



	//- this function iterate in each object passed by 2DLigh script and compare if this object is the player
	//-- game object --//

	//-- THIS SCRIPT MUST BE ATTACHED TO PLAYER GO --//

	void waveReach(GameObject[] g){

		bool found = false;
		string gsName = "";

		foreach(GameObject gs in g){
			//Debug.Log(" id: " + gs.GetInstanceID());

			if(gameObject.GetInstanceID() == gs.GetInstanceID()){
				found = true;
				gsName = gs.name;
			}
		}
		if(found == true){
			text.text = "PLAYER REACHED!!  _" + gsName +"__" + Time.time;
		}else{
			text.text = "in safe place";
		}


	}


	void onExitCaster(GameObject g){
		Debug.Log("OnExit");
	}

	void onEnterCaster(GameObject g){
		Debug.Log("OnEnter");
	}


}
