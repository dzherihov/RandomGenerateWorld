using UnityEngine;
using System.Collections;

public class CasterCollider{

	public Collider2D collider;
	public Vector2[] points;
	public Transform transform;
	public int TotalPointsCount;
	public enum CasterType{
		BoxCollider2d,
		CircleCollider2d,
		PolygonCollider2d,
		EdgeCollider2d
	};

	public CasterType type;

	internal float lastRadius;
	internal float lastZRot;



	internal CasterCollider(PolygonCollider2D coll){
		collider = coll;
		transform = coll.transform;
		TotalPointsCount = coll.GetTotalPointCount();
		points = new Vector2[TotalPointsCount];
		points = coll.points;
		type = CasterType.PolygonCollider2d;
	}

	internal CasterCollider(BoxCollider2D coll){
		collider = coll;
		transform = coll.transform;
		TotalPointsCount = 4; 
		points = new Vector2[TotalPointsCount];
		points = getSquarePoints();
		type = CasterType.BoxCollider2d;
	}

	internal CasterCollider(CircleCollider2D coll, Vector2 lightSource){
		collider = coll;
		transform = coll.transform;
		TotalPointsCount = 2;
		points = new Vector2[TotalPointsCount];
		points = getCirclePoints(lightSource);
		type = CasterType.CircleCollider2d;
	}

	internal CasterCollider(EdgeCollider2D coll){
		collider = coll;
		transform = coll.transform;
		TotalPointsCount = coll.pointCount;
		points = new Vector2[TotalPointsCount];
		points = coll.points;
		type = CasterType.EdgeCollider2d;
	}

	internal Vector2[] getSquarePoints(){

		Collider2D thisBox = collider;

		lastZRot = transform.eulerAngles.z;

		if (lastZRot != 0)
			transform.eulerAngles = Vector3.zero;		
	
		Rect p = new Rect();
	
		// NO ROTATION
		p.x = thisBox.bounds.min.x;
		p.y = thisBox.bounds.min.y;
		p.width = thisBox.bounds.max.x ;
		p.height = thisBox.bounds.max.y;




		Vector2 []poly2DPoints = new Vector2[4];
		poly2DPoints[0].x = p.x;
		poly2DPoints[0].y = p.y;
		
		poly2DPoints[1].x = p.width;
		poly2DPoints[1].y = p.y;

		poly2DPoints[2].x = p.width;
		poly2DPoints[2].y = p.height;

		poly2DPoints[3].x = p.x;
		poly2DPoints[3].y = p.height;


		for (int i = 0; i < TotalPointsCount; i++){
			// To local
			poly2DPoints[i] = transform.InverseTransformPoint(poly2DPoints[i]);
		}

		transform.eulerAngles = new Vector3(0,0,lastZRot);

		return poly2DPoints;


	}

	internal Vector2[] getCirclePoints(Vector2 lightSource){
		
		CircleCollider2D thisCircle = (CircleCollider2D) collider;
		
//		Vector2 center = thisCircle.center;
		float radius = thisCircle.radius * transform.localScale.x* 1.0001f;

		Vector2 finalCirclePos = (Vector2)transform.position; //(Vector2) transform.TransformPoint(new Vector3(center.x, center.y, 0));
		Vector2 lightToCircleDirection = finalCirclePos - lightSource;
		float lightToCircleAngle = Mathf.Atan2(lightToCircleDirection.y, lightToCircleDirection.x);
		//Debug.Log("lightToCircleAngle " + lightToCircleAngle);


		float dist = lightToCircleDirection.magnitude;

		float theta = Mathf.Asin((radius / dist));
		float finalTheta1 = lightToCircleAngle - theta;
		float finalTheta2 = lightToCircleAngle + theta;


		//calculate the tangential vector 
		//remember, the radial vector is (x, y) 
		//to get the tangential vector we flip those coordinates and negate one of them 

		Vector2 p1 = new Vector2(Mathf.Cos( finalTheta1), Mathf.Sin(finalTheta1));

		p1.Normalize();
		p1 *= dist;
		p1 += lightSource;
			
		//---Reverse pending --// 
		theta = lightToCircleAngle + theta;
		Vector2 p2 = new Vector2(Mathf.Cos(finalTheta2), Mathf.Sin(finalTheta2));
		p2.Normalize();
		p2 *= dist;
		p2 += lightSource;

		//Vector2 rr = new Vector2(lightSource.x * p1.x, lightSource.y * p1.y);
		//Debug.DrawLine(lightSource,p1,Color.red);
		//Debug.DrawLine(lightSource,p2,Color.white);

		Vector2 []poly2DPoints = new Vector2[2];
		poly2DPoints[0] = (Vector2)transform.InverseTransformPoint((Vector3) p1);
		poly2DPoints[1] = (Vector2)transform.InverseTransformPoint((Vector3) p2);



		return poly2DPoints;
		
		
	}

	public void recalcTan(Vector2 source){
		points = getCirclePoints(source);
	}

	public void recalcBox(){
		points = getSquarePoints();
	}

	public int getTotalPointsCount(){
		return TotalPointsCount;
	}
}
