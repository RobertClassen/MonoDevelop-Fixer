using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LangVersionPostprocessor : AssetPostprocessor
{
	#region Constants
	private const string openTag = "<LangVersion>";
	private const string closeTag = "</LangVersion>";
	#endregion

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
			int startIndex = contents.IndexOf(openTag) + openTag.Length;
			int endIndex = contents.IndexOf(closeTag);
			int length = endIndex - startIndex;

			if(startIndex >= 0 && endIndex >= 0)
			{
				contents = contents.Replace(
					openTag + contents.Substring(startIndex, length) + closeTag, 
					openTag + settings.LangVersions[settings.SelectedIndex].Name + closeTag);
			}
		}

		return contents;
	}
	#endregion
}