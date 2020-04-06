namespace RCDev.Postprocessors.CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// Describes which property with the same name should be overwritten in *.csproj files.
	/// </summary>
	/// <remarks>
	/// Instances must be placed in the Editor\Resources\ folder in order for the <see cref="Postprocessor"/> to find them.
	/// </remarks>
	[CreateAssetMenu(menuName = "Postprocessors/CSProject/Property")]
	[Serializable]
	internal partial class Property : ScriptableObject
	{
		#region Constants
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
		private List<Value> values = new List<Value>();
		[NonSerialized]
		private string[] valueNames = null;
		[SerializeField]
		private string infoURL = string.Empty;

		[SerializeField]
		private EditMode selectedEditMode = EditMode.Overwrite;
		[SerializeField]
		private int selectedValueIndex = 0;
		#endregion

		#region Properties
		private string SelectedValue
		{
			get
			{
				return values[selectedValueIndex].Name;
			}
		}

		private string[] ValueNames
		{
			get
			{
				if(valueNames == null)
				{
					valueNames = values.Select(value => value.Name).ToArray();
				}
				return valueNames;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods
		/// <summary>
		/// Checks if a property with the same name exists in the passed contents a *csproj file and overwrites it.
		/// </summary>
		/// <returns>The contents</returns>
		/// <param name="contents">Contents.</param>
		public string ApplyTo(string contents)
		{
			if(selectedEditMode == Property.EditMode.Ignore)
			{
				return contents;
			}

			string startTag = string.Format("<{0}>", name);
			string endTag = string.Format("</{0}>", name);
			int startIndex = contents.IndexOf(startTag) + startTag.Length;
			int endIndex = contents.IndexOf(endTag);
			return startIndex < startTag.Length || endIndex < 0 ? contents : contents.Replace(
				string.Format("{0}{1}{2}", startTag, contents.Substring(startIndex, endIndex - startIndex), endTag), 
				string.Format("{0}{1}{2}", startTag, SelectedValue, endTag));
		}

		public void Draw()
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				selectedEditMode = (EditMode)EditorGUILayout.EnumPopup(selectedEditMode, GUILayout.Width(popupWidth));
				GUILayout.Label(string.Format("<b>{0}</b>", name), GUIStyleUtility.RichTextLabel);
				if(selectedEditMode != EditMode.Ignore && selectedValueIndex < values.Count)
				{
					GUILayout.Label("to");
					selectedValueIndex = EditorGUILayout.Popup(selectedValueIndex, ValueNames, GUILayout.Width(popupWidth));
				}
				GUILayout.FlexibleSpace();
				DrawInfoURL();
			}
			DrawDescription();
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

		private void DrawDescription()
		{
			if(selectedEditMode == EditMode.Ignore)
			{
				return;
			}

			using(new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Space(spaceWidth);
				if(selectedValueIndex < values.Count)
				{
					GUILayout.Label(values[selectedValueIndex].Description);
				}
				else
				{
					EditorGUILayout.HelpBox("This Property has no possible values to select from.", MessageType.Warning);
				}
			}
		}
		#endregion
	}
}