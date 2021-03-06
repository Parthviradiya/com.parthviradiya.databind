using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ParthViradiya.Core
{
	[CustomPropertyDrawer(typeof(DataSourceReference))]
	public class DataSourceReferencePropertyDrawer : PropertyDrawer
	{
		private static readonly string[] BaseDataSourceArray = {"NONE"};

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.serializedObject.isEditingMultipleObjects) return;

			var labelWidth = EditorGUIUtility.labelWidth;
			var lineHeight = EditorGUIUtility.singleLineHeight;

			var indent = EditorGUI.indentLevel;
			EditorGUI.LabelField(new Rect(position.x, position.y, labelWidth, position.height), property.displayName);
			position.x += labelWidth;
			position.width -= labelWidth;

			var binding = property.serializedObject.targetObject as DataBinding;
			if (binding && binding.ViewModel)
			{
				
				var viewModelProp = property.FindPropertyRelative("viewModel");
				var dataSourceNameProp = property.FindPropertyRelative("dataSourceName");
				
				Type[] allowedDataTypes = null;
				var dataSourceReferenceAttribute = fieldInfo.GetCustomAttribute<AllowedDataTypesAttribute>();
				if (dataSourceReferenceAttribute != null)
				{
					allowedDataTypes = dataSourceReferenceAttribute.AllowedDataTypes;
				}

				var viewModelType = binding.ViewModel.GetType();
	
				var dataSourceNames = ReflectionHelpers.GetDataSourceFieldNames(viewModelType, allowedDataTypes).ToArray();
				var dataSourceDescriptions = BaseDataSourceArray.Concat(ReflectionHelpers.GetDataSourceDescriptions(viewModelType, allowedDataTypes)).ToArray();

				var index = -1;
				if (!string.IsNullOrEmpty(dataSourceNameProp.stringValue)) index = Array.IndexOf(dataSourceNames, dataSourceNameProp.stringValue);
				if (index == -1) index = 0;
				else index++;

				EditorGUI.BeginChangeCheck();
				viewModelProp.objectReferenceValue = binding.ViewModel;

				index = EditorGUI.Popup(new Rect(position.x, position.y, position.width, lineHeight), index, dataSourceDescriptions);
				dataSourceNameProp.stringValue = index == 0 ? null : dataSourceNames[index - 1];

				if (EditorGUI.EndChangeCheck()) {
					property.serializedObject.ApplyModifiedProperties();
					binding.Rebind();
				}
			}

			EditorGUI.indentLevel = indent;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}
	}
}