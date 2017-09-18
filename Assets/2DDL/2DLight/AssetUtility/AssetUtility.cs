#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;

using System.Collections;

public class AssetUtility{
	//static SerializedObject profile;

	public static UnityEngine.Object CreateAsset<T> (string atPath = "", string name = "unNamed") where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		//string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/" + name + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
		
		return asset;
	}

	public static Object LoadAsset<T>(string atPath, string name) where T : ScriptableObject{
		return AssetDatabase.LoadAssetAtPath<T> (atPath + "/" + name);
	}



	public static string LoadProperty(string property, SerializedObject profile){
		if (profile == null)
			return null;
		
		SerializedProperty prop = profile.FindProperty (property);
		return prop.stringValue;
	}
	public static bool LoadPropertyAsBool(string property,SerializedObject profile){
		if (profile == null)
			return false;
		
		SerializedProperty prop = profile.FindProperty (property);
		return prop.boolValue;
	}
	public static int LoadPropertyAsInt(string property, SerializedObject profile){
		if (profile == null)
			return 0;
		
		SerializedProperty prop = profile.FindProperty (property);
		return prop.intValue;
	}
	public static void SaveProperty(string property, string value, SerializedObject profile){
		if (profile == null)
			return;
		
		SerializedProperty prop = profile.FindProperty (property);
		prop.stringValue = value;
		profile.ApplyModifiedProperties ();
	}
	public static void SaveProperty(string property, bool value, SerializedObject profile){
		if (profile == null)
			return;
		
		SerializedProperty prop = profile.FindProperty (property);
		prop.boolValue = value;
		profile.ApplyModifiedProperties ();
	}
	public static void SaveProperty(string property, int value, SerializedObject profile){
		if (profile == null)
			return;
		
		SerializedProperty prop = profile.FindProperty (property);
		prop.intValue = value;
		profile.ApplyModifiedProperties ();
	}

}

#endif
