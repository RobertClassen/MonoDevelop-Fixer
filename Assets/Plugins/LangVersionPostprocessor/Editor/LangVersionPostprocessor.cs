using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LangVersionPostprocessor : AssetPostprocessor
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
		LangVersionPostprocessorSettings settings = 
			Resources.Load<LangVersionPostprocessorSettings>("LangVersionPostprocessorSettings");
		if(settings.DoOverride)
		{
			contents = SetProperty(contents, "LangVersion", settings.LangVersions[settings.SelectedIndex].Name);
		}

		return contents;
	}

	static string SetProperty(string contents, string tag, string value)
	{
		string openTag = string.Format("<{0}>", tag);
		string closeTag = string.Format("</{0}>", tag);
		int startIndex = contents.IndexOf(openTag) + openTag.Length;
		int endIndex = contents.IndexOf(closeTag);
		return startIndex < 0 || endIndex < 0 ? contents : contents.Replace(
			string.Format("{0}{1}{2}", openTag, contents.Substring(startIndex, endIndex - startIndex), closeTag), 
			string.Format("{0}{1}{2}", openTag, value, closeTag));
	}

	[PreferenceItem("My Preferences")]
	public static void PreferencesGUI()
	{
		
	}
	#endregion
}