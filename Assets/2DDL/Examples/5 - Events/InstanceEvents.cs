using UnityEngine;
using System.Collections;

public class InstanceEvents : MonoBehaviour {

	DynamicLight light2d;
	float speed = .3f;
	
	
	
	
	IEnumerator Start () {
		// Find and set 2DLight Object //
		light2d = GameObject.Find("2DLight").GetComponent<DynamicLight>() as DynamicLight;
		

		// Add listeners

		light2d.OnExitFieldOfView += onExit;
		light2d.OnEnterFieldOfView += onEnter;

		yield return new WaitForEndOfFrame();
		StartCoroutine(loop());
		
	}
	
	
	IEnumerator loop(){
		while(true){
			Vector3 pos = gameObject.transform.position;
			if(pos.y < -39){
				pos.y = 20;

			}else{
				pos.y -= speed;
			}



			yield return new WaitForEndOfFrame();
			gameObject.transform.position = pos;
		}
	}

	
	
	void onExit(GameObject g){
		Debug.Log("OnExit");
		speed = .3f;
	}
	
	void onEnter(GameObject g){

		if (gameObject.GetInstanceID () == g.GetInstanceID ()) {
			Debug.Log("OnEnter");
			speed = 0.08f;	
		}

	}

}
