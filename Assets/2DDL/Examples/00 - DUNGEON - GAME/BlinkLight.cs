using UnityEngine;
using System.Collections;

public class BlinkLight : MonoBehaviour {

	public GameObject fireGO;
	public Light pointLight;
	public GameObject normalScalarObj;

	private const float mag = 0.051f;
	private float lastOffset;
	private float lastRange;
	private DynamicLight light2d; 

	private Vector3 fireScale;
	private Vector3 lastScale;


	IEnumerator Start () {
		light2d = GetComponent<DynamicLight>();
		if(light2d)
			lastOffset = light2d.LightRadius;

		if(pointLight)
			lastRange = pointLight.range;

		if(normalScalarObj)
			lastScale = normalScalarObj.transform.localScale;


		yield return null;
		if(fireGO)
			fireScale = fireGO.transform.localScale;

		StartCoroutine(updateLoop());

	}

	IEnumerator updateLoop(){

		while(true){
			yield return new WaitForSeconds(.081f);
			float rnd = Random.Range(-1f,1f) * mag;
			yield return null;
			if(fireGO){
				fireScale.x *=  1 * (Mathf.Sign(rnd));
				fireGO.transform.localScale = fireScale;
				yield return null;
			}
			if(light2d){
				pointLight.range = lastRange + rnd;
				yield return null;
				light2d.LightRadius = lastOffset + rnd;
			}

			if(normalScalarObj){
				lastScale.x += rnd*0.05f;
				lastScale.y = lastScale.x;
				normalScalarObj.transform.localScale = lastScale;
				yield return null;
			}

			yield return new WaitForEndOfFrame();
		}
	}

}
