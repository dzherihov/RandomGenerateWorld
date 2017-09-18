using UnityEngine;
using System.Collections;

//-- Timer Class -- Easy handle timer in C# --//
//--by  Martin Ysa --//


public class TimerClass : MonoBehaviour {

	// Event Handler
	public delegate void OnTimerTargetDelegate();
	public event OnTimerTargetDelegate OnTargetTimerEvent;

	public delegate void OnTimerUpdateDelegate(float currentDelta);
	public event OnTimerUpdateDelegate OnUpdateTimerEvent;

	
	public float Delay = 0;
	public bool Loop = false;

	bool enable = false;
	float cDeltaTime;
	TimerClass _timerInstance;


	public void InitTimer(float _delay = 0, bool _loop = false){
		Delay = _delay;
		Loop = _loop;
		enable = true;
	}



	

	void Update () {
		if(enable == false)
			return;

		if(cDeltaTime <= Delay){
			cDeltaTime += Time.deltaTime;
			OnUpdateTimerEvent(cDeltaTime);
		}else{
			cDeltaTime *=0;
			OnTargetTimerEvent(); 

			if(Loop == false)
				enable = false;
		}

	}



}
