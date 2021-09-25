namespace MonoDevelopFixer.XML.CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml.Linq;
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// Overwrites the values of specified properties in *.csproj files after Unity updates them.
	/// </summary>
	/// <remarks>
	/// <para>Unity frequently rebuilds these 4 different *.csproj files (if they exist):</para>
	/// <para>* [Assembly-CSharp]-firstpass.csproj</para>
	/// <para>* [Assembly-CSharp]-Editor-firstpass.csproj</para>
	/// <para>* [Assembly-CSharp].csproj</para>
	/// <para>* [Assembly-CSharp]-Editor.csproj</para>
	/// <para>For more information see https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html</para>
	/// <para>
	/// This class must inherit from <see cref="AssetPostprocessor"/> since Unity searches for all classes which derive 
	/// from it and tries to call certain static methods on them, one of which is <see cref="OnGeneratedCSProject"/>.
	/// </para>
	/// </remarks>
	internal partial class Postprocessor : AssetPostprocessor
	{
		#region Constants
		private const string name = "MonoDevelop Fixer";
		private const string menuPath = "Tools/" + name + "/";
		private const string preferencesPath = "Preferences/" + name;
		private const string description = "[" + name + "]";
		private const string fileExtension = "*.csproj";
		private static readonly GUILayoutOption buttonWidth = GUILayout.Width(50f);
		private static readonly string loggingPreferenceName = typeof(Postprocessor).FullName + ".isLoggingEnabled";
		#endregion

		#region Fields
		private static ElementDefinition[] elementDefinitions = LoadElementDefinitions();
		private static string[] filePaths = GetFilePaths();
		private static string[] relativePaths = GetRelativePaths(filePaths);
		private static bool isLoggingEnabled = EditorPrefs.GetBool(loggingPreferenceName);
		#endregion

		#region Properties

		#endregion

		#region Constructors

		#endregion

		#region Methods
		/// <summary>
		/// Gets called automatically by Unity after a *.csproj file has been updated.
		/// </summary>
		/// <remarks>
		/// For implemetation details see 
		/// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/AssetPostprocessor.cs#L154-L167
		/// </remarks>
		static string OnGeneratedCSProject(string path, string contents)
		{
			return ApplyElementDefinitions(path, contents);
		}

		[MenuItem(menuPath + "Update all " + fileExtension + " files", false, 0)]
		static void UpdateAllCSProjectFiles()
		{
			filePaths = GetFilePaths();
			relativePaths = GetRelativePaths(filePaths);
			for(int i = 0; i < filePaths.Length; i++)
			{
				File.WriteAllText(filePaths[i], ApplyElementDefinitions(filePaths[i], File.ReadAllText(filePaths[i])));
			}
		}

		[MenuItem(menuPath + "Open Preferences", false, 1)]
		static void OpenPreferences()
		{
			#if UNITY_2018_3_OR_NEWER
			SettingsService.OpenUserPreferences(preferencesPath);
			#else
			EditorApplication.ExecuteMenuItem("Edit/Preferences...");
			#endif
		}

		private static string ApplyElementDefinitions(string path, string contents)
		{
			XDocument xDocument;
			using(StringReader stringReader = new StringReader(contents))
			{
				xDocument = XDocument.Load(stringReader);
				foreach(ElementDefinition elementDefinition in elementDefinitions)
				{
					if(elementDefinition.SelectedEditMode == ElementDefinition.EditMode.Ignore)
					{
						continue;
					}
					xDocument.Root.SetValueRecursively(elementDefinition);
				}
			}
			using(StringWriter stringWriter = new UTF8StringWriter())
			{
				xDocument.Save(stringWriter);
				contents = stringWriter.ToString();
			}
			if(isLoggingEnabled)
			{
				Debug.LogFormat(description + " File has been updated: {0}", path);
			}
			return contents;
		}

		#if UNITY_2018_3_OR_NEWER
		[SettingsProvider]
		private static UnityEditor.SettingsProvider GetSettingsProvider()
		{
			return new SettingsProvider(preferencesPath);
		}
		#else
		[PreferenceItem(name)]
		#endif
		private static void Draw()
		{
			DrawFiles();
			EditorGUILayout.LabelField(GUIContent.none, GUI.skin.horizontalSlider);
			DrawElementDefinitions();
			GUILayout.FlexibleSpace();
			DrawSettings();
		}

		private static void DrawFiles()
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				if(GUILayout.Button("Find", buttonWidth))
				{
					filePaths = GetFilePaths();
					relativePaths = GetRelativePaths(filePaths);
					if(isLoggingEnabled)
					{
						Debug.Log(description + " The list of " + fileExtension + " files has been refreshed.");
					}
				}
				GUILayout.Label("The following " + fileExtension + " files will be updated:", EditorStyles.boldLabel);
			}
			for(int i = 0; i < filePaths.Length; i++)
			{
				using(new EditorGUILayout.HorizontalScope())
				{
					if(GUILayout.Button("Show", buttonWidth))
					{
						EditorUtility.RevealInFinder(filePaths[i]);
					}
					GUILayout.Label(relativePaths[i]);
				}
			}
			if(GUILayout.Button("Update all " + fileExtension + " files"))
			{
				UpdateAllCSProjectFiles();
			}
		}

		private static void DrawElementDefinitions()
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				if(GUILayout.Button("Find", buttonWidth))
				{
					elementDefinitions = LoadElementDefinitions();
				}
				GUILayout.Label("The following ElementDefinitions will be applied: ", EditorStyles.boldLabel);
			}
			ValidateElementDefinitions();
			foreach(ElementDefinition elementDefinition in elementDefinitions)
			{
				EditorGUILayout.Space();
				elementDefinition.Draw();
			}
		}

		/// <summary>
		/// Updates references if an ElementDefinition has been removed since the last call to avoid NullReferenceExceptions.
		/// </summary>
		private static void ValidateElementDefinitions()
		{
			foreach(ElementDefinition elementDefinition in elementDefinitions)
			{
				if(elementDefinition == null)
				{
					elementDefinitions = LoadElementDefinitions();
					return;
				}
			}
		}

		private static void DrawSettings()
		{
			bool isLoggingEnabledNew = GUILayout.Toggle(isLoggingEnabled, "Log to Console");
			if(isLoggingEnabledNew != isLoggingEnabled)
			{
				EditorPrefs.SetBool(loggingPreferenceName, isLoggingEnabledNew);
				isLoggingEnabled = isLoggingEnabledNew;
			}
		}

		private static string[] GetFilePaths()
		{
			return Directory.GetFiles(Path.GetFullPath(Application.dataPath + "/.."), fileExtension);
		}

		private static string[] GetRelativePaths(string[] absolutePaths)
		{
			string[] relativePaths = new string[absolutePaths.Length];
			string dataPathAlt = Application.dataPath.Replace("Assets", string.Empty);
			string dataPath = dataPathAlt.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			int length = dataPath.Length;
			for(int i = 0; i < relativePaths.Length; i++)
			{
				if(absolutePaths[i].StartsWith(dataPath) || absolutePaths[i].StartsWith(dataPathAlt))
				{
					relativePaths[i] = "..\\" + absolutePaths[i].Substring(length);
				}
			}
			return relativePaths;
		}

		private static ElementDefinition[] LoadElementDefinitions()
		{
			if(isLoggingEnabled)
			{
				Debug.Log(description + " Updating ElementDefinitions.");
			}
			return Resources.LoadAll<ElementDefinition>(string.Empty);
		}
		#endregion
	}
}