using UnityEngine;
using System.Collections;

public class AnimateFireSprite : MonoBehaviour {


	private const float mag = 0.051f;
	private Vector3 fireScale;
	// Use this for initialization
	IEnumerator Start () {


		fireScale = transform.localScale;
		yield return null;
		StartCoroutine(updateLoop());
	}
	
	// Update is called once per frame
	IEnumerator updateLoop () {
		while (true) {
		
			yield return new WaitForSeconds(.081f);
			float rnd = Random.Range(0f,1f);
			fireScale = transform.localScale;
			yield return null;
			
			if(rnd > .5f)
				fireScale.x *=-1;
			
			gameObject.transform.localScale = fireScale;
			yield return null;	
		}


	}
}
