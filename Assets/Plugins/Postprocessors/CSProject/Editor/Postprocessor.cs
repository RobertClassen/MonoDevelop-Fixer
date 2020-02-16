namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// Contains methods to overwrite the values of properties in *.csproj files after Unity updates them.
	/// </summary>
	/// <remarks>
	/// <para>Unity frequently rebuilds these 4 different *.csproj files (if they exist):</para>
	/// <para>* Assembly-CSharp-firstpass.csproj</para>
	/// <para>* Assembly-CSharp-Editor-firstpass.csproj</para>
	/// <para>* Assembly-CSharp.csproj</para>
	/// <para>* Assembly-CSharp-Editor.csproj</para>
	/// <para>For more information see https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html</para>
	/// <para>
	/// This class must inherit from <see cref="AssetPostprocessor"/> since Unity searches for all classes which derive 
	/// from it and tries to call certain static methods on them, one of which is <see cref="OnGeneratedCSProject"/>.
	/// </para>
	/// </remarks>
	internal partial class Postprocessor : AssetPostprocessor
	{
		#region Constants
		private const string fileExtension = "*.csproj";
		private const float buttonWidth = 60f;
		#endregion

		#region Fields
		private static string[] filePaths = null;
		private static Property[] properties = null;
		#endregion

		#region Properties
		private static string[] FilePaths
		{
			get
			{
				if(filePaths == null)
				{
					filePaths = Directory.GetFiles(Path.GetFullPath(Application.dataPath + "/.."), fileExtension);
				}
				return filePaths;
			}
		}

		private static Property[] Properties
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
		/// This method is called automatically by Unity after a *.csproj file has been updated.
		/// </summary>
		/// <remarks>
		/// For implemetation details see 
		/// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/AssetPostprocessor.cs#L154-L167
		/// </remarks>
		/// <param name="path">Path.</param>
		/// <param name="contents">Contents.</param>
		static string OnGeneratedCSProject(string path, string contents)
		{
			return ApplyProperties(path, contents);
		}

		[MenuItem("Tools/Postprocessors/Update *.csproj files")]
		static void UpdateAllCSProjectFiles()
		{
			for(int i = 0; i < FilePaths.Length; i++)
			{
				File.WriteAllText(filePaths[i], ApplyProperties(filePaths[i], File.ReadAllText(filePaths[i])));
			}
		}

		private static string ApplyProperties(string path, string contents)
		{
			foreach(Property property in Properties)
			{
				if(property.SelectedEditMode == Property.EditMode.Ignore)
				{
					continue;
				}
				contents = property.ApplyTo(contents);
			}
			Debug.Log("[Postprocessor] File has been updated: " + path);
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
			DrawFiles();
			EditorGUILayout.Space();
			DrawProperties();
		}

		private static void DrawFiles()
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				if(GUILayout.Button("Refresh", GUILayout.Width(buttonWidth)))
				{
					filePaths = null;
					Debug.Log("[Postprocessor] The list of *csproj files has been refreshed.");
				}
				GUILayout.Label("The following *.csproj files will be updated by the Postprocessor:", EditorStyles.boldLabel);
				GUILayout.FlexibleSpace();
			}
			for(int i = 0; i < FilePaths.Length; i++)
			{
				using(new EditorGUILayout.HorizontalScope())
				{
					if(GUILayout.Button("Show", GUILayout.Width(buttonWidth)))
					{
						EditorUtility.RevealInFinder(filePaths[i]);
					}
					GUILayout.Label(filePaths[i]);
				}
			}
		}

		private static void DrawProperties()
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