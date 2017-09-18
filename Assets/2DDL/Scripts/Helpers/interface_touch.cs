using UnityEngine;
using System.Collections;

public class interface_touch: MonoBehaviour {

	public float speed = 3f;

	GameObject cLight;
	GameObject cubeL;
	
	//GUIText UIlights;
	//GUIText UIvertex;


	[HideInInspector] public static int vertexCount;


	IEnumerator Start(){
		cLight = GameObject.Find("2DLight");
		yield return null;
		StartCoroutine (LoopUpdate ());
	}

	// Update is called once per frame
	IEnumerator LoopUpdate () {
		while (true) {

			//if(Input.GetAxis("Horizontal")){
			//light.transform.position = new Vector3 (Input.mousePosition.x -Screen.width*.5f, Input.mousePosition.y -Screen.height*.5f);
			Vector3 pos = cLight.transform.position;
			pos.x += Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
			pos.y += Input.GetAxis ("Vertical") * speed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
			cLight.transform.position = pos;

		}


	}



}
