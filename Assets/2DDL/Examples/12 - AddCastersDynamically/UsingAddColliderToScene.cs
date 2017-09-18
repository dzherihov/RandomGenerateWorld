using UnityEngine;
using System.Collections;


/// <summary>
/// IMPORTANTE NOTE:
/// Use addColliderToScene public method.
/// is very efficient against addingCasters.cs script
/// </summary>

public class UsingAddColliderToScene : MonoBehaviour {

	bool doing = false;
	DynamicLight light2d;
	

	void Start () {
		light2d = GameObject.FindGameObjectWithTag("2ddl").GetComponent<DynamicLight>();
	}

	

	void Update () {
		if(Input.GetMouseButtonUp(0) && doing == false)
		{
			// Bool parameter
			doing = true;

			// Creation of GO
			GameObject newGo = new GameObject("newColl"+ Time.time);
			newGo.tag = "Respawn";
			Vector3 location = Camera.main.ScreenToWorldPoint( Input.mousePosition);
			location.z = 0;

			// Set as mouse position 
			newGo.transform.position = location;


			if(Input.GetKey(KeyCode.A)){
				//Create the BoxCollider2D
				//----------------------------

				BoxCollider2D polyCol = newGo.AddComponent<BoxCollider2D>();
				polyCol.offset = Vector2.zero;
				polyCol.size = new Vector2(1,1);
								
				// important Step: adding to current 2ddl layer
				polyCol.gameObject.layer = LayerMask.NameToLayer("ShadowLayer-2DDL");
				//Push collider to the main array or vertices
				light2d.addColliderToScene(polyCol);

			}else if(Input.GetKey(KeyCode.B)){
					//Create the BoxCollider2D
					//----------------------------
					
					CircleCollider2D polyCol = newGo.AddComponent<CircleCollider2D>();
					
					polyCol.offset = Vector2.zero;
					polyCol.radius = 2f;
					
					// important Step: adding to current 2ddl layer
					polyCol.gameObject.layer = LayerMask.NameToLayer("ShadowLayer-2DDL");

					//Push collider to the main array or vertices
					light2d.addColliderToScene(polyCol);
			
			}else if(Input.GetKey(KeyCode.C)){
				//Create the BoxCollider2D
				//----------------------------
				
				EdgeCollider2D polyCol = newGo.AddComponent<EdgeCollider2D>();
				
				Vector2[] ColPoints = new Vector2[3];
				ColPoints[0] = new Vector2(0,-1);
				ColPoints[1] = new Vector2(1,-1);
				ColPoints[2] = new Vector2(1,-1.5f);
				polyCol.points= ColPoints;
				
				// important Step: adding to current 2ddl layer
				polyCol.gameObject.layer = LayerMask.NameToLayer("ShadowLayer-2DDL");
				
				//Push collider to the main array or vertices
				light2d.addColliderToScene(polyCol);
				

			}else{

				//Create the polygonCollider2D
				//----------------------------

				PolygonCollider2D polyCol = newGo.AddComponent<PolygonCollider2D>();
				Vector2[] ColPoints = new Vector2[5];
				ColPoints[0] = new Vector2(-1,-1);
				ColPoints[1] = new Vector2(1,-1);
				ColPoints[2] = new Vector2(1,1);
				ColPoints[3] = new Vector2(-.5f,1.5f);
				ColPoints[4] = new Vector2(-1,1);

				polyCol.points = ColPoints;
				
				// important Step: adding to current 2ddl layer
				polyCol.gameObject.layer = LayerMask.NameToLayer("ShadowLayer-2DDL");
				//Push collider to the main array or vertices
				light2d.addColliderToScene(polyCol);
			}

			

		
			//Restoring privileges for creation
			StartCoroutine(restoreDoing());
		}
	}

	IEnumerator restoreDoing(){
		yield return new WaitForSeconds(.2f);
		doing = false;
	}
}
