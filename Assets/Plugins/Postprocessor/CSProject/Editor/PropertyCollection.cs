namespace Postprocessors
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
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
		public void Draw()
		{
			EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
			for(int i = 0; i < properties.Count; i++)
			{
				using(new EditorGUILayout.HorizontalScope())
				{
					using(new EditorGUI.DisabledScope(selectedIndex == i))
					{
						if(GUILayout.Button(properties[i].Name, GUILayout.Width(75f)))
						{
							selectedIndex = i;
						}
					}

					EditorGUILayout.LabelField(properties[i].Description);
				}
			}
		}
		#endregion
	}
}