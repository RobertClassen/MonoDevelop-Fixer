namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	internal class CSProjectPostprocessor
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
		private class SettingsProvider : UnityEditor.SettingsProvider
		{
			public SettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
			{
			}

			public override void OnGUI(string searchContext)
			{
				Draw();
			}
		}

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