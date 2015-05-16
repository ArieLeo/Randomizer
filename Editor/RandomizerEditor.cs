using UnityEngine;
using System.Collections;
using UnityEditor;
using OneDayGame;

[CustomEditor(typeof(Randomizer))]
public class RandomizerEditor: GameComponentEditor {

	private SerializedProperty _initDelay;
	private SerializedProperty _interval;
	private SerializedProperty _intervalType;
	private SerializedProperty _minInterval;
	private SerializedProperty _maxInterval;

	public override void OnEnable() {
		base.OnEnable();

		_initDelay = serializedObject.FindProperty("_initDelay");
		_interval = serializedObject.FindProperty("_interval");
		_intervalType = serializedObject.FindProperty("_intervalType");
		_minInterval = serializedObject.FindProperty("_minInterval");
		_maxInterval = serializedObject.FindProperty("_maxInterval");
	}

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		//Randomizer script = (Randomizer)target;
		serializedObject.Update();

		EditorGUILayout.BeginHorizontal();
		EditorGUIUtility.labelWidth = 70;
		EditorGUILayout.PropertyField(
				_initDelay,
				GUILayout.MaxWidth(120));
		EditorGUIUtility.labelWidth = 40;
		EditorGUILayout.PropertyField(
				_intervalType,
				new GUIContent("Type", "Type of the interval applied."));
		EditorGUILayout.EndHorizontal();

		switch (_intervalType.enumValueIndex) {
			case (int)Randomizer.IntervalTypes.Fixed:
				EditorGUIUtility.labelWidth = 0;
				EditorGUILayout.PropertyField(_interval);
			break;
			case (int)Randomizer.IntervalTypes.Random:
				EditorGUILayout.BeginHorizontal();
				EditorGUIUtility.labelWidth = 80;
				EditorGUILayout.PropertyField(_minInterval);
				EditorGUILayout.PropertyField(_maxInterval);
				EditorGUILayout.EndHorizontal();
			break;
		}

		serializedObject.ApplyModifiedProperties();
		// Save changes
		/*if (GUI.changed) {
			EditorUtility.SetDirty(script);
		}*/
	}
}
