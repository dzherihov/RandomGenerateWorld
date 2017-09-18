#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;


static public class SettingsManager
{

	//[MenuItem("2DDL/Create/Settings AssetBundle")]
	public static void CreateAsset ()
	{
		//CustomAssetUtility.CreateAsset<DynamicLightSetting>();
		SettingsManager.LoadMainSettings ();
	}



	static SerializedObject profile;

	public static Object LoadMainSettings(){
		UnityEngine.Object asset = AssetUtility.LoadAsset<DynamicLightSetting> ("Assets/2DDL/2DLight/Settings", "Settings.asset");
		//UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath<DynamicLightSetting>("Assets/2DDL/2DLight/Settings/Settings.asset");
		//Debug.Log (asset);

		if (asset == null) {
			Debug.Log("Create a new file Settings.asset");
			asset = AssetUtility.CreateAsset<DynamicLightSetting> ("Assets/2DDL/2DLight/Settings", "Settings");
		}

		profile = new SerializedObject(asset);

		/*
		//Automatic Layer Creation
		if(AssetUtility.LoadPropertyAsBool("LayerCreationHasBeenPerformed", profile) == false){
			TagLayerClass.findLayer(TagLayerClass.LayerName);
			TagLayerClass.createLayer();
			AssetUtility.SaveProperty("LayerCreationHasBeenPerformed", true, profile);
			
			//save layer mask//
			AssetUtility.SaveProperty("layer",LayerMask.GetMask(TagLayerClass.LayerName), profile);
		}
		*/


		//Debug.Log("Profile loaded");
		return asset;
	}

	static public string getVersion(){
		if (profile == null)
			LoadMainSettings ();

		return AssetUtility.LoadProperty ("version", profile);
	}
}
#endif