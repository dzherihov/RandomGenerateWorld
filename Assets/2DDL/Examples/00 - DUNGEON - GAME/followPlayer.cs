using UnityEngine;
using System.Collections;




public class followPlayer : MonoBehaviour {

	public GameObject GO2ddl;
	public GameObject HeroGO;

	Vector2 heroLastPos;
	Vector3 delta;
	DynamicLight lit;

	void Start(){
		if(HeroGO)
			heroLastPos = (Vector2) HeroGO.transform.position;

		if(GO2ddl)
			lit = GO2ddl.transform.FindChild("2DLight").GetComponent<DynamicLight>();

		resetDelta();


	}

	internal void resetDelta(){
		delta = new Vector3(0.148f,0,0);
	}

	// Update is called once per frame
	void LateUpdate () {
		if(HeroGO){




			if(heroLastPos != (Vector2)HeroGO.transform.position){
				if(lit.RangeAngle < 360f)
					delta.x *= HeroGO.transform.localScale.x;

				delta += HeroGO.transform.position;

				if(GO2ddl)
					GO2ddl.transform.position = (Vector3)delta;

				heroLastPos = (Vector2)HeroGO.transform.position;

				if(lit.RangeAngle < 360f){
					if(HeroGO.transform.localScale.x < 0){
						lit.gameObject.transform.localEulerAngles = new Vector3(0,0,90);
					}else if(HeroGO.transform.localScale.x > 0){
						lit.gameObject.transform.localEulerAngles = new Vector3(0,0,270);
					}
				}

				resetDelta();


			}

		}
	}
}
