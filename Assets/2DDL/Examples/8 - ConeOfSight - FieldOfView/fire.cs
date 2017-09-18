using UnityEngine;
using System.Collections;



public class fire : MonoBehaviour {

	DynamicLight lineOfSight;
	GameObject [] ghostInScene;
	GameObject [] bullets;
	GameObject vehicle;

	AudioSource audioSrc;

	bool grow = false;


	IEnumerator Start () {

		ghostInScene = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		vehicle = GameObject.FindGameObjectWithTag("vehicle");

		bullets = new GameObject[20];
		for(int i = 0; i<bullets.Length; i++){
			//bullets[i].AddComponent<>();
		}


		yield return new WaitForSeconds(1);

		lineOfSight = GameObject.FindGameObjectWithTag("gun").GetComponent<DynamicLight>();
		if(lineOfSight)
			lineOfSight.InsideFieldOfViewEvent += iSawAGhost;

		audioSrc = GetComponent<AudioSource>();

		StartCoroutine (SightVariance());

	}
	


	void iSawAGhost(GameObject[] g){
		bool found = false;

		foreach(GameObject gs in g){
			foreach(GameObject ghost in ghostInScene){
				if(ghost.GetInstanceID() == gs.GetInstanceID()){
					if(ghost.tag == "ghost"){
						//Debug.Log("find");
						found = true;
						shot(ghost);
						break;
					}
				}
			}
		}


		if(found == true){
			vehicle.GetComponent<Renderer>().material.color = Color.red;

		}
	}

	internal void shot(GameObject obj){
#if UNITY_EDITOR
		GLDebug.DrawLine(lineOfSight.transform.position, obj.transform.position,Color.green);

#endif

		if(! audioSrc.isPlaying){
			audioSrc.Play();
			StartCoroutine(restoreAudio());
		}
		
	}

	IEnumerator restoreAudio(){
		yield return new WaitForSeconds(.2f);
		audioSrc.Stop();
		vehicle.GetComponent<Renderer>().material.color = Color.white;
	}

	IEnumerator SightVariance(){

		while (true)
		{
			//yield return new WaitForSeconds (.1f);
			if (lineOfSight.RangeAngle <= 20 && grow == false)
				grow = true;
			if (lineOfSight.RangeAngle > 150 && grow == true)
				grow = false;
			
			if (grow == true) {
				lineOfSight.RangeAngle++;
			} else {
				lineOfSight.RangeAngle--;		
			}				
			yield return new WaitForEndOfFrame ();
		}



	}

}
