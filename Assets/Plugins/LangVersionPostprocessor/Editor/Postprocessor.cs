namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	internal class Postprocessor : AssetPostprocessor
	{
		#region Fields

		#endregion

		#region Properties

		#endregion

		#region Constructor

		#endregion

		#region Methods
		public static string OnGeneratedCSProject(string path, string contents)
		{
			foreach(PropertyCollection propertyCollection in Resources.LoadAll<PropertyCollection>(string.Empty))
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
		#endregion
	}
}