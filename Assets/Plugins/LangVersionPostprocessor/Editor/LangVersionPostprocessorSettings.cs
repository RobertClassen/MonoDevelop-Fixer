namespace CSProject
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu]
	[Serializable]
	public class LangVersionPostprocessorSettings : ScriptableObject
	{
		#region Fields
		[FormerlySerializedAs("langVersions")]
		[SerializeField]
		private List<LangVersion> langVersions = new List<LangVersion>();
		[SerializeField]
		private bool doOverride = true;
		[SerializeField]
		private int selectedIndex = 0;
		#endregion

		#region Properties
		public List<LangVersion> LangVersions
		{
			get
			{
				return langVersions;
			}
		}

		public bool DoOverride
		{
			get
			{
				return doOverride;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return selectedIndex;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods

		#endregion
	}
}