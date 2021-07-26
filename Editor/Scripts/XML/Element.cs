namespace MonoDevelopFixer.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable]
	internal partial class Element
	{
		#region Fields
		[SerializeField]
		private string name = null;
		[SerializeField]
		private Attribute[] attributes = null;
		#endregion

		#region Properties
		public string Name
		{ get { return name; } }

		public Attribute[] Attributes
		{ get { return attributes; } }
		#endregion

		#region Constructors

		#endregion

		#region Methods

		#endregion
	}
}