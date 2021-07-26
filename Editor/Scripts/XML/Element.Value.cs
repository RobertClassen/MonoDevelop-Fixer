namespace MonoDevelopFixer.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	internal partial class Element
	{
		[Serializable]
		internal class Value
		{
			#region Fields
			[SerializeField]
			private string name = string.Empty;
			[SerializeField]
			private string description = string.Empty;
			#endregion

			#region Properties
			public string Name
			{ get { return name; } }

			public string Description
			{ get { return description; } }
			#endregion

			#region Constructors

			#endregion

			#region Methods

			#endregion
		}
	}
}