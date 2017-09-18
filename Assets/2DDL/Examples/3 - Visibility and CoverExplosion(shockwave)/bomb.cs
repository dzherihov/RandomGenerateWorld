using UnityEngine;
using System.Collections;

public class bomb : MonoBehaviour {

	DynamicLight thisLight;
	TimerClass timer;
	GameObject player;

	float tmpValue = 0;

	Vector3 pos;


	void Start () {
		thisLight = GameObject.Find("2DLight").GetComponent<DynamicLight>();
		player = GameObject.Find("MartinHead");

		timer = gameObject.AddComponent<TimerClass>();


		// Subscribe timer events //
		timer.OnUpdateTimerEvent += timerUpdate;
		timer.OnTargetTimerEvent += tick;

		timer.InitTimer(1.2f, true);





	}


	void Update(){

		pos.x += Input.GetAxis ("Horizontal") * 20f * Time.deltaTime;
		pos.y += Input.GetAxis ("Vertical") * 20f * Time.deltaTime;
		thisLight.gameObject.transform.position = pos;

		Vector3 martinPos = Input.mousePosition;
		martinPos = Camera.main.ScreenToWorldPoint(martinPos);
		martinPos.z = 0;
		player.transform.position = martinPos;



	}

	
	void tick(){
		thisLight.LightRadius = 1.7f;
		tmpValue *=0;
	}
	void timerUpdate(float value){
	thisLight.LightRadius += value*.8f;
	}
}
