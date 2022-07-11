using UnityEngine;
using System.Collections;
using UnityEditor;
namespace ParthViradiya.Core {
    [CustomEditor(typeof(Bindings.ActiveBinding))]
    public class NewMonoBehaviour : Editor {
        Bindings.ActiveBinding activeBinding;

        SerializedProperty viewModel;
        SerializedProperty gObject;
        SerializedProperty source;

        private void OnEnable() {
            activeBinding = (Bindings.ActiveBinding)target;
            viewModel = serializedObject.FindProperty("ViewModel");
            gObject = serializedObject.FindProperty("Target");
            source = serializedObject.FindProperty("BoolSource");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(viewModel);
            EditorGUILayout.PropertyField(gObject);
            EditorGUILayout.PropertyField(source);
            if (GUILayout.Button("Locate View Model")) {
                activeBinding.LocateViewModel();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}