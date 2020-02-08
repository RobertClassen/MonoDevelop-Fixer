namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu]
	[Serializable]
	internal class PropertyCollection : ScriptableObject
	{
		#region Fields
		[SerializeField]
		private List<Property> properties = new List<Property>();
		[SerializeField]
		private bool isOverwriteEnabled = true;
		[SerializeField]
		private int selectedIndex = 0;
		#endregion

		#region Properties
		public bool IsOverwriteEnabled
		{
			get
			{
				return isOverwriteEnabled;
			}
		}

		public string Value
		{
			get
			{
				return properties[selectedIndex].Name;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods

		#endregion
	}
}