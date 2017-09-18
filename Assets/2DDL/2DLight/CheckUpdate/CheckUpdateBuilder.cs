#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CheckUpdateManager{
	//[MenuItem("2DDL/Create/Versions AssetBundle")]
	public static void CreateassetForCheckUpdates ()
	{

		EditorUtility.DisplayDialog ("","Este proc Genera un archivo extesion .asset que debera ser subido al server \n y se utilizara para gestionar los check de actualizacioners ", "ok");
		AssetUtility.CreateAsset<CheckUpdateAsset>("", "CheckUpdateAssetFile");
		
	}
}
#endif