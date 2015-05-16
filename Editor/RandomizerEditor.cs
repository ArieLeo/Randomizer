using UnityEngine;
using System.Collections;
using UnityEditor;
using OneDayGame;

namespace Randomizer {

    [CustomEditor(typeof (Randomizer))]
    public class RandomizerEditor : Editor {

        private SerializedProperty initDelay;
        private SerializedProperty interval;
        private SerializedProperty intervalType;
        private SerializedProperty minInterval;
        private SerializedProperty maxInterval;

        private void OnEnable() {
            initDelay = serializedObject.FindProperty("initDelay");
            interval = serializedObject.FindProperty("interval");
            intervalType = serializedObject.FindProperty("intervalType");
            minInterval = serializedObject.FindProperty("minInterval");
            maxInterval = serializedObject.FindProperty("maxInterval");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 70;
            EditorGUILayout.PropertyField(
                initDelay,
                GUILayout.MaxWidth(120));
            EditorGUIUtility.labelWidth = 40;
            EditorGUILayout.PropertyField(
                intervalType,
                new GUIContent("Type", "Type of the interval applied."));
            EditorGUILayout.EndHorizontal();

            switch (intervalType.enumValueIndex) {
                case (int) IntervalTypes.Fixed:
                    EditorGUIUtility.labelWidth = 0;
                    EditorGUILayout.PropertyField(interval);
                    break;
                case (int) IntervalTypes.Random:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUIUtility.labelWidth = 80;
                    EditorGUILayout.PropertyField(minInterval);
                    EditorGUILayout.PropertyField(maxInterval);
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

}
