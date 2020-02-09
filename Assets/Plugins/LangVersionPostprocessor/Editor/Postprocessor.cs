namespace Postprocessors
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	internal class CSProject
	{
		#region Fields
		private static PropertyCollection[] propertyCollections = null;
		#endregion

		#region Properties
		public static PropertyCollection[] PropertyCollections
		{
			get
			{
				if(propertyCollections == null)
				{
					propertyCollections = Resources.LoadAll<PropertyCollection>(string.Empty);
					Debug.Log("Updated propertyCollections, found " + propertyCollections.Length);
				}
				return propertyCollections;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods
		public static string OnGeneratedCSProject(string path, string contents)
		{
			foreach(PropertyCollection propertyCollection in PropertyCollections)
			{
				if(propertyCollection.IsOverwriteEnabled)
				{
					contents = SetProperty(contents, propertyCollection.name, propertyCollection.Value);
				}
			}
			return contents;
		}

		private static string SetProperty(string contents, string key, string value)
		{
			string openTag = string.Format("<{0}>", key);
			string closeTag = string.Format("</{0}>", key);
			int startIndex = contents.IndexOf(openTag) + openTag.Length;
			int endIndex = contents.IndexOf(closeTag);
			string oldValue = contents.Substring(startIndex, endIndex - startIndex);
			return startIndex < 0 || endIndex < 0 || value == oldValue ? contents : contents.Replace(
				string.Format("{0}{1}{2}", openTag, oldValue, closeTag), 
				string.Format("{0}{1}{2}", openTag, value, closeTag));
		}

		#if UNITY_2018_3_OR_NEWER
		private class CSProjectPostprocessorSettingsProvider : SettingsProvider
		{
			public CSProjectPostprocessorSettingsProvider(string path, SettingsScope scopes = SettingsScope.User) : base(path, scopes)
			{
			}

			public override void OnGUI(string searchContext)
			{
				DrawSettings();
			}
		}

		[SettingsProvider]
		private static SettingsProvider GetSettingsProvider()
		{
			return new CSProjectPostprocessorSettingsProvider("Preferences/CSProjectPostprocessor");
		}
		#else
		[PreferenceItem("CSProjectPostprocessor")]
		#endif
		private static void DrawSettings()
		{
			foreach(PropertyCollection propertyCollection in PropertyCollections)
			{
				EditorGUILayout.LabelField(propertyCollection.name);

			}
		}
		#endregion
	}
}