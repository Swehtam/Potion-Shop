using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IngredientsData))]
public class IngredientsDataEditor : Editor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
		SerializedProperty imageProp = serializedObject.FindProperty("Image");
		SerializedProperty goldValueProp = serializedObject.FindProperty("GoldValue");
		SerializedProperty strValueProp = serializedObject.FindProperty("STRValue");
		SerializedProperty intValueProp = serializedObject.FindProperty("INTValue");
		SerializedProperty agiValueProp = serializedObject.FindProperty("AGIValue");

		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.PropertyField(scriptProp);
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.PropertyField(imageProp);
		EditorGUILayout.PropertyField(goldValueProp);

		bool disableSTRValue = intValueProp.intValue != 0 && agiValueProp.intValue != 0;
		bool disableINTValue = strValueProp.intValue != 0 && agiValueProp.intValue != 0;
		bool disableAGIValue = strValueProp.intValue != 0 && intValueProp.intValue != 0;
		
		EditorGUI.BeginDisabledGroup(disableSTRValue);
		EditorGUILayout.PropertyField(strValueProp);
		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(disableINTValue);
		EditorGUILayout.PropertyField(intValueProp);
		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(disableAGIValue);
		EditorGUILayout.PropertyField(agiValueProp);
		EditorGUI.EndDisabledGroup();


		serializedObject.ApplyModifiedProperties();
	}
}
