namespace RCDev.Postprocessors.CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public static class GUIStyleUtility
	{
		#region Fields
		public static GUIStyle richTextLabel = null;
		#endregion

		#region Properties
		public static GUIStyle RichTextLabel
		{
			get
			{
				if(richTextLabel == null)
				{
					richTextLabel = new GUIStyle(GUI.skin.label);
					richTextLabel.richText = true;
				}
				return richTextLabel;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods

		#endregion
	}
}