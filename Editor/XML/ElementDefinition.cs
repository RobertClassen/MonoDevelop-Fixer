namespace RCDev.Postprocessors.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// Defines which XML Element with the same parents and attributes should be overwritten in *.xml files.
	/// </summary>
	/// <remarks>
	/// Instances must be placed in a "Resources" folder to be found.
	/// </remarks>
	[CreateAssetMenu(menuName = "Postprocessors/XML/ElementDefinition")]
	[Serializable]
	internal class ElementDefinition : ScriptableObject
	{
		#region Constants
		private static readonly char[] separator = { '.' };
		private const float popupWidth = 75f;
		private const float buttonWidth = 60f;
		private const float spaceWidth = 20f;
		#endregion

		#region Enums
		public enum EditMode
		{
			Ignore,
			Overwrite,
		}
		#endregion

		#region Fields
		[SerializeField]
		private Element[] elements = null;
		[SerializeField]
		private Element.Value[] valueOptions = null;
		[NonSerialized]
		private string[] valueNames = null;

		[SerializeField]
		private string infoURL = string.Empty;

		[SerializeField]
		private EditMode selectedEditMode = EditMode.Overwrite;
		[SerializeField]
		private int selectedValueOptionIndex = 0;
		#endregion

		#region Properties
		public Element[] Elements
		{ get { return elements; } }

		public EditMode SelectedEditMode
		{ get { return selectedEditMode; } }

		public string SelectedValueOption
		{ get { return valueOptions[selectedValueOptionIndex].Name; } }

		private string[] ValueNames
		{
			get
			{
				if(valueNames == null)
				{
					valueNames = valueOptions.Select(valueOption => valueOption.Name).ToArray();
				}
				return valueNames;
			}
		}
		#endregion

		#region Constructors

		#endregion

		#region Methods
		public void Draw()
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				selectedEditMode = (EditMode)EditorGUILayout.EnumPopup(selectedEditMode, GUILayout.Width(popupWidth));
				GUILayout.Label(string.Format("<b>{0}</b>", name), GUIStyleUtility.RichTextLabel);
				if(selectedEditMode != EditMode.Ignore && selectedValueOptionIndex < valueOptions.Length)
				{
					GUILayout.Label("to");
					selectedValueOptionIndex = EditorGUILayout.Popup(selectedValueOptionIndex, ValueNames, 
						GUILayout.Width(popupWidth));
				}
				GUILayout.FlexibleSpace();
				DrawInfoURL();
			}
			DrawDefinition();
		}

		private void DrawInfoURL()
		{
			if(!string.IsNullOrEmpty(infoURL))
			{
				if(GUILayout.Button(new GUIContent("Info", infoURL), GUILayout.Width(buttonWidth)))
				{
					Help.BrowseURL(infoURL);
				}
			}
		}

		private void DrawDefinition()
		{
			if(selectedEditMode == EditMode.Ignore)
			{
				return;
			}

			using(new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Space(spaceWidth);
				if(selectedValueOptionIndex < valueOptions.Length)
				{
					GUILayout.Label(valueOptions[selectedValueOptionIndex].Description);
				}
				else
				{
					EditorGUILayout.HelpBox("This Element has no value options to select from.", MessageType.Warning);
				}
			}
		}
		#endregion
	}
}