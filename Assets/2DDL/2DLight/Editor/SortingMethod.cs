using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;

[CanEditMultipleObjects()]
[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererSortingLayersEditor : Editor
{
	Renderer renderer;
	string[] sortingLayerNames;
	int selectedOption;


	void OnEnable()
	{
		sortingLayerNames = GetSortingLayerNames();
		renderer = (target as Renderer).gameObject.GetComponent<Renderer>();
		//light2d = (target as DynamicLight);
		
		for (int i = 0; i<sortingLayerNames.Length;i++)
		{
			if (sortingLayerNames[i] == renderer.sortingLayerName)
				selectedOption = i;
		}
	}
	
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		if (!renderer) return;
		
		EditorGUILayout.BeginHorizontal();
		selectedOption = EditorGUILayout.Popup("Sorting Layer", selectedOption, sortingLayerNames);
		if (sortingLayerNames[selectedOption] != renderer.sortingLayerName)
		{
			Undo.RecordObject(renderer, "Sorting Layer");
			renderer.sortingLayerName = sortingLayerNames[selectedOption];
			EditorUtility.SetDirty(renderer);
		}
		EditorGUILayout.LabelField("(Id:" + renderer.sortingLayerID.ToString() + ")", GUILayout.MaxWidth(40));
		EditorGUILayout.EndHorizontal();
		
		int newSortingLayerOrder = EditorGUILayout.IntField("Order in Layer", renderer.sortingOrder);
		if (newSortingLayerOrder != renderer.sortingOrder)
		{
			Undo.RecordObject(renderer, "Edit Sorting Order");
			renderer.sortingOrder = newSortingLayerOrder;
			EditorUtility.SetDirty(renderer);
		}
	}
	
	// Get the sorting layer names
	public string[] GetSortingLayerNames()
	{
		Type internalEditorUtilityType = typeof(InternalEditorUtility);
		PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
		return (string[])sortingLayersProperty.GetValue(null, new object[0]);
	}	

}