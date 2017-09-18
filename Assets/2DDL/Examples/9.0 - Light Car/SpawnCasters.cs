using UnityEngine;
using System.Collections;

public class SpawnCasters : MonoBehaviour {

	GameObject []casters;
	GameObject line;



	void Start () {
		casters = new GameObject[7];


		casters[0] = GameObject.Find("square");
		line = GameObject.Find("line");

		for (int i =1 ; i<casters.Length; i++){
			casters[i] = Instantiate(casters[0]) as GameObject;
			casters[i].transform.position = new Vector3(Random.Range(-1f,1f) * 35f, Random.Range(-1f,1f) * 35f, 0);
		}

		// --------------------  TIP  --------------------------------------------------------------
		// Already created casters procedurally is IMPORTATANT set the Lights to StaticScene = true
		// for avoiding check every frame for new colliders.
		StartCoroutine(convertToStaticLights());



	}


	IEnumerator convertToStaticLights(){
		yield return new WaitForSeconds(.5f);

		GameObject[] lights = GameObject.FindGameObjectsWithTag("2ddl");
		
		for(int i = 0; i< lights.Length ;i++){
			DynamicLight dyn = lights[i].GetComponent<DynamicLight>();
			dyn.staticScene = true;
		}


		StartCoroutine(FrameUpdate());
	}
	
	// Update is called once per frame
	IEnumerator FrameUpdate () {
		while (true){

			yield return new WaitForEndOfFrame();
			
			for (int i =0 ; i<casters.Length; i++){
				Vector3 cPos = casters[i].transform.position;
				
				if(cPos.y < -30){
					cPos.y = 35;
					cPos.x = Random.Range(-1f,1f) * 15f;
					if(cPos.x < 0){
						cPos.x -= 12;
					}
					if(cPos.x > 0){
						cPos.x += 12;
					}
				}else{
					cPos.y -= 0.5f;
				}
				
				
				casters[i].transform.position = cPos;
			}
			
			
			Vector3 lPos = line.transform.position;
			if(lPos.y < -30){
				lPos.y = 40;
			}else{
				lPos.y -= 0.5f;
			}
			line.transform.position = lPos;
		}
	}
}
