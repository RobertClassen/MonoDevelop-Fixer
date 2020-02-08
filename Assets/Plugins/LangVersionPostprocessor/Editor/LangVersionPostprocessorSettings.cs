using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class LangVersionPostprocessorSettings : ScriptableObject
{
	#region Fields
	[SerializeField]
	private List<LangVersion> langVersions = new List<LangVersion>();
	[SerializeField]
	private bool doOverride;
	[SerializeField]
	private int selectedIndex;
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
		set
		{
			doOverride = value;
		}
	}

	public int SelectedIndex
	{
		get
		{
			return selectedIndex;
		}
		set
		{
			selectedIndex = value;
		}
	}
	#endregion

	#region Constructor
	
	#endregion

	#region Methods
	
	#endregion
}