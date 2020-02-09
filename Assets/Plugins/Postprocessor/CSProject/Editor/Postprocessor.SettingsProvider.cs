namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	internal partial class Postprocessor
	{
		#if UNITY_2018_3_OR_NEWER
		public class SettingsProvider : UnityEditor.SettingsProvider
		{
			#region Fields

			#endregion

			#region Properties

			#endregion

			#region Constructor
			public SettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
			{
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