using UnityEngine;
using System.Collections;

public class TablaSenoCoseno{

	static bool hasInstanced = false;

	// -- almaceno seno y cose pos cuestiones de performance --//
	public static float []SenArray;
	public static float []CosArray;




	public static void initSenCos(){
		if(hasInstanced == false){
			SenArray = new float[360];
			CosArray = new float[360];
			
			for(int i = 0; i< 360; i++){
				SenArray[i] = Mathf.Sin(i*Mathf.Deg2Rad);
				CosArray[i] = Mathf.Cos(i*Mathf.Deg2Rad);
			}

			hasInstanced = true;
		}
			
	}

}
