using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	Animator Anim;

	GameObject[] floor;
	GameObject[] LightsList;

	Vector3 tempPosVec;

	float DeltaWalk = 0.9f;

	IEnumerator Start () {

		// Init Floor
		floor = new GameObject[12];
		floor[0] = GameObject.Find("floorTile");

		for(int i=1; i< floor.Length; i++)
		{
			floor[i] = (GameObject) Instantiate(floor[0]);
			tempPosVec = floor[i-1].transform.position;
			tempPosVec.x += floor[i].GetComponent<SpriteRenderer>().bounds.size.x;
			floor[i].transform.position = tempPosVec;
		}



		yield return null;


		// Init LightsList
		LightsList = new GameObject[3];
		LightsList[0] = GameObject.Find("Torch");

		tempPosVec = LightsList[0].transform.position;

		for(int i=1; i< LightsList.Length; i++)
		{
			LightsList[i] = (GameObject) Instantiate(LightsList[0]);
			tempPosVec.x += (i + 2.5f);
			LightsList[i].transform.position = tempPosVec;
		}




		tempPosVec = Vector3.zero;
		yield return new WaitForEndOfFrame();

		StartCoroutine(LoopUpdate());

		Anim = GameObject.Find("Hero").GetComponent<Animator>();
		Anim.Play( Animator.StringToHash( "Run" ) );

	}
	
	// Update is called once per frame
	IEnumerator LoopUpdate () {
		while (true){

			// Run floor
			for(int i=0; i< floor.Length; i++){
				if(floor[i].transform.position.x < -(Camera.main.orthographicSize *2.8f)){ 
					tempPosVec = floor[i].transform.position;
					for(int z=0; z<floor.Length; z++){
						if(floor[z].transform.position.x > tempPosVec.x){
							tempPosVec = floor[z].transform.position;
						}

							
					}

					floor[i].transform.position = new Vector3(tempPosVec.x + floor[i].GetComponent<SpriteRenderer>().bounds.size.x,0,0);
				}else{
					tempPosVec = floor[i].transform.position;
					tempPosVec.x -= DeltaWalk * Time.deltaTime;
					floor[i].transform.position = tempPosVec;
				}
			}

			yield return new WaitForEndOfFrame();


			// Run Lights
			for(int i=0; i< LightsList.Length; i++){
				if(LightsList[i].transform.position.x < -(Camera.main.orthographicSize *3.2f)){ 
					int tileLastPlace = 0;
					tempPosVec = LightsList[i].transform.position;
					for(int z=0; z<LightsList.Length; z++){
						if(LightsList[z].transform.position.x > tempPosVec.x){
							tempPosVec = LightsList[z].transform.position;
							tileLastPlace = z;
						}
					}
					
					LightsList[i].transform.position = new Vector3(LightsList[tileLastPlace].transform.position.x + 3.2f,0,0);
					DynamicLight dynLit = LightsList[i].transform.FindChild("2DLight").GetComponent<DynamicLight>();
					Light pointLight = LightsList[i].transform.FindChild("PointLight").GetComponent<Light>(); 
					int rnd = Random.Range(0,4);
					if( rnd == 0 ){
						// Blue light
						dynLit.LightColor = new Color(10/255f,0,86/255f);
					}else if( rnd == 1 ){
						// Red light
						dynLit.LightColor = new Color(116/255f,34/255f,34/255f);
					}else if( rnd == 2 ){
						// green light
						dynLit.LightColor = new Color(29/255f,54/255f,89/255f);
					}else if( rnd == 3 ){
						// gray light
						dynLit.LightColor = new Color(197/255f,107/255f,96/255f);
					}else if( rnd == 4 ){
						// Cyan light
						dynLit.LightColor = Color.cyan;
					}

					pointLight.color = dynLit.LightColor;
				}else{
					tempPosVec = LightsList[i].transform.position;
					tempPosVec.x -= DeltaWalk * Time.deltaTime;
					LightsList[i].transform.position = tempPosVec;
				}
			}

			yield return new WaitForEndOfFrame();
		}

	}
}
