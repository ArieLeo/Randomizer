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

            DrawInitDelayField();

            EditorGUIUtility.labelWidth = 40;

            DrawIntervalTypeDropdown();

            EditorGUILayout.EndHorizontal();

            HandleIntervalTypeOption();

            serializedObject.ApplyModifiedProperties();
        }

        private void HandleIntervalTypeOption() {
            switch (intervalType.enumValueIndex) {
                case (int) IntervalTypes.Fixed:
                    DrawFixedIntervalTypeFields();
                    break;
                case (int) IntervalTypes.Random:
                    DrawRandomIntervalTypeFields();
                    break;
            }
        }

        private void DrawFixedIntervalTypeFields() {
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.PropertyField(interval);
        }

        private void DrawRandomIntervalTypeFields() {
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.PropertyField(minInterval);
            EditorGUILayout.PropertyField(maxInterval);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawIntervalTypeDropdown() {
            EditorGUILayout.PropertyField(
                intervalType,
                new GUIContent("Type", "Type of the interval applied."));
        }

        private void DrawInitDelayField() {
            EditorGUILayout.PropertyField(
                initDelay,
                GUILayout.MaxWidth(120));
        }

    }

}
