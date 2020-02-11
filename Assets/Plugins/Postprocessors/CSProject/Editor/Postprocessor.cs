namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// This class must derive from "AssetPostprocessor" since Unity searches for all classes which derive from it 
	/// and tries to call certain static methods on them, one of which is the "OnGeneratedCSProject" method below.
	/// </summary>
	internal partial class Postprocessor : AssetPostprocessor
	{
		#region Fields
		private static Property[] properties = null;
		#endregion

		#region Properties
		public static Property[] Properties
		{
			get
			{
				if(properties == null)
				{
					properties = Resources.LoadAll<Property>(string.Empty);
				}
				return properties;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods
		/// <summary>
		/// This method is called automatically by Unity after a .csproj file has been updated, see
		/// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/AssetPostprocessor.cs#L154-L167
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="contents">Contents.</param>
		public static string OnGeneratedCSProject(string path, string contents)
		{
			//Debug.Log(path);
			foreach(Property property in Properties)
			{
				if(property.SelectedEditMode == Property.EditMode.Ignore)
				{
					continue;
				}
				contents = property.ApplyTo(contents);
			}
			return contents;
		}

		#if UNITY_2018_3_OR_NEWER
		[SettingsProvider]
		private static UnityEditor.SettingsProvider GetSettingsProvider()
		{
			return new SettingsProvider("Preferences/CSProject");
		}
		#else
		[PreferenceItem("CSProject")]
		#endif
		private static void Draw()
		{
			foreach(Property propertyCollection in Properties)
			{
				propertyCollection.Draw();
				EditorGUILayout.Space();
			}
		}
		#endregion
	}
}