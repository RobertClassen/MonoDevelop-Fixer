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
			private string name = null;
			[SerializeField]
			private string value = null;
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