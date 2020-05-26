namespace Postprocessors.XML.CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	internal partial class Postprocessor
	{
		#if UNITY_2018_3_OR_NEWER
		/// <summary>
		/// Provides a minimalistic implementation to replace the obsolete <see cref="PreferenceItem"/> attribute.
		/// </summary>
		internal class SettingsProvider : UnityEditor.SettingsProvider
		{
			#region Fields

			#endregion

			#region Properties
			private static HashSet<string> KeyWords
			{
				get
				{
					HashSet<string> keyWords = new HashSet<string>{ "CSProject" };
					for(int i = 0; i < elementDefinitions.Length; i++)
					{
						keyWords.Add(elementDefinitions[i].name);
					}
					return keyWords;
				}
			}
			#endregion

			#region Constructors
			public SettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
			{
				keywords = KeyWords;
			}
			#endregion

			#region Methods
			public override void OnGUI(string searchContext)
			{
				Draw();
			}
			#endregion
		}
		#endif
	}
}