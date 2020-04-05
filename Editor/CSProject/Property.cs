namespace RCDev.Postprocessors.CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
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
		private const float buttonWidth = 75f;
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
		[SerializeField]
		private string infoURL = string.Empty;

		[SerializeField]
		private EditMode selectedEditMode = EditMode.Overwrite;
		[SerializeField]
		private int selectedValueIndex = 0;
		#endregion

		#region Properties
		public EditMode SelectedEditMode
		{
			get
			{
				return selectedEditMode;
			}
		}

		private string SelectedValue
		{
			get
			{
				return values[selectedValueIndex].Name;
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
			string startTag = string.Format("<{0}>", name);
			string endTag = string.Format("</{0}>", name);
			int startIndex = contents.IndexOf(startTag);
			int endIndex = contents.IndexOf(endTag);
			string oldValue = contents.Substring(startIndex + startTag.Length, endIndex - startIndex - startTag.Length);
			return startIndex < 0 || endIndex < 0 || SelectedValue == oldValue ? contents : contents.Replace(
				string.Format("{0}{1}{2}", startTag, oldValue, endTag), 
				string.Format("{0}{1}{2}", startTag, SelectedValue, endTag));
		}

		public void Draw()
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Label(name, EditorStyles.boldLabel);
				DrawInfoURL();
				selectedEditMode = (EditMode)EditorGUILayout.EnumPopup(selectedEditMode, GUILayout.Width(buttonWidth));
				GUILayout.FlexibleSpace();
			}
			if(selectedEditMode == EditMode.Ignore)
			{
				return;
			}

			DrawValues();
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

		private void DrawValues()
		{
			for(int i = 0; i < values.Count; i++)
			{
				using(new EditorGUILayout.HorizontalScope())
				{
					using(new EditorGUI.DisabledScope(selectedValueIndex == i))
					{
						if(GUILayout.Button(values[i].Name, GUILayout.Width(buttonWidth)))
						{
							selectedValueIndex = i;
						}
					}
					GUILayout.Label(values[i].Description);
				}
			}
		}
		#endregion
	}
}