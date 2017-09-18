using UnityEngine;
using System.Collections;


/*
 *IMPORTANT NOTE: THIS METHOD IS UNUPDATED AND UNEFFICIENT. USE "UsingAddColliderToScene.cs" INSTEAD 
 *MARTIN YSA
 */




public class addingCastersUnefficient: MonoBehaviour {

	bool doing = false;
	DynamicLight light2d;

	// Use this for initialization
	void Start () {
		light2d = GameObject.FindGameObjectWithTag("2ddl").GetComponent<DynamicLight>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonUp(0) && doing == false)
		{
			doing = true;
	
			GameObject newGo = new GameObject("newColl"+ Time.time);
			PolygonCollider2D polyCol = newGo.AddComponent<PolygonCollider2D>();
			Vector3 location = Camera.main.ScreenToWorldPoint( Input.mousePosition);
			location.z = 0;


			Vector2[] ColPoints = new Vector2[4];
			ColPoints[0] = new Vector2(-1,-1);
			ColPoints[1] = new Vector2(1,-1);
			ColPoints[2] = new Vector2(1,1);
			ColPoints[3] = new Vector2(-1,1);

			polyCol.points = ColPoints;

			newGo.layer = LayerMask.NameToLayer("ShadowLayer-2DDL");

			newGo.transform.position = location;

			// If StaticScene is unchecked, this following
			//condition doesn't have relevance
			if(light2d){
				light2d.GetCollidersOnScene();
			}
			//-----------------------------------

			StartCoroutine(restoreDoing());
		}
	}

	IEnumerator restoreDoing(){
		yield return new WaitForSeconds(.2f);
		doing = false;
	}
}
