using UnityEngine;
using System.Collections;

public class rotate_2 : MonoBehaviour {


	public float speed = 15f;

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
			
			euler.z += speed * Time.deltaTime;

			yield return new WaitForEndOfFrame();
			transform.localEulerAngles = euler;


		}

	}
}
