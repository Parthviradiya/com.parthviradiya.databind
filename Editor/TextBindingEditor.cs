using UnityEditor;
using UnityEngine;
namespace ParthViradiya.Core
{
    [CustomEditor(typeof(Bindings.TextBinding))]
    public class TextBindingEditor : Editor {

        Bindings.TextBinding textbinding;

        SerializedProperty viewModel;
        SerializedProperty source;
        SerializedProperty textType;
        SerializedProperty simpleText;
        SerializedProperty TMPText;
        SerializedProperty format;

        private void OnEnable() {
            textbinding = (Bindings.TextBinding)target;
            viewModel = serializedObject.FindProperty("ViewModel");
            source = serializedObject.FindProperty("StringSource");
            textType = serializedObject.FindProperty("textType");
            simpleText = serializedObject.FindProperty("TextField");
            TMPText = serializedObject.FindProperty("TMPField");
            format = serializedObject.FindProperty("format");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(viewModel);

            EditorGUILayout.PropertyField(textType);
            if (textbinding.textType == TextType.Text) {
                EditorGUILayout.PropertyField(simpleText);
            } else {
                EditorGUILayout.PropertyField(TMPText);

            }
            EditorGUILayout.PropertyField(format);
            EditorGUILayout.PropertyField(source);


            if (GUILayout.Button("Locate View Model")) {
                textbinding.LocateViewModel();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
