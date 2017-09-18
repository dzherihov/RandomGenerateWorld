#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;
using System.Collections;



public class DynamicLightAboutWindow : EditorWindow {

	DynamicLightAboutWindow  window;
	//public Rect _handleArea;
	private bool _nodeOption, _options, _handleActive, _action;
	private Texture2D _rawIcon;
	private GUIContent _icon;
	private float _winMinX, _winMinY;
	private int _mainwindowID;


	// Updates variables
	static string assetFileName = "CheckUpdateAssetFile.asset";
	static string updateUrlAsset = "http://martinysa.com/MyFiles/2DDL/Updates/"; //"http://martinysa.com/MyFiles/2DDL/Updates/DynamicLightServerAsset.asset";
	bool isWWWDone = false;
	WWW remoteLogUpdates;
	static bool _init;

	bool doAction = false;

	bool enableBtn = false;
	string destinyFolder;

	SerializedObject profile;
	int framesCount = 0;

	UnityEngine.Object asset;

	// Layouts
	GUIStyle style1;

	private struct checkVersionStruct{
		public string version;
		public string dataReleased;
		public string changeLog;
		public string link;
	}

	private checkVersionStruct _chVersion;
	private Texture2D m_Logo = null;

	[MenuItem("2DDL/About")]
	public static void Init()
	{
		_init = true;

		//window = (DynamicLightAboutWindow )EditorWindow.GetWindow(typeof(DynamicLightAboutWindow));
		EditorWindow.GetWindow( typeof(DynamicLightAboutWindow), true, "2DDL about" );
		//Debug.Log ("Init");


	}

	void OnEnable(){
		//m_Logo = (Texture2D)Resources.Load("logo2ddl",typeof(Texture2D));
		m_Logo = (Texture2D)AssetDatabase.LoadAssetAtPath ("Assets/2DDL/2DLight/Misc/logo2DDL.png", typeof(Texture2D));
	}

 	void OnFocus(){
		//EditorApplication.update += editorCallbackUpdate;
	}

	void OnLostFocus(){
		//EditorApplication.update -= editorCallbackUpdate;
	}

	void OnDestroy(){
		EditorApplication.update -= editorCallbackUpdate;
		if (System.IO.File.Exists (destinyFolder)) {
			System.IO.File.Delete(destinyFolder);
		}
		Debug.Log ("Ondestroy");
	}



	void OnGUI()
	{
			
		if (_init) {
			_init = false;
			StartDownload();
		}

		style1 = new GUIStyle(GUI.skin.label);
		style1.fontStyle = FontStyle.Bold;
		style1.alignment = TextAnchor.MiddleCenter;
		//style1.

		GUILayout.Label(m_Logo);

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		//GUILayout.Label("v1.3.1", style1);

		//GUI.contentColor = Color.black;
		GUI.Label (new Rect (50, 70, 200, 60), "v" + SettingsManager.getVersion());


		if (GUI.Button (new Rect(120,20,200,60),"Documentation")) {
			Application.OpenURL("http://martinysa.com/2d-dynamic-lights-doc/");
		}
		if (GUI.Button (new Rect(325,20,80,60),"AssetStore")) {
			Application.OpenURL("http://u3d.as/asp");
		}

		if (GUI.Button (new Rect(410,20,80,60),"Contact")) {
			Application.OpenURL("mailto:info@martinysa.com");
		}
		GUILayout.EndHorizontal();



		// si se ha leido desde server
		if(enableBtn){

			LoadCheckUpdateAsset();

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			GUILayout.Label("Remote Version");
			if (GUILayout.Button ("Reload")) {
				LoadCheckUpdateAsset();
			}

			EditorGUILayout.Separator();
			GUILayout.Label("Lastest Version:    " + _chVersion.version);
			GUILayout.Label("Date Released:    " + _chVersion.dataReleased);
			GUILayout.Label("ChangeLog:    " + _chVersion.changeLog);
			GUILayout.Label("Link:    " + _chVersion.link);

			if(GUILayout.Button("Download")){
				Application.OpenURL(_chVersion.link);

			}
			if(GUILayout.Button("Open ChangeLog")){
				Application.OpenURL(_chVersion.changeLog);
				
			}

		}else{
			string lbl1 = "Checking new version";
			string lbl2 = " .";
			if(framesCount == 0){
				lbl2 = " .";
			}else if(framesCount == 1){
				lbl2 = " ..";
			}else if(framesCount == 2){
				lbl2 = " ...";
			}else if(framesCount == 3){
				lbl2 = " ....";
			}else if(framesCount == 4){
				lbl2 = " .....";
			}else if(framesCount == 5){
				lbl2 = " ......";
				framesCount *=0;
			}

			framesCount++;

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			GUILayout.Label(lbl1 + lbl2);
		}

	}

	void StartDownload(){
		EditorApplication.update += editorCallbackUpdate;
		
		AssetDatabase.Refresh ();
		
		//Start downloading updates log Asset
		Debug.Log ("donwloadin started");
		// Full path
		updateUrlAsset += assetFileName;
		
		remoteLogUpdates = new WWW (updateUrlAsset);
		
		destinyFolder = Application.dataPath;
		destinyFolder = destinyFolder.Substring(0, destinyFolder.Length - 5);
		destinyFolder = destinyFolder.Substring(0, destinyFolder.LastIndexOf("/"));
		destinyFolder += "/Assets/" + assetFileName;//"/Assets/2DDL/2DLight/CheckUpdate/" + assetFileName;
	}

	void editorCallbackUpdate(){
		if (remoteLogUpdates != null && remoteLogUpdates.isDone)
			isWWWDone = true;

		if (isWWWDone && !doAction)
			syncUpdateInfo();


	}


	void syncUpdateInfo(){

#if !UNITY_WEBPLAYER

		doAction = true;

		Debug.Log ("downloaded");

		Debug.Log ("parsing...");

		byte[] data = remoteLogUpdates.bytes;



		//if (System.IO.File.Exists (destinyFolder)) {
		//	System.IO.File.Delete(destinyFolder);
		//}
		System.IO.File.WriteAllBytes(destinyFolder, data);


//		System.IO.FileStream fs;
//		fs = System.IO.File.Create(destinyFolder);
//		fs.Write(data,1,5);
//		fs.Close ();
//		

		AssetDatabase.Refresh ();

		//AssetDatabase.RemoveUnusedAssetBundleNames ();

		enableBtn = true;
#else

		Debug.LogWarning("Can't check updates under Web Player platform. Change your project to another target in File -> Build Settings");
#endif



	}

	void LoadCheckUpdateAsset(){
		asset = AssetDatabase.LoadAssetAtPath<CheckUpdateAsset> ("Assets/" + assetFileName);
		Debug.Log (destinyFolder);

		if (asset == null) {
			Debug.LogError("Can't load Updates Log File");
			return;
		}

		Debug.Log(asset);


		
		profile = new SerializedObject(asset);

		if (profile != null) {
			Debug.Log("Profile loaded");

			_chVersion.version = AssetUtility.LoadProperty("version", profile);
			_chVersion.dataReleased = AssetUtility.LoadProperty("dateReleased", profile);
			_chVersion.changeLog = AssetUtility.LoadProperty("changeLog", profile);
			_chVersion.link = AssetUtility.LoadProperty("link", profile);
		}


	}

}

#endif
