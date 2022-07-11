using UnityEngine;
using UnityEditor;
namespace ParthViradiya.Core {

    [CustomEditor(typeof(Bindings.CollectionBinding))]
    public class CollectionBindingEditor : Editor {
        Bindings.CollectionBinding collectionbinding;

        SerializedProperty viewModel;
        SerializedProperty root;
        SerializedProperty prefab;
        SerializedProperty collectionSource;


        private void OnEnable() {
            collectionbinding = (Bindings.CollectionBinding)target;

            viewModel = serializedObject.FindProperty("ViewModel");
            root = serializedObject.FindProperty("Root");
            prefab = serializedObject.FindProperty("Prefab");
            collectionSource = serializedObject.FindProperty("CollectionSource");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(viewModel);
            EditorGUILayout.PropertyField(root);
            EditorGUILayout.PropertyField(prefab);
            EditorGUILayout.PropertyField(collectionSource);

            if (GUILayout.Button("Locate View Model")) {
                collectionbinding.LocateViewModel();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}