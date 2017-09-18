using UnityEngine;
//using UnityEditor;
using System.Collections;

public class ProceduralLightAndCasters : MonoBehaviour {

	//publics
	public Object LightPrefab;
	public float spacing = 30;
	public Sprite sprite;


	//privates
	Vector3 initPos = Vector3.zero;
	GameObject [] allGOBoxColliders;

	IEnumerator Start () {
		// instantiate GO's
		allGOBoxColliders = new GameObject[30];

		//Create the gameObjects
		Debug.Log ("Crafting " + allGOBoxColliders.Length + " Game Objects");
		for (int i=0; i<allGOBoxColliders.Length; i++) 
		{
			// Create a empty Game Object
			allGOBoxColliders[i] = new GameObject("Caster_" + i);

			// Add sprite renderer component
			SpriteRenderer sr = allGOBoxColliders[i].AddComponent<SpriteRenderer>();

			// Assign a public sprite
			if(sprite)
			sr.sprite = sprite;

			//This value will be used for give a size on a boxCollider
			float _size = sr.bounds.max.x * 2;

			//Create a boxCollider for each GO
			BoxCollider2D boxColl2d = allGOBoxColliders[i].AddComponent<BoxCollider2D>();
			boxColl2d.offset = Vector2.zero;
			boxColl2d.size = new Vector2(_size,_size);

			// set dynamic position
			allGOBoxColliders[i].transform.position = initPos;
			// Increment X coord
			initPos.x += spacing;

			// Jump Y coord when is needed
			if(i == 9 || i == 19){
				initPos.y -= spacing;
				initPos.x = 0;
			}


			// ---------- IMPORTANT STEP -------- //
			// Followings lines ensure the correct functionality of 2DDL system

			// 1st adding GO to correct Layer
			allGOBoxColliders[i].layer = LayerMask.NameToLayer("ShadowLayer-2DDL");

			//2nd apply a tiny rotation over Z AXIS for prevent weird results in 2DDL mesh generation
			allGOBoxColliders[i].transform.localEulerAngles = new Vector3(0,0,0.0001f);

			// ---------- END IMPORTANT STEP --- //

			// wait one frame
			}

		Debug.Log ("Setup the 2DLight ...");
		yield return new WaitForSeconds (1);
		// Finally add a point of 2D Light 

		if (LightPrefab.GetType () != typeof(GameObject)) {
			Debug.Log("Light created") ;
		}
			

		GameObject gameObject2DDL = Instantiate((GameObject)LightPrefab);
		DynamicLight s = gameObject2DDL.GetComponent<DynamicLight> ();

		//set Light2d Layer
		s.Layer = LayerMask.GetMask ("ShadowLayer-2DDL");
		s.LightRadius = 40;


		// Center screen
		gameObject2DDL.transform.position = new Vector3 (135, 30, 0);


		Debug.Log ("Increment 2DLight radius...");
		// Change radius gradually
		for (int i=0; i< 200; i++) {
			s.LightRadius += 1;
			yield return new WaitForEndOfFrame();
		}

		Debug.Log ("Finished");

		// wait end of frame
		yield return new WaitForEndOfFrame();

	}
	

}
