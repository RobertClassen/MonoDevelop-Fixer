namespace MonoDevelopFixer.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	internal partial class Element
	{
		[Serializable]
		internal class Attribute
		{
			#region Fields
			[SerializeField]
			private string name = string.Empty;
			[SerializeField]
			private string value = string.Empty;
			#endregion

			#region Properties
			public string Name
			{ get { return name; } }

			public string Value
			{ get { return value; } }
			#endregion

			#region Constructors

			#endregion

			#region Methods

			#endregion
		}
	}
}