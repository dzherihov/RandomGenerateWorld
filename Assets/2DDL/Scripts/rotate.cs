using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	internal Vector3 euler;
	//uint frames;

	// Update is called once per frame
	private void Start () {
		StartCoroutine(rotateNow());
	}


	private IEnumerator rotateNow(){
		 while (true){
			
			euler = transform.localEulerAngles;

			yield return null;
			
			euler.z += 2f;


			transform.localEulerAngles = euler;

			yield return new WaitForEndOfFrame();
		}

	}
}
