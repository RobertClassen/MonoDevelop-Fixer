namespace MonoDevelopFixer
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	internal static class GUIStyleUtility
	{
		#region Fields
		private static GUIStyle richTextLabel = new GUIStyle(GUI.skin.label){ richText = true };
		#endregion

		#region Properties
		public static GUIStyle RichTextLabel
		{ get { return richTextLabel; } }
		#endregion

		#region Constructors

		#endregion

		#region Methods

		#endregion
	}
}