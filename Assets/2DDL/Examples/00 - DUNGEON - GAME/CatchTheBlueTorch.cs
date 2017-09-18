using UnityEngine;
using System.Collections;

public class CatchTheBlueTorch : MonoBehaviour {


	public GameObject BlueTorch;


	

	IEnumerator Start () {
		if(!BlueTorch)
			Debug.LogError("Setup Blue torch on Inspector");

		yield return null;

		StartCoroutine(LoopUpdate());
	}
	
	// Update is called once per frame
	IEnumerator LoopUpdate () {
		while(true){
			float sqrDistance = (transform.position - BlueTorch.transform.position).sqrMagnitude;
			if(sqrDistance <= 0.02f){
				// Catch the torch
				// Set child of Hero
				GetComponent<followPlayer>().enabled = true;
			}

			yield return null;
			/*
			if(transform.localScale.x < 0){
				Vector3 localTorchScale = BlueTorch.transform.localScale;
				localTorchScale.x *= -1;
				BlueTorch.transform.localScale = localTorchScale;
				BlueTorch.transform.position = new Vector3(-0.148f,0,0);

			}
			*/

			yield return new WaitForEndOfFrame();
		}
	}
}
