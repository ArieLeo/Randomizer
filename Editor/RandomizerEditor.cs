﻿// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
// 
// This file is part of the Randomizer extension for Unity. Licensed under the
// MIT license. See LICENSE file in the project root folder.

using UnityEditor;
using UnityEngine;

namespace RandomizerEx {

    [CustomEditor(typeof (Randomizer))]
    public class RandomizerEditor : Editor {
        #region SERIALIZED PROPERTIES

        private SerializedProperty initDelay;
        private SerializedProperty interval;
        private SerializedProperty intervalType;
        private SerializedProperty maxInterval;
        private SerializedProperty minInterval;
        private SerializedProperty triggerCallback;
        private SerializedProperty initStateOnProbability;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawInitStateOnProbabilitySlider();

            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 70;

            DrawInitDelayField();

            EditorGUIUtility.labelWidth = 40;

            DrawIntervalTypeDropdown();

            EditorGUILayout.EndHorizontal();

            HandleIntervalTypeOption();

            EditorGUILayout.Space();

            DrawTriggerCallback();

            serializedObject.ApplyModifiedProperties();
        }
        private void OnEnable() {
            initDelay = serializedObject.FindProperty("initDelay");
            interval = serializedObject.FindProperty("interval");
            intervalType = serializedObject.FindProperty("intervalType");
            minInterval = serializedObject.FindProperty("minInterval");
            maxInterval = serializedObject.FindProperty("maxInterval");
            triggerCallback = serializedObject.FindProperty("triggerCallback");
            initStateOnProbability =
                serializedObject.FindProperty("initStateOnProbability");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR METHODS
        private void DrawInitStateOnProbabilitySlider() {
            initStateOnProbability.floatValue = EditorGUILayout.Slider(
                new GUIContent(
                    "Init Probability",
                    "Probability that the initial state will be on."),
                initStateOnProbability.floatValue,
                0,
                1);
        }

        private void DrawTriggerCallback() {
            EditorGUILayout.PropertyField(
                triggerCallback,
                new GUIContent(
                    "Trigger Callback",
                    "Callback executed in time intervals."));
        }

        private void DrawFixedIntervalTypeFields() {
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.PropertyField(
                interval,
                new GUIContent(
                    "Interval",
                    "How often to change state (in seconds)."));
        }

        private void DrawInitDelayField() {
            EditorGUILayout.PropertyField(
                initDelay,
                new GUIContent(
                    "Init. Delay",
                    "Initial delay applied after entering into play mode. " +
                    "If delay is set to 3 and fixed time is set to 5 then " +
                    "first trigger will occur after 8 seconds."),
                GUILayout.MaxWidth(120));
        }

        private void DrawIntervalTypeDropdown() {
            EditorGUILayout.PropertyField(
                intervalType,
                new GUIContent("Type", "Type of the interval."));
        }

        private void DrawRandomIntervalTypeFields() {
            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 80;

            EditorGUILayout.PropertyField(
                minInterval,
                new GUIContent(
                    "Min. Interval",
                    "Minimum interval before changing state."));

            EditorGUILayout.PropertyField(
                maxInterval,
                new GUIContent(
                    "Max. Interval",
                    "Maximum interval before changing state."));

            EditorGUIUtility.labelWidth = 0;

            EditorGUILayout.EndHorizontal();
        }

        private void HandleIntervalTypeOption() {
            switch (intervalType.enumValueIndex) {
                case (int) IntervalType.Fixed:
                    DrawFixedIntervalTypeFields();
                    break;

                case (int) IntervalType.Random:
                    DrawRandomIntervalTypeFields();
                    break;
            }
        }

        #endregion INSPECTOR METHODS

        #region METHODS

        [MenuItem("Component/Randomizer")]
        private static void AddRandomizerComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(
                    typeof (Randomizer));
            }
        }

        #endregion METHODS
    }

}