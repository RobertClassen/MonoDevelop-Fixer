namespace Postprocessors
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
					Debug.Log("Updated properties, found " + properties.Length);
				}
				return properties;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods
		public static string OnGeneratedCSProject(string path, string contents)
		{
			foreach(Property property in Properties)
			{
				if(property.IsOverwriteEnabled)
				{
					contents = SetProperty(contents, property.name, property.Value);
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
			return new CSProjectPostprocessorSettingsProvider("Preferences/CSProject");
		}
		#else
		[PreferenceItem("CSProject")]
		#endif
		private static void DrawSettings()
		{
			foreach(Property propertyCollection in Properties)
			{
				propertyCollection.Draw();
			}
		}
		#endregion
	}
}