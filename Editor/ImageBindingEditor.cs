using System;
using UnityEditor;
using UnityEngine;

namespace ParthViradiya.Core {
    [CustomEditor(typeof(Bindings.ImageBinding))]
    public class ImageBindingEditor:Editor {
        Bindings.ImageBinding imageBinding;

        SerializedProperty viewModel;
        SerializedProperty spriteSource;
        SerializedProperty imageTarget;
        SerializedProperty image;

        SerializedProperty imgWidth;
        SerializedProperty imgHight;
        SerializedProperty color;

        private void OnEnable() {
            imageBinding = (Bindings.ImageBinding)target;
            viewModel = serializedObject.FindProperty("ViewModel");
            imageTarget = serializedObject.FindProperty("imageTarget");

            image = serializedObject.FindProperty("Image");

            spriteSource = serializedObject.FindProperty("SpriteSource");
            imgWidth = serializedObject.FindProperty("WidthSource");
            imgHight = serializedObject.FindProperty("HeightSource");
            color = serializedObject.FindProperty("ColorSource");
        }
        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(viewModel);
            EditorGUILayout.PropertyField(imageTarget);

            EditorGUILayout.PropertyField(image);

            switch (imageBinding.imageTarget) {
                case ImageTarget.Sprite:
                EditorGUILayout.PropertyField(spriteSource);
                    break;
                case ImageTarget.Size:
                EditorGUILayout.PropertyField(imgWidth);
                EditorGUILayout.PropertyField(imgHight);
                    break;
                case ImageTarget.Color:
                    EditorGUILayout.PropertyField(color);
                    break;
                default:
                    break;
            }
            
            if (GUILayout.Button("Locate View Model")) {
                imageBinding.LocateViewModel();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
