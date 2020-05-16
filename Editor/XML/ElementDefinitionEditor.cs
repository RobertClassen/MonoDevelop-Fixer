namespace RCDev.Postprocessors.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(ElementDefinition))]
	internal class ElementDefinitionEditor : Editor
	{
		#region Constants
		private const float nameFieldWidth = 110f;
		private const string elementDescription = 
			"Nested Elements must be matched in the same order as below.";
		private const string attributeDescription = 
			"All respective Attributes must match but can be in any order.";
		private const string valueOptionDescription = 
			"If a matching Element is found its value will be replaced by the selected one below.";
		#endregion

		#region Fields

		#endregion

		#region Properties

		#endregion

		#region Constructors

		#endregion

		#region Methods
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawElements();

			SerializedProperty selectedEditMode = serializedObject.FindProperty("selectedEditMode");
			ElementDefinition.EditMode editMode = (ElementDefinition.EditMode)selectedEditMode.enumValueIndex;
			editMode = (ElementDefinition.EditMode)EditorGUILayout.EnumPopup(editMode);
			selectedEditMode.enumValueIndex = (int)editMode;

			DrawValueOptions(editMode == ElementDefinition.EditMode.Ignore);

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawElements()
		{
			SerializedProperty elements = serializedObject.FindProperty("elements");
			EditorGUILayout.HelpBox(string.Format("{0}\n{1}", elementDescription, attributeDescription), MessageType.Info);
			using(new EditorGUILayout.VerticalScope(GUI.skin.box))
			{
				using(new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.LabelField("Element Hierarchy", GUILayout.Width(nameFieldWidth));
					elements.arraySize = EditorGUILayout.IntField(elements.arraySize);
				}
				DrawHeader(20f, "Name", "Attributes");
				for(int i = 0; i < elements.arraySize; i++)
				{
					DrawElement(elements.GetArrayElementAtIndex(i));
				}
			}
		}

		private void DrawElement(SerializedProperty element)
		{
			SerializedProperty name = element.FindPropertyRelative("name");
			SerializedProperty attributes = element.FindPropertyRelative("attributes");
			using(new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Space(20f);
				name.stringValue = EditorGUILayout.TextField(name.stringValue, GUILayout.Width(nameFieldWidth));
				attributes.arraySize = EditorGUILayout.IntField(attributes.arraySize);
			}
			if(attributes.arraySize > 0)
			{
				DrawHeader(40f, "Name", "Value");
				for(int j = 0; j < attributes.arraySize; j++)
				{
					DrawAttribute(attributes.GetArrayElementAtIndex(j));
				}
			}
		}

		private void DrawAttribute(SerializedProperty attribute)
		{
			SerializedProperty name = attribute.FindPropertyRelative("name");
			SerializedProperty value = attribute.FindPropertyRelative("value");
			using(new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Space(40f);
				name.stringValue = EditorGUILayout.TextField(name.stringValue, GUILayout.Width(nameFieldWidth));
				value.stringValue = EditorGUILayout.TextField(value.stringValue);
			}
		}

		private void DrawValueOptions(bool isDisabled)
		{
			SerializedProperty valueOptions = serializedObject.FindProperty("valueOptions");
			SerializedProperty selectedValueOptionIndex = serializedObject.FindProperty("selectedValueOptionIndex");
			using(new EditorGUI.DisabledScope(isDisabled))
			{
				EditorGUILayout.HelpBox(valueOptionDescription, MessageType.Info);
				using(new EditorGUILayout.VerticalScope(GUI.skin.box))
				{
					using(new EditorGUILayout.HorizontalScope())
					{
						EditorGUILayout.LabelField("Value Options", GUILayout.Width(nameFieldWidth));
						valueOptions.arraySize = EditorGUILayout.IntField(valueOptions.arraySize);
					}
					DrawHeader(20f, "Value", "Description");
					int index = selectedValueOptionIndex.intValue;
					for(int i = 0; i < valueOptions.arraySize; i++)
					{
						if(DrawValueOption(valueOptions.GetArrayElementAtIndex(i), i == index))
						{
							index = i;
						}
					}
					selectedValueOptionIndex.intValue = index;
				}
			}
		}

		private bool DrawValueOption(SerializedProperty valueOption, bool isSelected)
		{
			SerializedProperty name = valueOption.FindPropertyRelative("name");
			SerializedProperty description = valueOption.FindPropertyRelative("description");
			using(new EditorGUILayout.HorizontalScope())
			{
				isSelected = EditorGUILayout.Toggle(isSelected, GUILayout.Width(15f));
				name.stringValue = EditorGUILayout.TextField(name.stringValue, GUILayout.Width(nameFieldWidth));
				description.stringValue = EditorGUILayout.TextField(description.stringValue);
			}
			return isSelected;
		}

		private void DrawHeader(float indentation, string titleLeft, string titleRight)
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Space(indentation);
				EditorGUILayout.LabelField(titleLeft, GUILayout.Width(nameFieldWidth));
				EditorGUILayout.LabelField(titleRight);
			}
		}
		#endregion
	}
}